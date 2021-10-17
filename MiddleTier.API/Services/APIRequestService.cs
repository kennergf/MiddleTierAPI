using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MiddleTier.API.Interfaces;
using MiddleTier.API.Response;
using MiddleTier.API.ViewModels;

namespace MiddleTier.API.Services
{
    public class APIRequestService : BaseService, IAPIRequestService
    {
        private static string APIUrl = "http://localhost:4000/api/v1.0/Companies";
        private static HttpClient client;

        public APIRequestService(INotifier notifier) : base(notifier)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(APIUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<CompanyViewModel> GetById(Guid id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(APIUrl + "/GetById?Id=" + id);

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var rawResponse = readTask.GetAwaiter().GetResult();

                    // Deserealize JSON to ViewModel
                    return JsonSerializer.Deserialize<CompanyViewModel>(rawResponse,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                Notify(response.ReasonPhrase);
                return null;
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
                Notify(ex.InnerException?.Message);
                return null;
            }
        }

        public async Task<CompanyViewModel> GetByISIN(string isin)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(APIUrl + "/GetByISIN?ISIN=" + isin);

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var rawResponse = readTask.GetAwaiter().GetResult();

                    // Deserealize JSON to ViewModel
                    return JsonSerializer.Deserialize<CompanyViewModel>(rawResponse,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                Notify(response.ReasonPhrase);
                return null;
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
                Notify(ex.InnerException?.Message);
                return null;
            }
        }

        public async Task<bool> Add(CompanyViewModel company)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize<CompanyViewModel>(company), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(APIUrl, content);
                client.Dispose();

                if (response.IsSuccessStatusCode)
                {
                    // TODO refactor
                    var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var rawResponse = readTask.GetAwaiter().GetResult();
                    Console.WriteLine(rawResponse);
                    return true;
                }
                Notify(response.ReasonPhrase);
                return false;
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
                Notify(ex.InnerException?.Message);
                return false;
            }
        }

        public async Task<IEnumerable<CompanyViewModel>> GetAll()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(APIUrl);

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var rawResponse = readTask.GetAwaiter().GetResult();

                    // Deserealize JSON to ViewModel
                    return JsonSerializer.Deserialize<IEnumerable<CompanyViewModel>>(rawResponse,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                Notify(response.ReasonPhrase);
                return null;
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
                Notify(ex.InnerException?.Message);
                return null;
            }
        }

        public async Task<CompanyViewModel> Update(Guid id, CompanyViewModel company)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize<CompanyViewModel>(company), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(APIUrl + "?Id=" + id, content);

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var rawResponse = readTask.GetAwaiter().GetResult();

                    // Deserealize JSON to ViewModel
                    return JsonSerializer.Deserialize<CompanyViewModel>(rawResponse,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                Notify(response.ReasonPhrase);
                return null;
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
                Notify(ex.InnerException?.Message);
                return null;
            }
        }

        public void Dispose()
        {

        }
    }
}