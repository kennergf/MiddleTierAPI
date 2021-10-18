using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiddleTier.API.Interfaces;
using MiddleTier.API.Response;
using MiddleTier.API.Services;
using MiddleTier.API.ViewModels;
using Moq;
using Xunit;

namespace MiddleTier.Test
{
    public class CompanyServiceTest
    {
        private List<CompanyViewModel> companies;
        private CompanyService companyService;
        private Mock<IAPIRequestService> _APIRequestService;
        public CompanyServiceTest()
        {
            companies = new List<CompanyViewModel>
            {
                new CompanyViewModel{Id = Guid.Parse("55bca715-c209-45c6-8d53-1ab35907ce8a"), Name = "Apple", Exchange = "Nasdaq", Ticker = "0ticker0", ISIN = "US12A1235699", WebSite = "apple.com"}
            };

            _APIRequestService = new Mock<IAPIRequestService>();
            
            _APIRequestService.Setup(x => x.GetById(It.IsIn(Guid.Parse("55bca715-c209-45c6-8d53-1ab35907ce8a"))))
                .Returns(() => Task.FromResult(new CustomResponse<CompanyViewModel>(true, ResponseMessage.COMPANY_RETRIEVED, companies.Where(x => x.Id == Guid.Parse("55bca715-c209-45c6-8d53-1ab35907ce8a")).First())));
            
            _APIRequestService.Setup(x => x.GetByISIN(It.IsIn("US12A1235699")))
                .Returns(() => Task.FromResult(new CustomResponse<CompanyViewModel>(true, ResponseMessage.COMPANY_RETRIEVED, new CompanyViewModel { Id = Guid.Parse("55bca715-c209-45c6-8d53-1ab35907ce8a"), Name = "Apple", Exchange = "Nasdaq", Ticker = "0ticker0", ISIN = "US12A1235699", WebSite = "apple.com" })));
            
            _APIRequestService.Setup(x => x.GetAll())
                .Returns(Task.FromResult(new CustomResponse<IEnumerable<CompanyViewModel>>(true, ResponseMessage.COMPANY_RETRIEVED,(IEnumerable<CompanyViewModel>)companies)));
            
            _APIRequestService.Setup(x => x.Add(It.IsAny<CompanyViewModel>()))
                .Returns(Task.FromResult(new CustomResponse<CompanyViewModel>(true, ResponseMessage.COMPANY_CREATED, null)));
            
            companyService = new CompanyService(new Notifier(), _APIRequestService.Object);

        }

        [Fact]
        public async Task TestGetAllAsync()
        {
            var result = await companyService.GetAll();

            Assert.Equal(companies, result.Data);
        }

        [Fact]
        public async Task TestGetByIdAsync()
        {
            var result = await companyService.GetById(Guid.Parse("55bca715-c209-45c6-8d53-1ab35907ce8a"));

            Assert.Equal(Guid.Parse("55bca715-c209-45c6-8d53-1ab35907ce8a"), result?.Data?.Id);
        }

        [Fact]
        public async Task TestGetByIdNonExistentAsync()
        {
            var result = await companyService.GetById(Guid.Parse("6aed7229-dfb2-496b-ac43-8b79ee634be4"));

            Assert.Null(result);
        }

        [Fact]
        public async Task TestGetByISINAsync()
        {
            var result = await companyService.GetByISIN("US12A1235699");

            Assert.Equal("US12A1235699", result?.Data.ISIN);
        }

        [Fact]
        public async Task TestGetByISINNonExistentAsync()
        {
            var result = await companyService.GetByISIN("BR12A4535689");

            Assert.Null(result);
        }

        [Fact]
        public async Task TestAddNewCompanyAsync()
        {
            var result = await companyService.Add(new API.ViewModels.CompanyViewModel { Id = Guid.Parse("41abb5bd-ab95-4463-b142-c76ff867e81c"), Name = "Microsoft", Exchange = "Nasdaq", Ticker = "0ticker0", ISIN = "US12A1235648", WebSite = "microsoft.com" });

            Assert.True(result.Success);
        }

        [Fact]
        public async Task TestAddExistingISINAsync()
        {
            var result = await companyService.Add(new API.ViewModels.CompanyViewModel { Id = Guid.Parse("454a4850-749f-4daa-a47f-588da83da304"), Name = "Apple", Exchange = "Nasdaq", Ticker = "0ticker0", ISIN = "US12A1235699", WebSite = "apple.com" });

            Assert.False(result.Success);
        }
    }
}
