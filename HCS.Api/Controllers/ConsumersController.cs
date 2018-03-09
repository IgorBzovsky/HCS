using AutoMapper;
using HCS.Api.Controllers.Resources.Consumer.Household;
using HCS.Core;
using HCS.Core.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HCS.Api.Controllers
{
    [Produces("application/json")]
    [Route("consumers")]
    [Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.ProviderPolicy)]
    public class ConsumersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public ConsumersController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        /// <summary>
        /// Create household
        /// </summary>
        /// <param name="householdResource"></param>
        /// <returns></returns>
        [HttpPost("household")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(HouseholdResource))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] SaveHouseholdResource householdResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var household = _mapper.Map<SaveHouseholdResource, Household>(householdResource);
            _unitOfWork.Consumers.Add(household);
            await _unitOfWork.CompleteAsync();
            var result = _mapper.Map<Household, HouseholdResource>(household);
            return Ok(result);
        }

        /// <summary>
        /// Update household
        /// </summary>
        /// <param name="id"></param>
        /// <param name="householdResource"></param>
        /// <returns></returns>
        [HttpPut("household/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(HouseholdResource))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] SaveHouseholdResource householdResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var household = await _unitOfWork.Consumers.GetAsync(id);
            if (household == null || !(household is Household))
                return NotFound();
            _mapper.Map(householdResource, household);
            await _unitOfWork.CompleteAsync();
            var result = _mapper.Map<Household, HouseholdResource>(household as Household);
            return Ok(result);
        }

        /// <summary>
        /// Get consumer by location
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        [HttpGet("location/{locationId}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(HouseholdResource))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetConsumerByLocationId(int locationId)
        {
            var consumers = await _unitOfWork.Consumers.FindAsync(x => x.LocationId == locationId);
            var consumer = consumers.FirstOrDefault();
            if (consumer == null)
                return NotFound();
            if(consumer is Household)
            {
                var householdResource = _mapper.Map<Household, HouseholdResource>(consumer as Household);
                return Ok(householdResource);
            }
            return Ok();
        }
    }
}
