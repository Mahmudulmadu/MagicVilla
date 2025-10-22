using MagicVilla.Domain.Entities;
using MagicVilla.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var villas = _db.Villas.ToList();
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
                _db.Villas.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "The Villa has been Created Successfully.";
                return RedirectToAction("Index", "Villa");
            }

            return View(obj);


        }

        public IActionResult Update(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);
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
                _db.Villas.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "The Villa has been Updated Successfully.";
                return RedirectToAction("Index", "Villa");
            }

            return View();


        }


        public IActionResult Delete(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? villaFromDb = _db.Villas.FirstOrDefault(u => u.Id == obj.Id);

            if (villaFromDb is not null)
            {
                _db.Villas.Remove(villaFromDb);
                _db.SaveChanges();
                TempData["success"] = "The Villa has been deleted Successfully.";
                return RedirectToAction("Index", "Villa");
            }
            TempData["error"] = "The Villa isn't Deleted.";
            return View();


        }
    }
}
