using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiddleTier.API.Interfaces;
using MiddleTier.API.Models;
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

        [HttpPost]
        public async Task<ActionResult<CompanyViewModel>> Add(CompanyViewModel companyViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _companyService.Add(companyViewModel);
            // TODO return something more meaningful
            return CustomResponse(companyViewModel);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyViewModel>>> GetAll()
        {
            var result = await _companyService.GetAll();
            return CustomResponse(result);
        }
    }
}