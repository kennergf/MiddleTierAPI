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
using MiddleTier.API.ViewModels;

namespace MiddleTier.API.Services
{
    public class CompanyService : BaseService, ICompanyService
    {
        //private static string APIUrl = "https://api.openweathermap.org/data/2.5/weather?q=London,uk&APPID=90506b5793253b8740b4583a6f4386a8";
        private static string APIUrl = "http://localhost:4000/api/v1.0/Companies";

        public CompanyService(INotifier notifier) : base(notifier)
        {
        }

        public async Task<bool> Add(CompanyViewModel company)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(APIUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var content = new StringContent(JsonSerializer.Serialize<CompanyViewModel>(company), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(APIUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var rawResponse = readTask.GetAwaiter().GetResult();
                        Console.WriteLine(rawResponse);
                        return true;
                    }
                    Notify(response.ReasonPhrase);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<CompanyViewModel>> GetAll()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(APIUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(APIUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var rawResponse = readTask.GetAwaiter().GetResult();

                        // Deserealize JSON to ViewModel
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        return JsonSerializer.Deserialize<IEnumerable<CompanyViewModel>>(rawResponse, options);
                    }
                    Notify(response.ReasonPhrase);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
                return null;
            }
        }

        public void Dispose()
        {

        }
    }
}