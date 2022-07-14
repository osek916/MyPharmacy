using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Services;

namespace MyPharmacy.Controllers
{
    [Route("api/emailsender")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        private readonly IEmailSenderService _emailSenderService;

        public EmailSenderController(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager, Pharmacist")]
        public ActionResult SendMessageToAllClients()
        {
            var sendMessageDto = _emailSenderService.SendMessageToAllClients();
            return Ok();
        }
        /*
        [HttpPost]
        [Authorize(Roles = "Admin, Manager, Pharmacist")]
        public ActionResult SendMessageToClient()
        {
            var sendMessageDto = _emailSenderService.SendMessageToClient();
            return Ok();
        }*/

    }
}
