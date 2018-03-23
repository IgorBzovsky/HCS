using AutoMapper;
using HCS.Api.Controllers.Resources;
using HCS.Api.Controllers.Resources.Tariff;
using HCS.Core;
using HCS.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace HCS.Api.Controllers
{
    [Produces("application/json")]
    [Route("tariffs")]
    //[Authorize(AuthenticationSchemes =
    //JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.ProviderPolicy)]
    public class TariffsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TariffsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Create tariff
        /// </summary>
        /// <param name="tariffResource"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(TariffResource))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] TariffResource tariffResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var tariff = _mapper.Map<TariffResource, Tariff>(tariffResource);
            _unitOfWork.Tariffs.Add(tariff);
            await _unitOfWork.CompleteAsync();
            tariff = await _unitOfWork.Tariffs.GetTariffAsync(tariff.Id);
            var result = _mapper.Map<Tariff, TariffResource>(tariff);
            return Ok(result);
        }

        /// <summary>
        /// Update tariff
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tariffResource"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(TariffResource))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] TariffResource tariffResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var tariff = await _unitOfWork.Tariffs.GetTariffAsync(id);
            if (tariff == null)
                return NotFound();
            _mapper.Map(tariffResource, tariff);
            await _unitOfWork.CompleteAsync();
            tariff = await _unitOfWork.Tariffs.GetTariffAsync(id);
            var result = _mapper.Map<Tariff, TariffResource>(tariff);
            return Ok(result);
        }

        /// <summary>
        /// Get tariff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(TariffResource))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var tariff = await _unitOfWork.Tariffs.GetTariffAsync(id);
            if (tariff == null)
                return NotFound();
            var result = _mapper.Map<Tariff, TariffResource>(tariff);
            return Ok(result);
        }

        /// <summary>
        /// Get all provider`s tariffs
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        [HttpGet("provider/{providerId}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<TariffListItemResource>))]
        public async Task<IActionResult> GetAllByProvider(int providerId)
        {
            var tariffs = await _unitOfWork.Tariffs.GetTariffsByProviderAsync(providerId);
            var result = _mapper.Map<IEnumerable<Tariff>, IEnumerable<TariffListItemResource>>(tariffs);
            return Ok(result);
        }

        /// <summary>
        /// Delete tariff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(int))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var tariff = await _unitOfWork.Tariffs.GetAsync(id);
            if (tariff == null)
                return NotFound();
            _unitOfWork.Tariffs.Remove(tariff);
            await _unitOfWork.CompleteAsync();
            return Ok(id);
        }
    }
}
