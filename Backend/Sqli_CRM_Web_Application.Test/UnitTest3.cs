using Microsoft.AspNetCore.Mvc;
using Moq;
using Sqli_CRM_Web_Application.Controllers;
using Sqli_CRM_Web_Application.Models;
using Sqli_CRM_Web_Application.Services;


namespace Sqli_CRM_Web_Application.Test
{
    public class UnitTest3
    {
        private readonly Mock<IContactService> _mockContactService;
        private readonly ContactController _controller;

        public UnitTest3()
        {
            _mockContactService = new Mock<IContactService>();
            _controller = new ContactController(_mockContactService.Object);
        }

        // Test GetContacts action
        [Fact]
        public async Task GetContacts_Returns_OkResult_With_Contacts()
        {
            // Arrange
            var expectedContacts = new List<Contact>
            {
              new Contact()
              {
                 
                        contactId= new Guid("cdcfa450-cb0c-ea11-a813-000d3a1b1223"),
                        fullname = "Christian Coupart",
                        firstname= "Christian",
                        lastname = "Coupart",
                        emailaddress1= "heriberto@northwindtraders.com",
                        jobtitle = "CTO",
                        gendercode = 1,
                        statecode= 0,
                        accountrolecode = null
               
              }
            };
            _mockContactService.Setup(service => service.GetContacts()).ReturnsAsync(expectedContacts);

            // Act
            var result = await _controller.GetContacts();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(expectedContacts, okResult.Value);
        }

       

        [Fact]
        public async Task AddContact_With_InvalidGenderCode_Returns_BadRequest()
        {
            // Arrange
            var contactCreate = new ContactCreateRequest
            {
                gendercode = 3 // Invalid gender code, should be either 1 (Male) or 2 (Female)
            };

            // Act
            var result = await _controller.AddContact(contactCreate);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateContact_With_InvalidGenderCode_Returns_BadRequest()
        {
            // Arrange
            var contactId = Guid.NewGuid();
            var contactUpdate = new ContactUpdateRequest
            {
                gendercode = 3 // Invalid gender code, should be either 1 (Male) or 2 (Female)
            };
            _mockContactService.Setup(service => service.GetContactById(contactId)).ReturnsAsync(new Contact());

            // Act
            var result = await _controller.UpdateContact(contactId, contactUpdate);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.Equal("Gender code is incorrect: 1 => Male | 2 => Female", badRequestResult.Value);
        }





    }
}
