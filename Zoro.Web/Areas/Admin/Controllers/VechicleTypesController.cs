using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using Zoro.Application.ApplicationConstants;
using Zoro.Application.Contracts.Persistence;
using Zoro.Domain.Models;
using Zoro.Infrastructure.Comman;
using Zoro.Infrastructure.Migrations;
using VechicleTypes = Zoro.Domain.Models.VechicleTypes;


namespace Zoro.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = CustomRole.MasterAdmin + "," + CustomRole.Admin)]
    public class VechicleTypesController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _webhostenvironment;

        public VechicleTypesController(IUnitOfWork UnitOfWork, IWebHostEnvironment Webhostenvironment)
        {
            _unitOfWork = UnitOfWork;
            _webhostenvironment = Webhostenvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<VechicleTypes> vechicleTypes = await _unitOfWork.vehicleTypes.GetAllAsync();


            return View(vechicleTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {



            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VechicleTypes vechicleTypes)
        {
           

            if (ModelState.IsValid)
            {
                await _unitOfWork.vehicleTypes.Create(vechicleTypes);
                await _unitOfWork.SaveAsync();

                TempData["Created"] = CommanMessage.RecordCreated;


                return RedirectToAction(nameof(Index));
            }


            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            VechicleTypes vechicleTypes = await _unitOfWork.vehicleTypes.GeTByIDAsync(id);

            return View(vechicleTypes);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            VechicleTypes vechicleTypes = await _unitOfWork.vehicleTypes.GeTByIDAsync(id);

            return View(vechicleTypes);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(VechicleTypes vechicleTypes)
        {
           

            if (ModelState.IsValid)
            {



                await _unitOfWork.vehicleTypes.Update(vechicleTypes);
                await _unitOfWork.SaveAsync();

                TempData["Updated"] = CommanMessage.RecordUpdated;
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            VechicleTypes vechicleTypes = await _unitOfWork.vehicleTypes.GeTByIDAsync(id);

            return View(vechicleTypes);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(VechicleTypes vechicleTypes)
        {
            

            await _unitOfWork.vehicleTypes.Delete(vechicleTypes);
            await _unitOfWork.SaveAsync();

            TempData["Deleted"] = CommanMessage.RecordDeleted;
            return RedirectToAction(nameof(Index));
        }

    }
}
