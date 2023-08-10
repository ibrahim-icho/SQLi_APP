using Moq;
using Microsoft.AspNetCore.Mvc;
using Sqli_CRM_Web_Application.Controllers;
using Sqli_CRM_Web_Application.Models;
using Sqli_CRM_Web_Application.Services;

namespace Sqli_CRM_Web_Application.Test
{
    public class UnitTest1
    {
        private readonly Mock<IOpportunitiesService> _mockOpportunitiesService;
        private readonly OpportunitiesController _controller;
        private readonly Mock<ICurrencyService> _mockcurrencyService;

        public UnitTest1()
        {
            _mockOpportunitiesService = new Mock<IOpportunitiesService>();
            _mockcurrencyService = new Mock<ICurrencyService>();
            _controller = new OpportunitiesController(_mockOpportunitiesService.Object, _mockcurrencyService.Object);
        }

        /*
        [Fact]
        public async Task GetAllOpportunities_Returns_OkResult_With_Opportunities()
        {
            // Arrange
            var expectedOpportunities = new List<Opportunity>
            {
                // Sample Opportunity data
                new Opportunity
                {
                    opportunityid = new Guid("e90a0493-e8f0-ea11-a815-000d3a1b14a2"),
                    name = "10 machines à café Airpot XL pour Alpine Ski House",
                    description = "Ajout de machines à café au siège social",
                    estimatedvalue = 4990,
                    closeprobability = 65,
                    salesstagecode = 1,
                    currentsituation = "Il n’y a pas assez de machines à café pour répondre à la demande.",
                    purchasetimeframe = 4
                },
                new Opportunity
                {
                    opportunityid = new Guid("b052fc98-e8f0-ea11-a815-000d3a1b14a2"),
                    name = "18 machines à café Airpot pour Northwind Traders",
                    description = "Achat de nouvelles machines pour les bureaux",
                    estimatedvalue = 30582,
                    closeprobability = 93,
                    salesstagecode = 1,
                    currentsituation = "Les établissements n’ont aucune machine expresso.",
                    purchasetimeframe = 1
                }
            };
            _mockOpportunitiesService.Setup(service => service.GetAllOpportunities())
                .ReturnsAsync(expectedOpportunities);

            // Act
            var result = await _controller.GetAllOpportunities();

            // Assert
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(expectedOpportunities, okResult.Value);

        }*/

        [Fact]
        public async Task GetAllOpportunities_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            _mockOpportunitiesService.Setup(service => service.GetAllOpportunities())
                .ThrowsAsync(new Exception($"Failed to retrieve opportunities"));

            // Act
            var result = await _controller.GetAllOpportunities();

            // Assert
            Assert.IsType<ObjectResult>(result); // Expecting an ObjectResult
            var objectResult = (ObjectResult)result;
            Assert.Equal(500, objectResult.StatusCode); // Verify the status code
        }

        [Fact]
        public async Task GetAllOpportunities_Returns_EmptyList()
        {
            // Arrange
            var expectedOpportunities = new List<Opportunity>(); // Empty list of opportunities
            _mockOpportunitiesService.Setup(service => service.GetAllOpportunities())
                .ReturnsAsync(expectedOpportunities);

            // Act
            var result = await _controller.GetAllOpportunities();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Empty((IEnumerable<Opportunity>)okResult.Value);
        }

        /*
        [Fact]
        public async Task GetOpportunitiById_Returns_OkResult_withOpportunity()
        {
            // Arrange
            Opportunity exposedOpportunity = new Opportunity()
            {
                opportunityid = new Guid("e90a0493-e8f0-ea11-a815-000d3a1b14a2"),
                name = "10 machines à café Airpot XL pour Alpine Ski House",
                description = "Ajout de machines à café au siège social",
                estimatedvalue = 4990,
                closeprobability = 65,
                salesstagecode = 1,
                currentsituation = "Il n’y a pas assez de machines à café pour répondre à la demande.",
                purchasetimeframe = 4
            };
            var targetOpportunityId = new Guid("e90a0493-e8f0-ea11-a815-000d3a1b14a2");

            _mockOpportunitiesService.Setup(service => service.GetOpportunityByID(targetOpportunityId)).ReturnsAsync(exposedOpportunity);

            // Act
            var result = await _controller.GetOpportunityById(targetOpportunityId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(exposedOpportunity, okResult.Value);
        }*/

        [Fact]
        public async Task GetOpportunitiById_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            var targetOpportunityId = new Guid("e90a0493-e8f0-ea11-a815-000d3a1b14a2");
            _mockOpportunitiesService.Setup(service => service.GetOpportunityByID(targetOpportunityId))
                .ThrowsAsync(new Exception($"Failed to retrieve opportunity"));

            // Act
            var result = await _controller.GetOpportunityById(targetOpportunityId);

            // Assert
            Assert.IsType<ObjectResult>(result); // Expecting an ObjectResult
            var objectResult = (ObjectResult)result;
            Assert.Equal(500, objectResult.StatusCode); // Verify the status code
        }

      /*  [Fact]
        public async Task GetOpportunitiesByAccountId_Returns_OkResult_With_Opportunities()
        {
            // Arrange
            Guid accountId = Guid.NewGuid(); // Replace with the appropriate test accountId
            var expectedOpportunities = new List<Opportunity>
            {
                new Opportunity()
                {
                     opportunityid = new Guid("e90a0493-e8f0-ea11-a815-000d3a1b14a2"),
                    name = "10 machines à café Airpot XL pour Alpine Ski House",
                    description = "Ajout de machines à café au siège social",
                    estimatedvalue = 4990,
                    closeprobability = 65,
                    salesstagecode = 1,
                    currentsituation = "Il n’y a pas assez de machines à café pour répondre à la demande.",
                    purchasetimeframe = 4
                }
            };

            _mockOpportunitiesService.Setup(service => service.GetOpportunitiesByAccountId(accountId))
                .ReturnsAsync(expectedOpportunities);

            // Act
            IActionResult result = await _controller.GetOpportunitiesByAccountId(accountId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.Equal(expectedOpportunities, okResult.Value);
        }*/


        [Fact]
        public async Task GetOpportunitiesByAccountId_Returns_NotFound_When_NoOpportunitiesFound()
        {
            // Arrange
            Guid accountId = Guid.NewGuid(); // Replace with the appropriate test accountId

            _mockOpportunitiesService.Setup(service => service.GetOpportunitiesByAccountId(accountId))
                .ReturnsAsync((List<Opportunity>)null); // Simulate no opportunities found

            // Act
            IActionResult result = await _controller.GetOpportunitiesByAccountId(accountId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetOpportunitiesByAccountId_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            Guid accountId = Guid.NewGuid(); // Replace with the appropriate test accountId

            _mockOpportunitiesService.Setup(service => service.GetOpportunitiesByAccountId(accountId))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            IActionResult result = await _controller.GetOpportunitiesByAccountId(accountId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);

            var objectResult = result as ObjectResult;
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal("An error occurred: Test exception", objectResult.Value);
        }





    }




}
