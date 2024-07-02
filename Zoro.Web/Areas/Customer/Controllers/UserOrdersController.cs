using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Zoro.Application.ApplicationConstants;
using Zoro.Application.Contracts.Persistence;
using Zoro.Domain.Models;
using Zoro.Domain.ViewModel;
using Zoro.Infrastructure.Migrations;

namespace Zoro.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = CustomRole.Customer)]
    public class UserOrdersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public UserOrdersController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {

            _unitOfWork = unitOfWork;
            _userManager = userManager;
           
        }



        [HttpGet]
        [Authorize]
        public async Task <IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }


            var orderProducts = await _unitOfWork.OrderProducts.GetByUserIdAsync(userId);

            var viewModel = orderProducts.Select(cp => new OrderProductsVm
            {
                UserID = userId,
                Id = cp.Id.ToString(),
                ProductId = cp.ProductId,
                ProductName = cp.ProductName,
                Price = cp.Price,
                Quantity = cp.Quantity,
                CreateOn = cp.CreateOn,
                UserAddress=cp.UserAddress,
                phoneNumber=cp.PhoneNumber,
                OrderStatus=cp.OrderStatus,
                TotalPrice=cp.TotalPrice,
                Image = cp.Image

            }).ToList();

            return View(viewModel);

           
        }


        public async Task<IActionResult> Deleteorderproduct(string Id)
        {

            var userId = _userManager.GetUserId(User);
            try
            {
                var orderProducts = await _unitOfWork.OrderProducts.GeTByIDAsync(userId, Id);
                if (orderProducts != null)
                {
                    await _unitOfWork.OrderProducts.Delete(orderProducts);
                    await _unitOfWork.SaveAsync();
                    TempData["SuccessMessage"] = "Item deleted successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Item not found!";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            }
            return RedirectToAction(nameof(Index));

             
        }

        [HttpGet]
        public async Task<IActionResult> UserOrderHistory()
        {
            var userId = _userManager.GetUserId(User);

            var CompletedOrder = await _unitOfWork.CompletedOrders.GetAllCartByIdAsync(userId);

            var viewModel = CompletedOrder.Select(cp => new CompletedOrderVM
            {
               
                UserID = userId,
                UserName= cp.UserName,
                OrderId=cp.OrderId,
                Id = cp.Id.ToString(),
                ProductId = cp.ProductId,
                ProductName = cp.ProductName,
                Price = cp.Price,
                Quantity = cp.Quantity,
                CreateOn = cp.CreateOn,            
                PhoneNumber = cp.PhoneNumber,
                OrderStatus = cp.OrderStatus,
                TotalPrice = cp.TotalPrice,
                ModifiedOn= cp.ModifiedOn,
                Image = cp.Image

            }).ToList();

            return View(viewModel);




        }



    }
}
