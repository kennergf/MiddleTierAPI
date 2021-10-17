using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiddleTier.API.Interfaces;
using MiddleTier.API.Response;
using MiddleTier.API.ViewModels;

namespace MiddleTier.Test
{
    public class APIRequetServiceMock : IAPIRequestService
    {
        private List<CompanyViewModel> companies;
        public APIRequetServiceMock()
        {
            companies = new List<CompanyViewModel>
            {
                new CompanyViewModel{Id = Guid.Parse("55bca715-c209-45c6-8d53-1ab35907ce8a"), Name = "Apple", Exchange = "Nasdaq", Ticker = "0ticker0", ISIN = "US12A1235699", WebSite = "apple.com"}
            };
        }

        public Task<bool> Add(CompanyViewModel company)
        {
            companies.Add(company);
            return Task.FromResult(true);
        }

        public void Dispose()
        {
            
        }

        public Task<IEnumerable<CompanyViewModel>> GetAll()
        {
            return Task.FromResult((IEnumerable<CompanyViewModel>)companies);
        }

        public Task<CompanyViewModel> GetById(Guid id)
        {
            return Task.FromResult((CompanyViewModel)companies.Where(c => c.Id == id).FirstOrDefault());
        }

        public Task<CompanyViewModel> GetByISIN(string isin)
        {
            return Task.FromResult((CompanyViewModel)companies.Where(c => c.ISIN == isin).FirstOrDefault());
        }
    }
}