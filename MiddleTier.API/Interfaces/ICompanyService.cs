using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiddleTier.API.Models;
using MiddleTier.API.ViewModels;

namespace MiddleTier.API.Interfaces
{
    public interface ICompanyService : IDisposable
    {
        Task<bool> Add(Company company);

        Task<IEnumerable<CompanyViewModel>> GetAll();
    }
}