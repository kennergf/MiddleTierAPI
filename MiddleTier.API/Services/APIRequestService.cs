using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MiddleTier.API.Interfaces;
using MiddleTier.API.Response;
using MiddleTier.API.Settings;
using MiddleTier.API.ViewModels;

namespace MiddleTier.API.Services
{
    public class APIRequestService : BaseService, IAPIRequestService
    {
        private static string APIUrl;
        private static HttpClient client;

        public APIRequestService(INotifier notifier, IOptions<AppSettings> settings) : base(notifier)
        {
            APIUrl = settings.Value.DB_API_URL;
            client = new HttpClient();
            client.BaseAddress = new Uri(APIUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<CustomResponse<CompanyViewModel>> GetById(Guid id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(APIUrl + "/GetById?Id=" + id);

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var rawResponse = readTask.GetAwaiter().GetResult();

                    // Deserealize JSON to ViewModel
                    return JsonSerializer.Deserialize<CustomResponse<CompanyViewModel>>(rawResponse,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                Notify(response.ReasonPhrase);
                return new CustomResponse<CompanyViewModel>(false, ResponseMessage.COMPANY_NOT_FOUND + response.ReasonPhrase, null);
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
                Notify(ex.InnerException?.Message);
                return new CustomResponse<CompanyViewModel>(false, ResponseMessage.ERROR_PROCESSING_REQUEST + ex.Message, null);
            }
        }

        public async Task<CustomResponse<CompanyViewModel>> GetByISIN(string isin)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(APIUrl + "/GetByISIN?ISIN=" + isin);

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var rawResponse = readTask.GetAwaiter().GetResult();

                    // Deserealize JSON to ViewModel
                    var result = JsonSerializer.Deserialize<CustomResponse<CompanyViewModel>>(rawResponse,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return result;
                }
                Notify(response.ReasonPhrase);
                return new CustomResponse<CompanyViewModel>(false, ResponseMessage.COMPANY_NOT_FOUND + response.ReasonPhrase, null);
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
                Notify(ex.InnerException?.Message);
                return new CustomResponse<CompanyViewModel>(false, ResponseMessage.ERROR_PROCESSING_REQUEST + ex.Message, null);
            }
        }

        public async Task<CustomResponse<CompanyViewModel>> Add(CompanyViewModel company)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize<CompanyViewModel>(company), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(APIUrl, content);
                client.Dispose();

                if (response.IsSuccessStatusCode)
                {
                    return new CustomResponse<CompanyViewModel>(true, ResponseMessage.COMPANY_CREATED, null);
                }
                Notify(response.ReasonPhrase);
                return new CustomResponse<CompanyViewModel>(false, ResponseMessage.COULD_NOT_PERFORM_OPERATION + response.ReasonPhrase, null);
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
                Notify(ex.InnerException?.Message);
                return new CustomResponse<CompanyViewModel>(false, ResponseMessage.ERROR_PROCESSING_REQUEST + ex.Message, null);
            }
        }

        public async Task<CustomResponse<IEnumerable<CompanyViewModel>>> GetAll()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(APIUrl);

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var rawResponse = readTask.GetAwaiter().GetResult();

                    // Deserealize JSON to ViewModel
                    var result = JsonSerializer.Deserialize<CustomResponse<IEnumerable<CompanyViewModel>>>(rawResponse,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return result;
                }
                Notify(response.ReasonPhrase);
                return new CustomResponse<IEnumerable<CompanyViewModel>>(false, ResponseMessage.COULD_NOT_RETRIEVE_INFORMATION + response.ReasonPhrase, null);
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
                Notify(ex.InnerException?.Message);
                return new CustomResponse<IEnumerable<CompanyViewModel>>(false, ResponseMessage.ERROR_PROCESSING_REQUEST + ex.Message, null);
            }
        }

        public async Task<CustomResponse<CompanyViewModel>> Update(Guid id, CompanyViewModel company)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize<CompanyViewModel>(company), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(APIUrl + "?Id=" + id, content);

                if (response.IsSuccessStatusCode)
                {
                    return new CustomResponse<CompanyViewModel>(true, ResponseMessage.COMPANY_UPDATED, null);
                }
                Notify(response.ReasonPhrase);
                return new CustomResponse<CompanyViewModel>(false, ResponseMessage.COULD_NOT_PERFORM_OPERATION + response.ReasonPhrase, null);
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
                Notify(ex.InnerException?.Message);
                return new CustomResponse<CompanyViewModel>(false, ResponseMessage.ERROR_PROCESSING_REQUEST + ex.Message, null);
            }
        }

        public void Dispose()
        {

        }
    }
}