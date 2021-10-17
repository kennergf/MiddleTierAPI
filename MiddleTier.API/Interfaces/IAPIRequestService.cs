using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiddleTier.API.Response;
using MiddleTier.API.ViewModels;

namespace MiddleTier.API.Interfaces
{
    public interface IAPIRequestService : IDisposable
    {
        Task<CompanyViewModel> GetById(Guid id);
        
        Task<CompanyViewModel> GetByISIN(string isin);
        
        Task<bool> Add(CompanyViewModel company);

        Task<IEnumerable<CompanyViewModel>> GetAll();
    }
}