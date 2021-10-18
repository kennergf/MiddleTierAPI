using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiddleTier.API.Response;
using MiddleTier.API.ViewModels;

namespace MiddleTier.API.Interfaces
{
    public interface ICompanyService : IDisposable
    {
        Task<CustomResponse<CompanyViewModel>> GetById(Guid id);
        
        Task<CustomResponse<CompanyViewModel>> GetByISIN(string isin);

        Task<CustomResponse<CompanyViewModel>> Add(CompanyViewModel company);

        Task<CustomResponse<CompanyViewModel>> Update(Guid id, CompanyViewModel company);

        Task<CustomResponse<IEnumerable<CompanyViewModel>>> GetAll();
    }
}