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

        public async Task<CompanyViewModel> GetByISIN(string isin)
        {
            return await _APIRequestService.GetByISIN(isin);
        }

        public async Task<bool> Add(CompanyViewModel company)
        {
            var existingCompany = await _APIRequestService.GetByISIN(company.ISIN);
            if (existingCompany != null && existingCompany?.ISIN == company.ISIN)
            {
                Notify($"Can not insert duplicate ISIN for Company. Duplicate ISIN is ({company.ISIN})");
                return false;
            }
            else
            {
                return await _APIRequestService.Add(company);
            }
        }

        public async Task<IEnumerable<CompanyViewModel>> GetAll()
        {
            return await _APIRequestService.GetAll();
        }

        public void Dispose()
        {
            _APIRequestService?.Dispose();
        }
    }
}