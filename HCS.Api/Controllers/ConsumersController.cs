using AutoMapper;
using HCS.Api.Controllers.Resources;
using HCS.Api.Controllers.Resources.Consumer;
using HCS.Api.Controllers.Resources.Consumer.Household;
using HCS.Core;
using HCS.Core.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace HCS.Api.Controllers
{
    [Produces("application/json")]
    [Route("consumers")]
    public class ConsumersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public ConsumersController(IMapper mapper, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Create consumer
        /// </summary>
        /// <param name="consumerResource"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.ProviderPolicy)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ConsumerResource))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] ConsumerResource consumerResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var consumer = _mapper.Map<ConsumerResource, Consumer>(consumerResource);
            _unitOfWork.Consumers.Add(consumer);
            await _unitOfWork.CompleteAsync();
            consumer = await _unitOfWork.Consumers.GetConsumerAsync(consumer.Id);
            var result = _mapper.Map<Consumer, ConsumerResource>(consumer);
            return Ok(result);
        }

        /// <summary>
        /// Update consumer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="consumerResource"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.ProviderPolicy)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ConsumerResource))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] ConsumerResource consumerResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var consumer = await _unitOfWork.Consumers.GetConsumerAsync(id);
            if (consumer == null)
                return NotFound();
            _mapper.Map(consumerResource, consumer);
            await _unitOfWork.CompleteAsync();
            consumer = await _unitOfWork.Consumers.GetConsumerAsync(id);
            var result = _mapper.Map<Consumer, ConsumerResource>(consumer);
            return Ok(result);
        }

        /// <summary>
        /// Get consumer categories by ConsumerTypeId
        /// </summary>
        /// <param name="consumerTypeId"></param>
        /// <returns></returns>
        [HttpGet("categories/{consumerTypeId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.ProviderPolicy)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<KeyValuePairResource>))]
        public async Task<IActionResult> GetConsumerTypeCategories(int consumerTypeId)
        {
            var consumerCategories = await _unitOfWork.Consumers.GetCategoriesByTypeIdAsync(consumerTypeId);
            var result = _mapper.Map<IEnumerable<ConsumerCategory>, IEnumerable<KeyValuePairResource>>(consumerCategories);
            return Ok(result);
        }

        /// <summary>
        /// Get consumer categories by ConsumerType name (case ignored)
        /// </summary>
        /// <param name="consumerTypeName"></param>
        /// <returns></returns>
        [HttpGet("categories/type/{consumerTypeName}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.ProviderPolicy)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<KeyValuePairResource>))]
        public async Task<IActionResult> GetConsumerTypeCategories(string consumerTypeName)
        {
            var consumerCategories = await _unitOfWork.Consumers.GetCategoriesByTypeNameAsync(consumerTypeName);
            var result = _mapper.Map<IEnumerable<ConsumerCategory>, IEnumerable<KeyValuePairResource>>(consumerCategories);
            return Ok(result);
        }

        /// <summary>
        /// Get consumer by location
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        [HttpGet("location/{locationId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.ProviderPolicy)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ConsumerResource))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetConsumerByLocationId(int locationId)
        {
            var consumer = await _unitOfWork.Consumers.GetConsumerByLocationAsync(locationId);
            if (consumer == null)
                return NotFound();
            var consumerResource = _mapper.Map<Consumer, ConsumerResource>(consumer);
            return Ok(consumerResource);
        }

        /// <summary>
        /// Get consumer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.ProviderPolicy)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ConsumerResource))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var consumer = await _unitOfWork.Consumers.GetConsumerAsync(id);
            if (consumer == null)
                return NotFound();
            var consumerResource = _mapper.Map<Consumer, ConsumerResource>(consumer);
            return Ok(consumerResource);
        }

        /// <summary>
        /// Get consumer including tariffs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("consumer-info/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ConsumerInfoResource))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetConsumerInfo(int id)
        {
            var consumer = await _unitOfWork.Consumers.GetConsumerAsync(id);
            if (consumer == null)
                return NotFound();
            var result = _mapper.Map<Consumer, ConsumerInfoResource>(consumer);
            return Ok(result);
        }

        /// <summary>
        /// Get consumer including location
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("consumer-location/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ConsumerLocationResource))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetConsumerLocation(int id)
        {
            var consumer = await _unitOfWork.Consumers.GetConsumerAsync(id);
            if (consumer == null)
                return NotFound();
            var result = _mapper.Map<Consumer, ConsumerLocationResource>(consumer);
            return Ok(result);
        }

        /// <summary>
        /// Get all consumers
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        [HttpGet("provider/{providerId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.ProviderPolicy)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<ConsumerLocationResource>))]
        public async Task<IActionResult> GetAll(int providerId)
        {
            var consumers = await _unitOfWork.Consumers.GetAllByProviderAsync(providerId);
            var result = _mapper.Map<IEnumerable<Consumer>, IEnumerable<ConsumerLocationResource>>(consumers);
            return Ok(result);
        }

        /// <summary>
        /// Get current user consumers
        /// </summary>
        /// <returns></returns>
        [HttpGet("user/current")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<ConsumerLocationResource>))]
        public async Task<IActionResult> GetUserConsumers()
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return NotFound();
            var consumers = await _unitOfWork.Consumers.GetUserConsumersAsync(user.Id);
            var result = _mapper.Map<IEnumerable<Consumer>, IEnumerable<ConsumerLocationResource>>(consumers);
            return Ok(result);
        }

        /// <summary>
        /// Get consumer types
        /// </summary>
        /// <returns></returns>
        [HttpGet("types")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.ProviderPolicy)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<KeyValuePairResource>))]
        public async Task<IActionResult> GetConsumerTypes()
        {
            var types = await _unitOfWork.Consumers.GetConsumerTypesAsync();
            var result = _mapper.Map<IEnumerable<ConsumerType>, IEnumerable<KeyValuePairResource>>(types);
            return Ok(result);
        }

        /// <summary>
        /// Get exemptions
        /// </summary>
        /// <returns></returns>
        [HttpGet("exemptions")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = RolePolicies.ProviderPolicy)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<ExemptionResource>))]
        public async Task<IActionResult> GetExemptions()
        {
            var exemptions = await _unitOfWork.Consumers.GetExemptionsAsync();
            var result = _mapper.Map<IEnumerable<Exemption>, IEnumerable<ExemptionResource>>(exemptions);
            return Ok(result);
        }
    }
}
