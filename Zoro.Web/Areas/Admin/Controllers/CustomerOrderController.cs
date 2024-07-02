using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zoro.Application.ApplicationConstants;
using Zoro.Application.Contracts.Persistence;
using Zoro.Domain.ApplicationEnums;
using Zoro.Domain.Models;
using Zoro.Domain.ViewModel;
using CompletedOrders = Zoro.Domain.Models.CompletedOrders;

namespace Zoro.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = CustomRole.MasterAdmin + "," + CustomRole.Admin)]
    public class CustomerOrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

       
        public CustomerOrderController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orderProducts = await _unitOfWork.OrderProducts.GetAllAsync();
            return View(orderProducts);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var orderProducts = await _unitOfWork.OrderProducts.GeTByIDAsync(id);

            var orderStatus = Enum.GetValues(typeof(Orderstatus))
                                  .Cast<Orderstatus>()
                                  .Select(x => new SelectListItem
                                  {
                                      Text = x.ToString().Replace("_", " "),
                                      Value = x.ToString()
                                  });

            var customerOrdersVm = new CustomerOrdersVm
            {
                orderProducts = orderProducts,
                Orderstatus = orderStatus
            };

            return View(customerOrdersVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CustomerOrdersVm customerOrdersVm)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.OrderProducts.Update(customerOrdersVm.orderProducts);
                await _unitOfWork.SaveAsync();

                TempData["Updated"] = CommanMessage.RecordUpdated;
                return RedirectToAction(nameof(Index));
            }

            customerOrdersVm.Orderstatus = Enum.GetValues(typeof(Orderstatus))
                                               .Cast<Orderstatus>()
                                               .Select(x => new SelectListItem
                                               {
                                                   Text = x.ToString().Replace("_", " "),
                                                   Value = x.ToString()
                                               });

            return View(customerOrdersVm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompletedOrdersFromOrderProduct(Guid id)
        {
            var orderProduct = await _unitOfWork.OrderProducts.GeTByIDAsync(id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            var completedOrder = new CompletedOrders
            {
                Id = Guid.NewGuid(),
                OrderId = orderProduct.Id.ToString(),
                UserID = orderProduct.UserID,
                UserName = orderProduct.UserName,
                ProductId = orderProduct.ProductId,
                ProductName = orderProduct.ProductName,
                Price = orderProduct.Price,
                Quantity = orderProduct.Quantity,
                Image = orderProduct.Image,
                OrderStatus = "Delivered / Payment completed",
                PhoneNumber = orderProduct.PhoneNumber,
                UserAddress = orderProduct.UserAddress,
                TotalPrice = orderProduct.TotalPrice,
                CreateOn = orderProduct.CreateOn,
                CreatedBy = orderProduct.CreatedBy,
                ModifiedOn = DateTime.UtcNow,
                ModifiedBy = orderProduct.ModifiedBy
            };

            await _unitOfWork.CompletedOrders.Create(completedOrder);
            await _unitOfWork.OrderProducts.Delete(orderProduct);
            await _unitOfWork.SaveAsync();

            TempData["Completed"] = "Order has been completed and moved to Completed Orders.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id:guid}")]
        public async Task<IActionResult> Deleteorderproduct(Guid id)
        {
            try
            {
                var orderProduct = await _unitOfWork.OrderProducts.GeTByIDAsync(id);
                if (orderProduct != null)
                {
                    await _unitOfWork.OrderProducts.Delete(orderProduct);
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
    }
}
