using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using Zoro.Application.ApplicationConstants;
using Zoro.Application.Contracts.Persistence;
using Zoro.Application.Services;
using Zoro.Domain.ApplicationEnums;
using Zoro.Domain.Models;
using Zoro.Domain.ViewModel;
using Zoro.Infrastructure.Comman;


namespace Zoro.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = CustomRole.MasterAdmin + "," + CustomRole.Admin)]
    public class PostController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _webhostenvironment;

        private readonly IUserNameServices _userNameServices;

        public PostController(IUnitOfWork UnitOfWork, IWebHostEnvironment Webhostenvironment, IUserNameServices userNameServices)
        {
            _unitOfWork = UnitOfWork;

            _webhostenvironment = Webhostenvironment;

            _userNameServices = userNameServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Post> post = await _unitOfWork.Post.GetAllPost();


            return View(post);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> brandlist = _unitOfWork.Brand.Query().Select(x => new SelectListItem
            {
                Text = x.Name.ToUpper(),
                Value= x.Id.ToString()


            }
            
            
            );

            IEnumerable<SelectListItem> VehicleList = _unitOfWork.vehicleTypes.Query().Select(x => new SelectListItem
            {
                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()


            }


           );

            IEnumerable<SelectListItem> bycycleCategories = Enum.GetValues(typeof(BycycleCategories)).Cast<BycycleCategories>().Select(x => new SelectListItem
            {
                Text = x.ToString().ToUpper(),
                Value=((int)x).ToString()



            });

            IEnumerable<SelectListItem> bycycleTypes = Enum.GetValues(typeof(BycycleTypes)).Cast<BycycleTypes>().Select(x => new SelectListItem
            {
                Text = x.ToString().ToUpper(),
                Value = ((int)x).ToString()



            });

            PostVM postvm = new PostVM
            {
                Post = new Post(),
                BrandList = brandlist,
                VehicleTypeList = VehicleList,
                BycycleCategories = bycycleCategories,
                BycycleTypes = bycycleTypes


            };


            return View(postvm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostVM postvm)
        {
            string WebRootPath = _webhostenvironment.WebRootPath;

            var file = HttpContext.Request.Form.Files;

            if (file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();

                var upload = Path.Combine(WebRootPath, @"Images\post");

                var extension = Path.GetExtension(file[0].FileName);

                using (var filestream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(filestream);



                }

                postvm.Post.VehicleImage = @"\Images\post\" + newFileName + extension;

            }

            if (ModelState.IsValid)
            {
                await _unitOfWork.Post.Create(postvm.Post);
                await _unitOfWork.SaveAsync();

                TempData["Created"] = CommanMessage.RecordCreated;


                return RedirectToAction(nameof(Index));
            }


            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            Post post = await _unitOfWork.Post.GetPostById(id);

            post.CreatedBy = await _userNameServices.GetUserName(post.CreatedBy);

            post.ModifiedBy = await _userNameServices.GetUserName(post.ModifiedBy);

            return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            
            Post post = await _unitOfWork.Post.GetPostById(id);

            
            IEnumerable<SelectListItem> brandlist = _unitOfWork.Brand.Query().Select(x => new SelectListItem
            {
                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()
            });

            IEnumerable<SelectListItem> VehicleList = _unitOfWork.vehicleTypes.Query().Select(x => new SelectListItem
            {
                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()
            });

            IEnumerable<SelectListItem> bycycleCategories = Enum.GetValues(typeof(BycycleCategories)).Cast<BycycleCategories>().Select(x => new SelectListItem
            {
                Text = x.ToString().ToUpper(),
                Value = ((int)x).ToString()
            });

            IEnumerable<SelectListItem> bycycleTypes = Enum.GetValues(typeof(BycycleTypes)).Cast<BycycleTypes>().Select(x => new SelectListItem
            {
                Text = x.ToString().ToUpper(),
                Value = ((int)x).ToString()
            });

          
            PostVM postvm = new PostVM
            {
                Post = post,
                BrandList = brandlist,
                VehicleTypeList = VehicleList,
                BycycleCategories = bycycleCategories,
                BycycleTypes = bycycleTypes
            };

            
            return View(postvm);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(PostVM postvm)
        {
            string WebRootPath = _webhostenvironment.WebRootPath;

            var file = HttpContext.Request.Form.Files;

            if (file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(WebRootPath, @"Images\post");

                var extension = Path.GetExtension(file[0].FileName);

                //DELETE OLD IMAGE
                var objfromDb = await _unitOfWork.Post.GeTByIDAsync(postvm.Post.Id);

                if (objfromDb.VehicleImage != null)
                {

                    var oldImagePath = Path.Combine(WebRootPath, objfromDb.VehicleImage.Trim('\\'));


                    if (System.IO.File.Exists(oldImagePath))
                    {

                        System.IO.File.Delete(oldImagePath);
                    }

                }



                using (var filestream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(filestream);


                }

                postvm.Post.VehicleImage = @"\Images\post\" + newFileName + extension;

            }

            if (ModelState.IsValid)
            {



                await _unitOfWork.Post.Update(postvm.Post);
                await _unitOfWork.SaveAsync();

                TempData["Updated"] = CommanMessage.RecordUpdated;
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            Post post = await _unitOfWork.Post.GetPostById(id);

            IEnumerable<SelectListItem> brandlist = _unitOfWork.Brand.Query().Select(x => new SelectListItem
            {
                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()


            }


         );

            IEnumerable<SelectListItem> VehicleList = _unitOfWork.vehicleTypes.Query().Select(x => new SelectListItem
            {
                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()


            }


           );

            IEnumerable<SelectListItem> bycycleCategories = Enum.GetValues(typeof(BycycleCategories)).Cast<BycycleCategories>().Select(x => new SelectListItem
            {
                Text = x.ToString().ToUpper(),
                Value = ((int)x).ToString()



            });

            IEnumerable<SelectListItem> bycycleTypes = Enum.GetValues(typeof(BycycleTypes)).Cast<BycycleTypes>().Select(x => new SelectListItem
            {
                Text = x.ToString().ToUpper(),
                Value = ((int)x).ToString()



            });

            PostVM postvm = new PostVM
            {
                Post = post,
                BrandList = brandlist,
                VehicleTypeList = VehicleList,
                BycycleCategories = bycycleCategories,
                BycycleTypes = bycycleTypes


            };


            return View(postvm);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(PostVM postvm)
        {
            string WebRootPath = _webhostenvironment.WebRootPath;


            if (string.IsNullOrEmpty(postvm.Post.VehicleImage))
            {
                var objFromDB = await _unitOfWork.Post.GeTByIDAsync(postvm.Post.Id);

                if (objFromDB.VehicleImage != null)
                {
                    var oldimagepath = Path.Combine(WebRootPath, objFromDB.VehicleImage.Trim('\\'));

                    if (System.IO.File.Exists(oldimagepath))
                    {

                        System.IO.File.Delete(oldimagepath);
                    }



                }
            }

            await _unitOfWork.Post.Delete(postvm.Post);
            await _unitOfWork.SaveAsync();

            TempData["Deleted"] = CommanMessage.RecordDeleted;
            return RedirectToAction(nameof(Index));
        }

    }
}
