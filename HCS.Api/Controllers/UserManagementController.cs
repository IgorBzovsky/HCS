using AutoMapper;
using HCS.Api.Controllers.Resources.Queries;
using HCS.Api.Controllers.Resources.User;
using HCS.Core.Domain;
using HCS.Core.Extensions;
using HCS.Core.Queries;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HCS.Api.Controllers
{
    [Route("user-management")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme)]
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
        /// Reset password
        /// </summary>
        /// <param name="changePasswordResource"></param>
        /// <returns></returns>
        [HttpPut("user/password")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserResource))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ResetPassword([FromBody] ChangePasswordResource changePasswordResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
                return NotFound();
            var result = await _userManager.ChangePasswordAsync(user, changePasswordResource.CurrentPassword, changePasswordResource.NewPassword);
            if (!result.Succeeded)
                return BadRequest();
            var savedUser = _mapper.Map<ApplicationUser, UserResource>(user);
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
            if (user == null || user.IsDeleted)
                return NotFound();
            var principal = await _claimsFactory.CreateAsync(user);
            var userResource = _mapper.Map<ApplicationUser, UserResource>(user);
            userResource.Roles = principal.Claims.Where(x => x.Type == JwtClaimTypes.Role).Select(x => x.Value).ToList();
            return Ok(userResource);
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserResource))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null || user.IsDeleted)
                return NotFound();
            var principal = await _claimsFactory.CreateAsync(user);
            var userResource = _mapper.Map<ApplicationUser, UserResource>(user);
            userResource.Roles = principal.Claims.Where(x => x.Type == JwtClaimTypes.Role).Select(x => x.Value).ToList();
            return Ok(userResource);
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <param name="queryResource"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserResource))]
        public async Task<IActionResult> GetAll([FromQuery] UserQueryResource queryResource = null)
        {
            var query = _mapper.Map<UserQueryResource, UserQuery>(queryResource);
            var users = _userManager.Users.Where(x => !x.IsDeleted);
            if(!string.IsNullOrWhiteSpace(query.Search))
            {
                users = users.Where(u => u.Email.Contains(query.Search) || u.LastName.Contains(query.Search) || u.FirstName.Contains(query.Search) || u.MiddleName.Contains(query.Search));
            }

            var columnsMap = new Dictionary<string, Expression<Func<ApplicationUser, object>>>
            {
                ["email"] = u => u.Email,
                ["lastName"] = u => u.LastName
            };
            users = users.ApplyOrdering(query, columnsMap);

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

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(string))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null || user.IsDeleted)
                return NotFound();
            user.IsDeleted = true;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest();
            return Ok(id);
        }

        
    }
}
