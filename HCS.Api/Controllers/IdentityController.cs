using HCS.Core;
using HCS.Core.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HCS.Api.Controllers
{
    public class IdentityController : ControllerBase
    {
        IUnitOfWork _unitOfWork;
        public IdentityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("identity")]
        public async Task<IActionResult> Get()
        {
            Location location = new Location
            {
                Name = "Sverdlova str",
                Parent = new Location
                {
                    Name = "Vinnitsa",
                    Parent = new Location
                    {
                        Name = "Vinnitsa district",
                        Parent = new Location
                        {
                            Name = "Vinnitsa region"
                        }
                    }
                }
            };
            _unitOfWork.Locations.Add(location);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpGet]
        [Route("superpowers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "admin")]
        public IActionResult Superpowers()
        {
            return new JsonResult("Superpowers!");
        }
    }
}
