using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sqli_CRM_Web_Application.Models;
using Sqli_CRM_Web_Application.Services;

namespace Sqli_CRM_Web_Application.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
       // private APIResponse _response;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }


        [HttpGet("GetContacts")]
        public async Task<ActionResult> GetContacts()
        {
            List<Contact> contacts = await _contactService.GetContacts();
            return Ok(contacts);
        }

        [HttpGet("{contactId}")]
        public async Task<ActionResult> GetContactById(Guid contactId)
        {
            Contact contact = await _contactService.GetContactById(contactId);

            if (contact == null)
            {
                return BadRequest(contact);
            }

        
            return Ok(contact);
        }

        [HttpGet("{contactId}")]
        public async Task<IActionResult> GetContactNameById(Guid contactId)
        {
            string contact = await _contactService.GetContactFullnameById(contactId);
            return Ok(contact);
        }

        [HttpGet("GetContactByName/{fullname}")]
        public async Task<IActionResult> GetContactByName(string fullname)
        {
            Contact Contact = await _contactService.GetContactByFullname(fullname);
            return Ok(Contact);
        }

        [HttpPost("AddContact")]
        public async Task<IActionResult> AddContact(ContactCreateRequest contactCreate)
        {
            if (contactCreate.gendercode != 1 && contactCreate.gendercode != 2)
            {
                return BadRequest();
            }

            if (contactCreate.accountrolecode < 1 && contactCreate.accountrolecode > 3)
            {
                return BadRequest("Account role  code in incorrect : 1 => Decision Maker, 2 => Employee, 3 => Influencer");
            }
            var contact = await _contactService.AddContact(contactCreate);
            if (contact == null)
            {
                return BadRequest("Error while creating Contact");
            }

            return Ok(contact);
        }

        [HttpPatch("{contactId}")]
        public async Task<ActionResult> UpdateContact(Guid contactId, [FromBody] ContactUpdateRequest contactUpdate)
        {
            var contact = await _contactService.GetContactById(contactId);

            if (contact == null)
            {
                return BadRequest($"Contact with id = {contactId} not found");
            }

            if (contactUpdate.gendercode != 1 && contactUpdate.gendercode != 2)
            {
                return BadRequest("Gender code is incorrect: 1 => Male | 2 => Female");
            }

            if (contactUpdate.statecode != 0 && contactUpdate.statecode != 1)
            {
                return BadRequest("Status code is incorrect: 1 => Active, 2 => Inactive");
            }

            if (contactUpdate.accountrolecode < 1 || contactUpdate.accountrolecode > 3)
            {
                return BadRequest("Account role code is incorrect: 1 => Decision Maker, 2 => Employee, 3 => Influencer");
            }

            bool isUpdated = await _contactService.UpdateContact(contactId, contactUpdate);

            if (!isUpdated)
            {
                return BadRequest("Error while updating the contact");
            }

            return Ok(contactUpdate);
        }


        [HttpDelete("{contactId}")]
        public async Task<ActionResult> DeleteContact(Guid contactId)
        {
            Contact contact = await _contactService.GetContactById(contactId);

            if (contact == null)
            {
                return BadRequest($"Contact with id = {contactId} doesn't exist");
            }


            bool isDeleted = await _contactService.DeleteContact(contactId);

            if (!isDeleted)
            {
                return BadRequest($"Error while deleting and contact");
            }

            return Ok(contactId);
        }
    }
}
