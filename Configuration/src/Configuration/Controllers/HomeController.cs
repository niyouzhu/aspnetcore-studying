using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Configuration.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<MyOptions> _optionsAccessor;

        public HomeController(IOptions<MyOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
        }

        public IActionResult Index()
        {
            return View(_optionsAccessor.Value);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
