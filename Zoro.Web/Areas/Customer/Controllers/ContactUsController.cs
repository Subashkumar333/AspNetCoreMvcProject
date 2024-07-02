using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Zoro.Application.Contracts.Persistence;
using Zoro.Domain.Models;

namespace Zoro.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ContactUsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactUsController(IUnitOfWork UnitOfWork )
        {
            _unitOfWork = UnitOfWork;
           
        }
        public IActionResult Index( )
        {



            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(ContactusData model)
        {
            if (ModelState.IsValid)
            {
                var contactusData = new ContactusData
                {
                    YourName = model.YourName,
                    YourEmail = model.YourEmail,
                    YourPhone = model.YourPhone,                
                    YourMessage = model.YourMessage,
                    CreateOn = DateTime.Now 
                };

                await _unitOfWork.ContactUsData.Create(contactusData);
                await _unitOfWork.SaveAsync();

                TempData["MessageSend"] = "Message Send Successfully";

                return RedirectToAction("index");
            }

            return View("Index", model);
        }

    }
}
