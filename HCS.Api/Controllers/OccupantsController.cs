using AutoMapper;
using HCS.Api.Controllers.Resources.Consumer.Household;
using HCS.Core;
using HCS.Core.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace HCS.Api.Controllers
{
    [Produces("application/json")]
    [Route("occupants")]
    public class OccupantsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public OccupantsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Create occupant
        /// </summary>
        /// <param name="occupantResource"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.ProviderPolicy)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(OccupantResource))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] SaveOccupantResource occupantResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var occupant = _mapper.Map<SaveOccupantResource, Occupant>(occupantResource);
            _unitOfWork.Occupants.Add(occupant);
            await _unitOfWork.CompleteAsync();
            occupant = await _unitOfWork.Occupants.GetOccupantAsync(occupant.Id);
            var result = _mapper.Map<Occupant, OccupantResource>(occupant);
            return Ok(result);
        }

        /// <summary>
        /// Get occupants by household
        /// </summary>
        /// <param name="householdId"></param>
        /// <returns></returns>
        [HttpGet("household/{householdId}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<OccupantResource>))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetOccupantsByHousehold(int householdId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var occupants = await _unitOfWork.Occupants.GetOccupantsByHouseholdAsync(householdId);
            var result = _mapper.Map<IEnumerable<Occupant>, IEnumerable<OccupantResource>>(occupants);
            return Ok(result);
        }

        /// <summary>
        /// Get occupants
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<OccupantResource>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var occupant = await _unitOfWork.Occupants.GetOccupantAsync(id);
            var result = _mapper.Map<Occupant, OccupantResource>(occupant);
            return Ok(result);
        }

        /// <summary>
        /// Update occupant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="occupantResource"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.ProviderPolicy)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(OccupantResource))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] SaveOccupantResource occupantResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var occupant = await _unitOfWork.Occupants.GetOccupantAsync(id);
            _mapper.Map(occupantResource, occupant);
            await _unitOfWork.CompleteAsync();
            occupant = await _unitOfWork.Occupants.GetOccupantAsync(occupant.Id);
            var result = _mapper.Map<Occupant, OccupantResource>(occupant);
            return Ok(result);
        }

        /// <summary>
        /// Delete occupant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.ProviderPolicy)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(int))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var occupant = await _unitOfWork.Occupants.GetOccupantAsync(id);
            _unitOfWork.Occupants.Remove(occupant);
            await _unitOfWork.CompleteAsync();
            return Ok(id);
        }
    }
}
