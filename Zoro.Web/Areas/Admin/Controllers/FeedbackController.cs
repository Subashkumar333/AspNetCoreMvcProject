using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zoro.Application.ApplicationConstants;
using Zoro.Application.Contracts.Persistence;
using Zoro.Domain.Models;
using Zoro.Domain.ViewModel;

namespace Zoro.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = CustomRole.MasterAdmin + "," + CustomRole.Admin)]
    public class FeedbackController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;


        public FeedbackController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            List<ContactusData> contactusDataList = await _unitOfWork.ContactUsData.GetAllMessageAsync();

            return View(contactusDataList);
        }

        [HttpPost]
        public async Task<IActionResult> DeletefromConctactusdata(Guid id)
        {
            var GetDetail = await _unitOfWork.ContactUsData.GeTByIDAsync(id);

            if(GetDetail!= null)
            {
                await _unitOfWork.ContactUsData.Delete(GetDetail);
                await _unitOfWork.SaveAsync();


            }

            TempData["SuccessMessage"] = "Item deleted successfully!";

            return RedirectToAction(nameof(Index));
        }

    }
}
