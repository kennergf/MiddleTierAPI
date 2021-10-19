using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiddleTier.Api.Extensions;
using MiddleTier.API.Interfaces;
using MiddleTier.API.ViewModels;

namespace MiddleTier.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Companies")]
    public class CompanyController : MainController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(INotifier notifier, ICompanyService companyService,
                                 IMapper mapper) : base(notifier)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<ActionResult<CompanyViewModel>> GetById([FromQuery] string id)
        {
            var result = await _companyService.GetById(Guid.Parse(id));
            return CustomResponse(result.Data);
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<ActionResult<CompanyViewModel>> GetByISIN([FromQuery] string isin)
        {
            var result = await _companyService.GetByISIN(isin);
            return CustomResponse(result.Data);
        }

        [ClaimsAuthorize("Company", "Add")]
        [HttpPost]
        public async Task<ActionResult<CompanyViewModel>> Add(CompanyViewModel companyViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _companyService.Add(companyViewModel);
            // TODO return something more meaningful
            return CustomResponse(companyViewModel);
        }

        [ClaimsAuthorize("Company", "Update")]
        [HttpPut]
        public async Task<ActionResult<CompanyViewModel>> Update(Guid id, [FromBody] CompanyViewModel companyViewModel)
        {
            if (id != companyViewModel.Id)
            {
                NotifyErro("The Id provided is not the same infomed on the Company");
                return CustomResponse(companyViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _companyService.Update(id, companyViewModel);
            return CustomResponse(companyViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyViewModel>>> GetAll()
        {
            var result = await _companyService.GetAll();
            return CustomResponse(result.Data);
        }
    }
}