using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Services.ContactServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _ContactService;

        public ContactsController(IContactService ContactService)
        {
            _ContactService = ContactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            var values = await _ContactService.GetAllContactAsync();
            return Ok(values);
        }

        [HttpGet("GetContactById/{id}")]
        public async Task<IActionResult> GetContactById(string id)
        {
            var value = await _ContactService.GetContactByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            await _ContactService.CreateContactAsync(createContactDto);
            return Ok("Contact başarıyla eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            await _ContactService.UpdateContactAsync(updateContactDto);
            return Ok("Contact başarıyla güncellendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContact(string id)
        {
            await _ContactService.DeleteContactAsync(id);
            return Ok("Contact başarıyla silindi");
        }
    }
}
