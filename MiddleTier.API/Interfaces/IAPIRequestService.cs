using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiddleTier.API.Response;
using MiddleTier.API.ViewModels;

namespace MiddleTier.API.Interfaces
{
    /// <summary>
    /// Make requests on the API
    /// </summary>
    public interface IAPIRequestService : IDisposable
    {
        /// <summary>
        /// Retrieve Company based on ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CustomResponse<CompanyViewModel>> GetById(Guid id);

        /// <summary>
        /// Retrieve Company based on ISIN
        /// </summary>
        /// <param name="isin"></param>
        /// <returns></returns>
        Task<CustomResponse<CompanyViewModel>> GetByISIN(string isin);

        /// <summary>
        /// Add new Company to the system
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        Task<CustomResponse<CompanyViewModel>> Add(CompanyViewModel company);

        /// <summary>
        /// Update existing Company on the system
        /// </summary>
        /// <param name="id"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        Task<CustomResponse<CompanyViewModel>> Update(Guid id, CompanyViewModel company);

        /// <summary>
        /// Return a list with all the companies registered on the system
        /// </summary>
        /// <returns></returns>
        Task<CustomResponse<IEnumerable<CompanyViewModel>>> GetAll();
    }
}