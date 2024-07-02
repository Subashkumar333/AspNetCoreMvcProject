using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using Zoro.Application.Contracts.Persistence;
using Zoro.Application.TempDataExtension;
using Zoro.Domain.Models;
using Zoro.Domain.ViewModel;
using Zoro.Infrastructure.Migrations;
using System.Security.Claims;
using Post = Zoro.Domain.Models.Post;
using Zoro.Application.ApplicationConstants;


namespace Zoro.Web.Areas.Customer.Controllers
{
    [Area("Customer")]

   

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUnitOfWork _unitOfWork;


        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int? page, bool resetFilter = false)

        {

            IEnumerable<SelectListItem> brandList = _unitOfWork.Brand.Query().Select(x => new SelectListItem
            {
                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()
            });

            IEnumerable<SelectListItem> vehicleTypeList = _unitOfWork.vehicleTypes.Query().Select(x => new SelectListItem
            {
                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()
            });

            List<Post> posts;

            if (resetFilter)
            {
                TempData.Remove("FilteredPosts");
                TempData.Remove("SelectedBrandId");
                TempData.Remove("SelectedVehicleTypeId");
            }

            if (TempData.ContainsKey("FilteredPosts"))
            {
                posts = TempData.Get<List<Post>>("FilteredPosts");
                TempData.Keep("FilteredPosts");
            }
            else
            {
                posts = await _unitOfWork.Post.GetAllPost();
            }

            int pageSize = 6;

            int pageNumber = page ?? 1;

            int totalItems = posts.Count;

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;

            var pagedPosts = posts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            HttpContext.Session.SetString("PreviousUrl", HttpContext.Request.Path);

            HomePostVM homePostVM = new HomePostVM
            {
                Posts = pagedPosts,
                BrandList = brandList,
                VehicleTypeList = vehicleTypeList,
                BrandId = (Guid?)TempData["SelectedBrandId"],
                vehicleTypesId = (Guid?)TempData["SelectedVehicleTypeId"]
            };

            return View(homePostVM);




        }

        
        [HttpPost]
        public async Task<IActionResult> Index(HomePostVM homePostVM)
        {

            var posts = await _unitOfWork.Post.GetAllPost(homePostVM.searchBox, homePostVM.BrandId, homePostVM.vehicleTypesId);

            TempData.Put("FilteredPosts", posts);

            TempData["SelectedBrandId"] = homePostVM.BrandId;

            TempData["SelectedVehicleTypeId"] = homePostVM.vehicleTypesId;


            return RedirectToAction("Index", new { page = 1, resetFilter = false });

        }

        [Authorize]
        public async Task<IActionResult> Details(Guid id, int? page)
        {
            try
            {

                Post post = await _unitOfWork.Post.GetPostById(id);


                List<Post> posts = new List<Post>();


                if (post != null)
                {

                    posts = await _unitOfWork.Post.GetAllPost(post.Id, post.BrandId);
                }


                ViewBag.CurrentPage = page;


                CustomerDetailsVM customerDetailsVM = new CustomerDetailsVM
                {
                    Post = post,
                    Posts = posts
                };


                return View(customerDetailsVM);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred in Details action: {Message}", ex.Message);
                throw;
            }
        }

        public IActionResult GoBack(int? page)
        {

            string? previousUrl = HttpContext.Session.GetString("PreviousUrl");

            if (!string.IsNullOrEmpty(previousUrl))
            {

                if (page.HasValue)
                {
                    previousUrl = QueryHelpers.AddQueryString(previousUrl, "page", page.Value.ToString());
                }


                HttpContext.Session.Remove("PreviousUrl");


                return Redirect(previousUrl);
            }
            else
            {

                return RedirectToAction("Index");
            }
        }
        
        [HttpGet("IsItemInCart")]
        public async Task<IActionResult> IsItemInCart(string productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Json(false);
            }

            var isInCart = await _unitOfWork.cartProducts.CheckIfItemAddedToCart(userId, productId);
            return Json(isInCart);
        }



       
        [HttpPost("CreateCartProductsFromPosts")]
        public async Task<IActionResult> CreateCartProductsFromPosts(string productId, string productName, double price, int quantity,string image ,int? page)
        {
            var userName = User.Identity.Name;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {

                var cartProduct = new Domain.Models.CarTproducts
                {
                    UserID = userId,
                    UserName = userName,
                    ProductId = productId,
                    ProductName = productName,
                    Price = price,
                    Quantity = quantity,
                    Image = image
                };

                await _unitOfWork.cartProducts.Create(cartProduct);
                await _unitOfWork.SaveAsync();

               

            }
            else
            {
                TempData["LoginDisclaimer"] = "You need to login to add products to the cart.";
            }

            

            return RedirectToAction("Index", new { page = page });

           
        }






        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
} 
