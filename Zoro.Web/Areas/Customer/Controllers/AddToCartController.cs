using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using Zoro.Application.ApplicationConstants;
using Zoro.Application.Contracts.Persistence;
using Zoro.Application.Services;
using Zoro.Domain.Models;
using Zoro.Domain.ViewModel;


namespace Zoro.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = CustomRole.Customer)]
    public class AddToCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AddToCartController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {

            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var cartProducts = await _unitOfWork.cartProducts.GetAllCartByIdAsync(userId);
            var viewModel = cartProducts.Select(cp => new CartProductVM
            {
                UserID = userId,
                Id = cp.Id.ToString(),
                ProductId = cp.ProductId,
                ProductName = cp.ProductName,
                Price = cp.Price,
                Quantity = cp.Quantity,
                CreateOn = cp.CreateOn,
                CreatedBy = cp.CreatedBy,
                Image = cp.Image

            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderProductFromAddtocard(CartProductVM cartProductVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid model state";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var user = await _userManager.GetUserAsync(User);

                if (string.IsNullOrEmpty(user.PhoneNumber))
                {
                    TempData["ErrorMessage"] = "Phone number is required. Please update your profile.";
                    return RedirectToPage("/Account/Manage/Index", new { area = "Identity" });
                }

                var orderProduct = new OrderProducts
                {
                    Id = Guid.NewGuid(),
                    UserID = user.Id,
                    UserName = user.UserName,
                    ProductId = cartProductVM.ProductId,
                    ProductName = cartProductVM.ProductName,
                    Price = cartProductVM.Price,
                    Quantity = cartProductVM.Quantity,
                    Image = cartProductVM.Image,
                    OrderStatus = "Pending",
                    PhoneNumber = user.PhoneNumber, // Placeholder for real phone number
                    UserAddress = "null", // Placeholder for real address
                    TotalPrice = cartProductVM.Price * cartProductVM.Quantity,
                    CreateOn = DateTime.Now
                };

                await _unitOfWork.OrderProducts.Create(orderProduct);

                var cartProduct = await _unitOfWork.cartProducts.GeTByIDAsync(user.Id, cartProductVM.Id);
                if (cartProduct != null)
                {
                    await _unitOfWork.cartProducts.Delete(cartProduct);
                    await _unitOfWork.SaveAsync();
                }

                TempData["SuccessMessage"] = "Order created successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteFromAddtocart(string Id)
        {
            var userId = _userManager.GetUserId(User);
            try
            {
                var cartProduct = await _unitOfWork.cartProducts.GeTByIDAsync(userId, Id);
                if (cartProduct != null)
                {
                    await _unitOfWork.cartProducts.Delete(cartProduct);
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
        [Route("GetCartItemCount")]
        public async Task<IActionResult> GetCartItemCount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Json(new { count = 0 });
            }

            var cartProducts = await _unitOfWork.cartProducts.GetAllCartByIdAsync(userId);
            int cartItemCount = cartProducts.Count();

            return Json(new { success = true, count = cartItemCount });
        }






    }
}

       
    
