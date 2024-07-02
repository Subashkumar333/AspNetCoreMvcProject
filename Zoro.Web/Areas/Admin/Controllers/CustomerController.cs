using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoro.Application.ApplicationConstants;

namespace Zoro.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = CustomRole.MasterAdmin + "," + CustomRole.Admin)]
    public class CustomerController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CustomerController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }



        public async Task<IActionResult> Index()
        {
            var customerRole = await _roleManager.FindByNameAsync("Customer");

            if (customerRole == null)
            {
                return NotFound();
            }

            var customers = await _userManager.GetUsersInRoleAsync(customerRole.Name);

            return View(customers.ToList());
        }
    }
}
