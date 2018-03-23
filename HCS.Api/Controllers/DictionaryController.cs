using AutoMapper;
using HCS.Api.Controllers.Resources;
using HCS.Api.Controllers.Resources.Location;
using HCS.Api.Controllers.Resources.Utilities;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DictionaryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DictionaryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Get location along with parent locations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("locations/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LocationResource))]
        public async Task<LocationResource> GetLocation(int id)
        {
            var location = await _unitOfWork.Locations.GetLocationIncludeParentAsync(id);
            return _mapper.Map<Location, LocationResource>(location);
        }

        /// <summary>
        /// Get regions (locations where ParentId is null)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("locations/regions")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<KeyValuePairResource>))]
        public async Task<IEnumerable<KeyValuePairResource>> GetRegions()
        {
            var regions = await _unitOfWork.Locations.FindAsync(x => x.ParentId == null);
            return _mapper.Map<IEnumerable<Location>, IEnumerable<KeyValuePairResource>>(regions);
        }

        /// <summary>
        /// Get locations by parent location
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("locations/parent/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<KeyValuePairResource>))]
        public async Task<IEnumerable<KeyValuePairResource>> GetLocationsByParentId(int id)
        {
            var locations = await _unitOfWork.Locations.FindAsync(x => x.ParentId == id);
            return _mapper.Map<IEnumerable<Location>, IEnumerable<KeyValuePairResource>>(locations);
        }

        /// <summary>
        /// Create location 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("locations")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LocationResource))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(LocationResource), Description = "Existing address returned")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAddress([FromBody] SaveLocationResource locationResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var originalAddress = await _unitOfWork.Locations.GetLocationByAddressAsync(locationResource.ParentId, locationResource.Building, locationResource.Appartment);

            //If address exists
            if(originalAddress != null)
            {
                var originalResult = _mapper.Map<Location, LocationResource>(originalAddress);
                return StatusCode((int)HttpStatusCode.Conflict, originalResult);
            }

            var address = _mapper.Map<SaveLocationResource, Location>(locationResource);
            _unitOfWork.Locations.Add(address);
            await _unitOfWork.CompleteAsync();
            address = await _unitOfWork.Locations.GetLocationIncludeParentAsync(address.Id);
            var result = _mapper.Map<Location, LocationResource>(address);
            return Ok(result);
        }

        /// <summary>
        /// Update address
        /// </summary>
        /// <param name="id"></param>
        /// <param name="locationResource"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("locations/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(LocationResource))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(LocationResource), Description = "Existing address returned")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] SaveLocationResource locationResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var originalAddress = await _unitOfWork.Locations.GetLocationByAddressAsync(locationResource.ParentId, locationResource.Building, locationResource.Appartment);

            //If address exists
            if (originalAddress != null)
            {
                var originalResult = _mapper.Map<Location, LocationResource>(originalAddress);
                return StatusCode((int)HttpStatusCode.Conflict, originalResult);
            }

            var address = await _unitOfWork.Locations.GetAsync(id);
            if (address == null)
                return NotFound();
            _mapper.Map(locationResource, address);
            await _unitOfWork.CompleteAsync();
            address = await _unitOfWork.Locations.GetLocationIncludeParentAsync(address.Id);
            var result = _mapper.Map<Location, LocationResource>(address);
            return Ok(result);
        }

        /// <summary>
        /// Get all utilities
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("utilities")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<UtilityResource>))]
        public async Task<IEnumerable<UtilityResource>> GetUtilities()
        {
            var utilities = await _unitOfWork.Utilities.GetAllAsync();
            return _mapper.Map<IEnumerable<Utility>, IEnumerable<UtilityResource>>(utilities);
        }
    }
}
