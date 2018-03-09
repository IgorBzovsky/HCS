using AutoMapper;
using HCS.Api.Controllers.Resources.User;
using HCS.Core.Domain;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HCS.Api.Controllers
{
    [Route("user-management")]
    [Produces("application/json")]
    /*[Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.AdminPolicy)]*/
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

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="userResource"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserResource))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] UserResource userResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = _mapper.Map<UserResource, ApplicationUser>(userResource);
            var result = await _userManager.CreateAsync(user, userResource.Password);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("user_exist", "User already exists");
                return BadRequest(ModelState);
            }
            var principal = await _claimsFactory.CreateAsync(user);
            var claims = principal.Claims.Where(x => x.Type == JwtClaimTypes.Role).ToList();
            var addedClaims = userResource.Roles
                .Where(c => !claims.Any(x => x.Value == c))
                .Select(c => new Claim(JwtClaimTypes.Role, c));
            await _userManager.AddClaimsAsync(user, addedClaims);
            var savedUser = _mapper.Map<ApplicationUser, UserResource>(user);
            var userClaims = await _userManager.GetClaimsAsync(user);
            savedUser.Roles = userClaims.Where(x => x.Type.Equals(JwtClaimTypes.Role)).Select(x => x.Value).ToList();
            return Ok(savedUser);
        }
        
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userResource"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserResource))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(string id, [FromBody] UserResource userResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            _mapper.Map(userResource, user);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest();
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
            savedUser.Roles = userClaims.Where(x => x.Type.Equals(JwtClaimTypes.Role)).Select(x => x.Value).ToList();
            return Ok(savedUser);
        }

        
        /// <summary>
        /// Get user by UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("user-name/{userName}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserResource))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return NotFound();
            var principal = await _claimsFactory.CreateAsync(user);
            var userResource = _mapper.Map<ApplicationUser, UserResource>(user);
            userResource.Roles = principal.Claims.Where(x => x.Type == JwtClaimTypes.Role).Select(x => x.Value).ToList();
            return Ok(userResource);
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
    }
}
