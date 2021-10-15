using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Web.Http;
using MiddleTier.API.ViewModels;

namespace MiddleTier.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Company")]
    public class CompanyController : MainController
    {
        public CompanyController(ILogger<MainController> logger) : base(logger)
        {
        }

        [HttpPost]
        public async Task<ActionResult<CompanyViewModel>> Add(CompanyViewModel companyViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fornecedorService.Adicionar(_mapper.Map<Fornecedor>(companyViewModel));

            return CustomResponse(companyViewModel);
        }
    }
}