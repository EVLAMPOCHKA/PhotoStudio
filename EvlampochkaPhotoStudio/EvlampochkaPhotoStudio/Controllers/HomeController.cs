using EvlampochkaPhotoStudio.Data;
using EvlampochkaPhotoStudio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EvlampochkaPhotoStudio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EvlampochkaPhotoStudioContext _context;

        public HomeController(ILogger<HomeController> logger, EvlampochkaPhotoStudioContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var info = _context.Contacts.ToList();

            return View(info.FirstOrDefault());
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