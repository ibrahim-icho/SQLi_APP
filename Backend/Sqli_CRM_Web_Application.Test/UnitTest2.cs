using Microsoft.AspNetCore.Mvc;
using Moq;
using Sqli_CRM_Web_Application.Controllers;
using Sqli_CRM_Web_Application.Models;
using Sqli_CRM_Web_Application.Services;

namespace Sqli_CRM_Web_Application.Test
{
    public class UnitTest2
    {

        private readonly Mock<IAccountService> _mockOpportunitiesService;
        private readonly AccountController _controller;

        public UnitTest2()
        {
            _mockOpportunitiesService = new Mock<IAccountService>();
            _controller = new AccountController(_mockOpportunitiesService.Object);
        }

        [Fact]
        public async Task GetAccounts_Returns_OkResult_With_Accounts()
        {
            // Arrange
            var expectedAccounts = new List<Account>
            {
                new Account()
                {

                    accountid= new Guid("88cea450-cb0c-ea11-a813-000d3a1b1223"),
                    name = "Fabrikam, Inc.",
                    description = "Notre société commerciale publique est active depuis 105 ans. Forts de plus de 90 ans d’expérience dans le secteur du textile, nous portons l’un des noms bénéficiant de la plus grande confiance dans le textile et nous offrons un niveau inégalable de qualité et de sélection de lignes de produits. Nos équipements de production du monde entier nous permettent d’offrir à nos clients tous les textiles dont ils ont besoin.",
                    revenue = 8000000,
                    statecode = 0,
                    opendeals = 1,
                }
            };
            _mockOpportunitiesService.Setup(service => service.GetAccounts()).ReturnsAsync(expectedAccounts);

            // Act
            var result = await _controller.GetAccounts();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(expectedAccounts, okResult.Value);
        }

        [Fact]
        public async Task GetAccountById_Returns_OkResult_With_Account()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var expectedAccount = new Account
            {
                accountid = new Guid("88cea450-cb0c-ea11-a813-000d3a1b1223"),
                name = "Fabrikam, Inc.",
                description = "Notre société commerciale publique est active depuis 105 ans. Forts de plus de 90 ans d’expérience dans le secteur du textile, nous portons l’un des noms bénéficiant de la plus grande confiance dans le textile et nous offrons un niveau inégalable de qualité et de sélection de lignes de produits. Nos équipements de production du monde entier nous permettent d’offrir à nos clients tous les textiles dont ils ont besoin.",
                revenue = 8000000,
                statecode = 0,
                opendeals = 1,

            };

            _mockOpportunitiesService.Setup(service => service.GetAccountById(accountId)).ReturnsAsync(expectedAccount);

            // Act
            var result = await _controller.GetAccountById(accountId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(expectedAccount, okResult.Value);
        }


        [Fact]
        public async Task DeleteAccount_Returns_OkResult_With_IsDeleted_True()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            _mockOpportunitiesService.Setup(service => service.DeleteAccount(accountId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteAccount(accountId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task DeleteAccounts_Returns_OkResult()
        {
            // Arrange
            string[] accountIds = { "88cea450-cb0c-ea11-a813-000d3a1b1223", "a4cea450-cb0c-ea11-a813-000d3a1b1223" };
            _mockOpportunitiesService.Setup(service => service.DeleteAccounts(accountIds)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteAccounts(accountIds);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}

