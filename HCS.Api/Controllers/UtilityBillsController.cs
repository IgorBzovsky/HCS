using AutoMapper;
using HCS.Api.Controllers.Resources.UtilityBills;
using HCS.Core;
using HCS.Core.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HCS.Api.Controllers
{
    [Produces("application/json")]
    [Route("utility-bills")]
    public class UtilityBillsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UtilityBillsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Create meter reading
        /// </summary>
        /// <param name="utilityBillResource"></param>
        /// <returns></returns>
        [HttpPost("meters-reading")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UtilityBillResource))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateMetersReading([FromBody] SaveUtilityBillResource utilityBillResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var utilityBill = _mapper.Map<SaveUtilityBillResource, UtilityBill>(utilityBillResource);
            utilityBill.DateCreated = DateTime.Now;
            _unitOfWork.UtilityBills.Add(utilityBill);
            await _unitOfWork.CompleteAsync();
            utilityBill = await _unitOfWork.UtilityBills.GetUtilityBillAsync(utilityBill.Id);
            var result = _mapper.Map<UtilityBill, UtilityBillResource>(utilityBill);
            return Ok(result);
        }

        /// <summary>
        /// Create utility bill
        /// </summary>
        /// <param name="utilityBillResource"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UtilityBillResource))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] SaveUtilityBillResource utilityBillResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var previousUtilityBill = await _unitOfWork.UtilityBills.GetLatestBillAsync(utilityBillResource.ConsumerId);
            var utilityBill = _mapper.Map<SaveUtilityBillResource, UtilityBill>(utilityBillResource);
            foreach(var line in utilityBill.UtilityBillLines)
            {
                var previousLine = previousUtilityBill.UtilityBillLines.FirstOrDefault(l => l.ConsumedUtilityId == line.ConsumedUtilityId);
                if(previousLine != null)
                {
                    if(previousLine.MeterReadingEnd > line.MeterReadingEnd)
                        return BadRequest(ModelState);
                }
            }
            utilityBill.DateCreated = DateTime.Now;

            if(previousUtilityBill != null)
            {
                foreach (var line in utilityBill.UtilityBillLines)
                {
                    var previousLine = previousUtilityBill.UtilityBillLines.FirstOrDefault(x => x.ConsumedUtilityId == line.ConsumedUtilityId);
                    if (previousLine != null)
                    {
                        line.MeterReadingStart = previousLine.MeterReadingEnd;
                    }
                }
            }
            _unitOfWork.UtilityBills.Add(utilityBill);
            await _unitOfWork.CompleteAsync();
            utilityBill = await _unitOfWork.UtilityBills.GetUtilityBillAsync(utilityBill.Id);
            utilityBill.Calculate();
            await _unitOfWork.CompleteAsync();
            var result = _mapper.Map<UtilityBill, UtilityBillResource>(utilityBill);
            return Ok(result);
        }

        /// <summary>
        /// Update meter reading
        /// </summary>
        /// <param name="id"></param>
        /// <param name="utilityBillResource"></param>
        /// <returns></returns>
        [HttpPut("meters-reading/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UtilityBillResource))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateMetersReading(int id, [FromBody] SaveUtilityBillResource utilityBillResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var utilityBill = await _unitOfWork.UtilityBills.GetUtilityBillAsync(id);
            if (utilityBill == null)
                return NotFound();
            _mapper.Map(utilityBillResource, utilityBill);
            utilityBill.DateCreated = DateTime.Now;
            await _unitOfWork.CompleteAsync();
            utilityBill = await _unitOfWork.UtilityBills.GetUtilityBillAsync(id);
            var result = _mapper.Map<UtilityBill, UtilityBillResource>(utilityBill);
            return Ok(result);
        }

        /// <summary>
        /// Get meters reading
        /// </summary>
        /// <param name="consumerId"></param>
        /// <returns></returns>
        [HttpGet("consumer/{consumerId}/meters-reading")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UtilityBillResource))]
        public async Task<IActionResult> GetMetersReading(int consumerId)
        {
            var utilityBill = await _unitOfWork.UtilityBills.GetMetersReadingAsync(consumerId);
            if (utilityBill == null)
                return NotFound();
            var result = _mapper.Map<UtilityBill, UtilityBillResource>(utilityBill);
            return Ok(result);
        }

        /// <summary>
        /// Get utility bill
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UtilityBillResource))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var utilityBill = await _unitOfWork.UtilityBills.GetUtilityBillAsync(id);
            if (utilityBill == null)
                return NotFound();
            var result = _mapper.Map<UtilityBill, UtilityBillResource>(utilityBill);
            return Ok(result);
        }

        /// <summary>
        /// Get consumer utility bills
        /// </summary>
        /// <param name="consumerId"></param>
        /// <returns></returns>
        [HttpGet("consumer/{consumerId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UtilityBillResource))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetConsumerUtilityBills(int consumerId)
        {
            var utilityBills = await _unitOfWork.UtilityBills.GetConsumerUtilityBillsAsync(consumerId);
            utilityBills = utilityBills.OrderByDescending(u => u.Year).ThenBy(u => u.Month);
            var result = _mapper.Map<IEnumerable<UtilityBill>, IEnumerable< UtilityBillListItemResource>>(utilityBills);
            return Ok(result);
        }
    }
}
