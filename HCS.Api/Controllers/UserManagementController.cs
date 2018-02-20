using AutoMapper;
using HCS.Api.Controllers.Resources;
using HCS.Core.Domain;
using HCS.Data;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HCS.Api.Controllers
{
    [Route("user-management")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.AdminPolicy)]
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
            var result = await _userManager.CreateAsync(user, userResource.Password);
            if(!result.Succeeded)
            {
                ModelState.AddModelError("user_exist", "Користувач з такою електронною адресою вже зареєстрований");
                return BadRequest(ModelState);
            }
            
            var principal = await _claimsFactory.CreateAsync(user);
            var claims = principal.Claims.Where(x => x.Type == JwtClaimTypes.Role).ToList();
            var removedClaims = claims.Where(x => !userResource.Roles.Contains(x.Value));
            await _userManager.RemoveClaimsAsync(user, removedClaims);
            var addedClaims = userResource.Roles
                .Where(c => !claims.Any(x => x.Value == c))
                .Select(c => new Claim(JwtClaimTypes.Role, c));
            await _userManager.AddClaimsAsync(user, addedClaims);
            var savedUser = _mapper.Map<ApplicationUser, UserResource>(user);
            var userClaims = await _userManager.GetClaimsAsync(user);
            savedUser.Roles = userClaims.Where(x => x.Type.Equals("role")).Select(x => x.Value).ToList();
            return Ok(savedUser);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = _userManager.Users.ToList();
            var result = new List<UserResource>();

            foreach (var user in users)
            {
                var principal = await _claimsFactory.CreateAsync(user);
                var userResource = _mapper.Map<ApplicationUser, UserResource>(user);
                userResource.Roles = principal.Claims.Where(x => x.Type == JwtClaimTypes.Role).Select(x => x.Value).ToList();
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
