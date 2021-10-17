using System;
using System.Threading.Tasks;
using MiddleTier.API.Interfaces;
using MiddleTier.API.Response;
using MiddleTier.API.Services;
using Moq;
using Xunit;

namespace MiddleTier.Test
{
    public class CompanyServiceTest
    {
        private CompanyService companyService;
        private Mock<IAPIRequestService> _APIRequestService;
        public CompanyServiceTest()
        {
            _APIRequestService = new Mock<IAPIRequestService>();
            companyService = new CompanyService(new Notifier(), _APIRequestService.Object);
            
        }

        [Fact]
        public async Task TestAddNewCompanyAsync()
        {
            var result = await companyService.Add(new API.ViewModels.CompanyViewModel{Name = "Microsoft", Exchange = "Nasdaq", Ticker = "0ticker0", ISIN = "US12A1235648", WebSite = "microsoft.com"});

            Assert.False(result);
        }

        [Fact]
        public async Task TestAddExistingISINAsync()
        {
            var result = await companyService.Add(new API.ViewModels.CompanyViewModel{Name = "Apple", Exchange = "Nasdaq", Ticker = "0ticker0", ISIN = "US12A1235699", WebSite = "apple.com"});

            Assert.False(result);
        }
    }
}
