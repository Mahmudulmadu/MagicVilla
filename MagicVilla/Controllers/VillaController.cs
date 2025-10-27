using MagicVilla.Application.Common.Interface;
using MagicVilla.Domain.Entities;
using MagicVilla.Infrastructure.Data;
using MagicVilla.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var villas = _unitOfWork.Villa.GetAll();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if(obj.Name == obj.Details)
            {
                ModelState.AddModelError("details", "The Description can't exactly match the Name");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Villa.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "The Villa has been Created Successfully.";
                return RedirectToAction("Index", "Villa");
            }

            return View(obj);


        }

        public IActionResult Update(int villaId)
        {
            Villa? obj = _unitOfWork.Villa.Get(u => u.Id == villaId);
            if(obj==null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            

            if (ModelState.IsValid && obj.Id>0)
            {
                _unitOfWork.Villa.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "The Villa has been Updated Successfully.";
                return RedirectToAction("Index", "Villa");
            }

            return View();


        }


        public IActionResult Delete(int villaId)
        {
            Villa? obj = _unitOfWork.Villa.Get(u => u.Id == villaId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? villaFromDb = _unitOfWork.Villa.Get(u => u.Id == obj.Id);

            if (villaFromDb is not null)
            {
                _unitOfWork.Villa.Remove(villaFromDb);
                _unitOfWork.Save();
                TempData["success"] = "The Villa has been deleted Successfully.";
                return RedirectToAction("Index", "Villa");
            }
            TempData["error"] = "The Villa isn't Deleted.";
            return View();


        }
    }
}
