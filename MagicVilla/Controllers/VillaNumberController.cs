using MagicVilla.Application.Common.Interface;
using MagicVilla.Domain.Entities;
using MagicVilla.Infrastructure.Data;
using MagicVilla.Infrastructure.Repository;
using MagicVilla.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var villaNumbers = _unitOfWork.VillaNumber.GetAll(includeProperties: "Villa");
            return View(villaNumbers);
        }

        public IActionResult Create()
        {

            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().ToList().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })

            };
           
           
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {
            bool roomNumberExists = _unitOfWork.VillaNumber.Any(u => u.VillaNo == obj.VillaNumber.VillaNo);
           
            if (ModelState.IsValid && !roomNumberExists)
            {
                _unitOfWork.VillaNumber.Add(obj.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "The Villa Number has been Created Successfully.";
                return RedirectToAction("Index", "VillaNumber");
            }
            if(roomNumberExists)
            {
                TempData["error"] = "The Villa Number already exists.";
            }
            obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(obj);


        }

        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                VillaNumber = _unitOfWork.VillaNumber.Get(u => u.VillaNo == villaNumberId)

            };
            if (villaNumberVM.VillaNumber is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Update(VillaNumberVM villaNumberVM)
        {

            

            if (ModelState.IsValid)
            {
                _unitOfWork.VillaNumber.Update(villaNumberVM.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "The Villa Number has been Updated Successfully.";
                return RedirectToAction("Index", "VillaNumber");
            }
           
            villaNumberVM.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(villaNumberVM);



        }


        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                VillaNumber = _unitOfWork.VillaNumber.Get(u => u.VillaNo == villaNumberId)

            };
            if (villaNumberVM.VillaNumber is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Delete(VillaNumberVM villaNumberVM)
        {

            VillaNumber? villaNumberFromDb = _unitOfWork.VillaNumber.Get(u => u.VillaNo == villaNumberVM.VillaNumber.VillaNo);
            if (villaNumberFromDb is null)
            {
                return RedirectToAction("Error", "Home");
            }

            _unitOfWork.VillaNumber.Remove(villaNumberFromDb);
            _unitOfWork.Save();
            TempData["success"] = "The Villa Number has been Deleted Successfully.";
            return RedirectToAction("Index", "VillaNumber");

           



        }
    }
}
