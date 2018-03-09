using AutoMapper;
using HCS.Api.Controllers.Resources;
using HCS.Api.Controllers.Resources.Provider;
using HCS.Core;
using HCS.Core.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HCS.Api.Controllers
{
    [Route("providers")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.ProviderPolicy)]
    public class ProvidersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProvidersController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveProviderResource providerResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var provider = _mapper.Map<SaveProviderResource, Provider>(providerResource);
            provider.ApplicationUsers.Add(user);
            _unitOfWork.Providers.Add(provider);
            await _unitOfWork.CompleteAsync();

            provider = await _unitOfWork.Providers.GetProviderAsync(provider.Id);
            var result = _mapper.Map<Provider, ProviderResource>(provider);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaveProviderResource providerResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var provider = await _unitOfWork.Providers.GetProviderAsync(id);

            if (provider == null)
                return NotFound();

            _mapper.Map(providerResource, provider);
            await _unitOfWork.CompleteAsync();

            provider = await _unitOfWork.Providers.GetProviderAsync(id);
            var result = _mapper.Map<Provider, ProviderResource>(provider);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var provider = await _unitOfWork.Providers.GetProviderAsync(id);
            if (provider == null)
                return NotFound();
            var providerResource = _mapper.Map<Provider, ProviderResource>(provider);
            return Ok(providerResource);
        }

        [HttpGet]
        [Route("current")]
        public async Task<IActionResult> GetCurrentUserProvider()
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if(user.ProviderId == null)
                return NoContent();
            var provider = await _unitOfWork.Providers.GetProviderAsync(user.ProviderId.Value);
            var providerResource = _mapper.Map<Provider, ProviderResource>(provider);
            return Ok(providerResource);
        }
    }
}
