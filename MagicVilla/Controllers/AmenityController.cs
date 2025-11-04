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
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var amenities = _unitOfWork.Amenity.GetAll(includeProperties: "Villa");
            return View(amenities);
        }

        public IActionResult Create()
        {

            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().ToList().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })

            };
           
           
            return View(amenityVM);
        }
        [HttpPost]
        public IActionResult Create(AmenityVM obj)
        {
           
           
            if (ModelState.IsValid )
            {
                _unitOfWork.Amenity.Add(obj.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "The Amenity has been Created Successfully.";
                return RedirectToAction("Index", "Amenity");
            }
           
            obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(obj);


        }

        public IActionResult Update(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)

            };
            if (amenityVM.Amenity is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Update(AmenityVM amenityVM)
        {

            

            if (ModelState.IsValid)
            {
                _unitOfWork.Amenity.Update(amenityVM.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "The Amenity has been Updated Successfully.";
                return RedirectToAction("Index", "Amenity");
            }
           
            amenityVM.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(amenityVM);



        }


        public IActionResult Delete(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)

            };
            if (amenityVM.Amenity is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Delete(AmenityVM amenityVM)
        {

            Amenity? amenityFromDb = _unitOfWork.Amenity.Get(u => u.Id == amenityVM.Amenity.Id);
            if (amenityFromDb is null)
            {
                return RedirectToAction("Error", "Home");
            }

            _unitOfWork.Amenity.Remove(amenityFromDb);
            _unitOfWork.Save();
            TempData["success"] = "The Amenity has been Deleted Successfully.";
            return RedirectToAction("Index", "Amenity");

           



        }
    }
}
