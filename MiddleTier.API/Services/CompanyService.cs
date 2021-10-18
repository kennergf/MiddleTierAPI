using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MiddleTier.API.Interfaces;
using MiddleTier.API.Models;
using MiddleTier.API.Response;
using MiddleTier.API.ViewModels;

namespace MiddleTier.API.Services
{
    public class CompanyService : BaseService, ICompanyService
    {
        private readonly IAPIRequestService _APIRequestService;

        public CompanyService(INotifier notifier, IAPIRequestService aPIRequestService) : base(notifier)
        {
            _APIRequestService = aPIRequestService;
        }

        public async Task<CustomResponse<CompanyViewModel>> GetById(Guid id)
        {
            return await _APIRequestService.GetById(id);
        }

        public async Task<CustomResponse<CompanyViewModel>> GetByISIN(string isin)
        {
            return await _APIRequestService.GetByISIN(isin);
        }

        public async Task<CustomResponse<CompanyViewModel>> Add(CompanyViewModel company)
        {
            var existingCompany = await _APIRequestService.GetByISIN(company.ISIN);
            if (existingCompany.Data != null && existingCompany?.Data?.ISIN == company.ISIN)
            {
                Notify(ResponseMessage.ISIN_DUPLICATED);
                return new CustomResponse<CompanyViewModel>(false, ResponseMessage.ISIN_DUPLICATED, null);
            }
            else
            {
                return await _APIRequestService.Add(company);
            }
        }

        public async Task<CustomResponse<CompanyViewModel>> Update(Guid id, CompanyViewModel company)
        {
            if(id != company.Id)
            {
                Notify(ResponseMessage.ID_PROVIDED_DOES_NOT_MATCH_COMPANY_ID);
                return new CustomResponse<CompanyViewModel>(false, ResponseMessage.ID_PROVIDED_DOES_NOT_MATCH_COMPANY_ID, null);
            }
            else
            {
                return await _APIRequestService.Update(id, company);
            }
        }

        public async Task<CustomResponse<IEnumerable<CompanyViewModel>>> GetAll()
        {
            return await _APIRequestService.GetAll();
        }

        public void Dispose()
        {
            _APIRequestService?.Dispose();
        }
    }
}