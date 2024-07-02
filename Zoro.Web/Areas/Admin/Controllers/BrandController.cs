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
using Zoro.Domain.ApplicationEnums;
using Zoro.Domain.Models;
using Zoro.Infrastructure.Comman;


namespace Zoro.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =CustomRole.MasterAdmin +","+ CustomRole.Admin)]
    public class BrandController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _webhostenvironment;

        public BrandController(IUnitOfWork UnitOfWork, IWebHostEnvironment Webhostenvironment)
        {
            _unitOfWork = UnitOfWork;
            _webhostenvironment = Webhostenvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Brand> brand = await _unitOfWork.Brand.GetAllAsync();


            return View(brand);
        }

        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Brand brand)
        {
            string WebRootPath = _webhostenvironment.WebRootPath;

            var file = HttpContext.Request.Form.Files;

            if (file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();

                var upload = Path.Combine(WebRootPath, @"Images\brand");

                var extension = Path.GetExtension(file[0].FileName);

                using (var filestream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(filestream);



                }

                brand.BrandLogo = @"\Images\brand\" + newFileName + extension;

            }

            if (ModelState.IsValid)
            {
                await _unitOfWork.Brand.Create(brand);
                await _unitOfWork.SaveAsync();

                TempData["Created"] = CommanMessage.RecordCreated;


                return RedirectToAction(nameof(Index));
            }


            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            Brand brand = await _unitOfWork.Brand.GeTByIDAsync(id);

            return View(brand);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Brand brand = await _unitOfWork.Brand.GeTByIDAsync(id);

            return View(brand);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Brand brand)
        {
            string WebRootPath = _webhostenvironment.WebRootPath;

            var file = HttpContext.Request.Form.Files;

            if (file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(WebRootPath, @"Images\brand");

                var extension = Path.GetExtension(file[0].FileName);

                //DELETE OLD IMAGE
                var objfromDb = await _unitOfWork.Brand.GeTByIDAsync(brand.Id);

                if (objfromDb.BrandLogo != null)
                {

                    var oldImagePath = Path.Combine(WebRootPath, objfromDb.BrandLogo.Trim('\\'));


                    if (System.IO.File.Exists(oldImagePath))
                    {

                        System.IO.File.Delete(oldImagePath);
                    }

                }



                using (var filestream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(filestream);


                }

                brand.BrandLogo = @"\Images\brand\" + newFileName + extension;

            }

            if (ModelState.IsValid)
            {



                await _unitOfWork.Brand.Update(brand);
                await _unitOfWork.SaveAsync();

                TempData["Updated"] = CommanMessage.RecordUpdated;
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            Brand brand = await _unitOfWork.Brand.GeTByIDAsync(id);

            return View(brand);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Brand brand)
        {
            string WebRootPath = _webhostenvironment.WebRootPath;


            if (string.IsNullOrEmpty(brand.BrandLogo))
            {
                var objFromDB = await _unitOfWork.Brand.GeTByIDAsync(brand.Id);

                if (objFromDB.BrandLogo != null)
                {
                    var oldimagepath = Path.Combine(WebRootPath, objFromDB.BrandLogo.Trim('\\'));

                    if (System.IO.File.Exists(oldimagepath))
                    {

                        System.IO.File.Delete(oldimagepath);
                    }



                }
            }

            await _unitOfWork.Brand.Delete(brand);
            await _unitOfWork.SaveAsync();

            TempData["Deleted"] = CommanMessage.RecordDeleted;
            return RedirectToAction(nameof(Index));
        }

    }
}
