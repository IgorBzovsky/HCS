using AutoMapper;
using HCS.Api.Controllers.Resources;
using HCS.Core;
using HCS.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HCS.Api.Controllers
{
    //[Authorize("admin")]
    [Produces("application/json")]
    [Route("api/user-management")]
    public class UserManagementController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;

        public UserManagementController(IMapper mapper, UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _mapper = mapper;
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserResource userResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<UserResource, ApplicationUser>(userResource);
            await _userManager.CreateAsync(user, userResource.Password);
            var principal = await _claimsFactory.CreateAsync(user);
            var claims = principal.Claims.Where(x => x.Type == "role").ToList();
            var removedClaims = claims.Where(x => !userResource.Roles.Contains(x.Value));
            await _userManager.RemoveClaimsAsync(user, removedClaims);
            var addedClaims = userResource.Roles
                .Where(c => !claims.Any(x => x.Value == c))
                .Select(c => new Claim("role", c));
            await _userManager.AddClaimsAsync(user, addedClaims);
            var result = _mapper.Map<ApplicationUser, UserResource>(user);
            var userClaims = await _userManager.GetClaimsAsync(user);
            result.Roles = userClaims.Where(x => x.Type.Equals("role")).Select(x => x.Value).ToList();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = _userManager.Users.ToList();
            var result = new List<UserResource>();

            foreach (var user in users)
            {
                var principal = await _claimsFactory.CreateAsync(user);
                var claims = principal.Claims.Where(x => x.Type == "role").ToList();
                var userResource = _mapper.Map<ApplicationUser, UserResource>(user);
                userResource.Roles = claims.Select(x => x.Value).ToList();
                result.Add(userResource);
            }

            return Ok(result);
        }

        /*[HttpPut("{id}")]
        public void Put(string id, [FromBody]UserDto userDto)
        {
            var user = _context.Users.First(t => t.Id == id);

            user.IsAdmin = userDto.IsAdmin;
            if (userDto.IsActive)
            {
                if (user.AccountExpires < DateTime.UtcNow)
                {
                    user.AccountExpires = DateTime.UtcNow.AddDays(7.0);
                }
            }
            else
            {
                // deactivate user
                user.AccountExpires = new DateTime();
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }*/
    }
}
