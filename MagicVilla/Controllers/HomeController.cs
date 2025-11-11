using System.Diagnostics;
using MagicVilla.Application.Common.Interface;
using MagicVilla.Models;
using Microsoft.AspNetCore.Mvc;


namespace MagicVilla.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
          _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity"),
                CheckInDate = DateOnly.FromDateTime(DateTime.Now),
                CheckOutDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Nights = 1
            };
            return View(homeVM);    
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
