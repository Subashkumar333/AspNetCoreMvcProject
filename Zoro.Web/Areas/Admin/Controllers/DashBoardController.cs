using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zoro.Application.ApplicationConstants;
using Zoro.Application.Contracts.Persistence;
using Zoro.Domain.ViewModel;
using Zoro.Infrastructure.Comman;

namespace Zoro.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = CustomRole.MasterAdmin + "," + CustomRole.Admin)]
    public class DashBoardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        
        public DashBoardController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public  async Task<IActionResult> Index()
        {
            var totalProduct = await _unitOfWork.OrderProducts.GetTotalOrdersAsync();

            var totalPendingOrder= await _unitOfWork.OrderProducts.GetTotalPendingOrdersAsync();

            var totalReadyToPickOrders = await _unitOfWork.OrderProducts.GetTotalReadyToPickOrdersAsync();

            var totalCompletedOrder = await _unitOfWork.CompletedOrders.GetTotalCompletedOrdersAsync();

            var totalAmount = await _unitOfWork.CompletedOrders.GetTotalAmountAsync();

            var totalFeedBack = await _unitOfWork.ContactUsData.GetFeedBacksCountAsync();

            var totalCustomers = _context.Users.Count();

            var model = new DashboardViewModel
            {
                TotalOrders = totalProduct,

                TotalPendingOrders = totalPendingOrder,
                TotalReadyToPickOrders= totalReadyToPickOrders,
                TotalCompletedOrder= totalCompletedOrder,
                TotalAmount= totalAmount,
                TotalFeedBack= totalFeedBack,
                TotalCustomers= totalCustomers

            };


            return View(model);
        }
    }
}
