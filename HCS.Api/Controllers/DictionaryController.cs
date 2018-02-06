using AutoMapper;
using HCS.Api.Controllers.Resources;
using HCS.Core;
using HCS.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Api.Controllers
{
    [Route("api")]
    public class DictionaryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DictionaryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("locations")]
        [Authorize(Policy = "admin")]
        public async Task<IEnumerable<KeyValuePairResource>> GetLocations()
        {
            var locations = await _unitOfWork.Locations.GetLocationsIncludeChildrenAsync();
            var parentLocations = locations.Where(l => l.ParentId == null);
            return _mapper.Map<IEnumerable<Location>, IEnumerable<KeyValuePairResource>>(parentLocations);
        }

        /// <summary>
        /// Get regions (locations where ParentId is null)
        /// </summary>
        /// <returns></returns>
        [HttpGet("regions")]
        public async Task<IEnumerable<KeyValuePairResource>> GetRegions()
        {
            var regions = await _unitOfWork.Locations.FindAsync(x => x.ParentId == null);
            return _mapper.Map<IEnumerable<Location>, IEnumerable<KeyValuePairResource>>(regions);
        }

        [Authorize]
        [HttpGet("utilities")]
        public async Task<IEnumerable<KeyValuePairResource>> GetUtilities()
        {
            var utilities = await _unitOfWork.Utilities.GetAllAsync();
            return _mapper.Map<IEnumerable<Utility>, IEnumerable<KeyValuePairResource>>(utilities);
        }
    }
}
