using AutoMapper;
using HCS.Api.Controllers.Resources.Consumer.Household;
using HCS.Core;
using HCS.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using System.Threading.Tasks;

namespace HCS.Api.Controllers
{
    [Produces("application/json")]
    [Route("occupants")]
    /*[Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.ProviderPolicy)]*/
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
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(OccupantResource))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateOccupant([FromBody] SaveOccupantResource occupantResource)
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
    }
}
