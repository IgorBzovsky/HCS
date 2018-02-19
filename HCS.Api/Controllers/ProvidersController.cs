﻿using AutoMapper;
using HCS.Api.Controllers.Resources;
using HCS.Core;
using HCS.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Api.Controllers
{
    [Authorize(Policy = "admin")]
    [Produces("application/json")]
    [Route("providers")]
    public class ProvidersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProvidersController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProvider([FromBody] SaveProviderResource providerResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var provider = _mapper.Map<SaveProviderResource, Provider>(providerResource);
            var user = await _userManager.GetUserAsync(User);
            provider.ApplicationUsers.Add(user);
            _unitOfWork.Providers.Add(provider);
            await _unitOfWork.CompleteAsync();
            
            provider = await _unitOfWork.Providers.GetProviderAsync(provider.Id);
            var result = _mapper.Map<Provider, ProviderResource>(provider);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvider(int id, [FromBody] SaveProviderResource providerResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var provider = await _unitOfWork.Providers.GetProviderAsync(id);

            if (provider == null)
                return NotFound();

            _mapper.Map(providerResource, provider);
            await _unitOfWork.CompleteAsync();

            provider = await _unitOfWork.Providers.GetProviderAsync(id);
            var result = _mapper.Map<Provider, ProviderResource>(provider);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProvider(int id)
        {
            var provider = await _unitOfWork.Providers.GetProviderAsync(id);
            if (provider == null)
                return NotFound();
            var providerResource = _mapper.Map<Provider, ProviderResource>(provider);
            return Ok(providerResource);
        }
    }
}
