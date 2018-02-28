using AutoMapper;
using HCS.Api.Controllers.Resources;
using HCS.Core;
using HCS.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HCS.Api.Controllers
{
    [Route("households")]
    public class HouseholdsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public HouseholdsController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHousehold([FromBody] SaveHouseholdResource householdResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var household = _mapper.Map<SaveHouseholdResource, Household>(householdResource);
            _unitOfWork.Households.Add(household);
            await _unitOfWork.CompleteAsync();
            
            var result = _mapper.Map<Household, SaveHouseholdResource>(household);
            return Ok(result);
        }
    }
}
