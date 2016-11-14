using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalizationLocalization.Controllers
{
    public class TestController : Controller
    {

        private readonly IStringLocalizer _localizer;
        private readonly IStringLocalizer _localizer2;
        // GET: /<controller>/

        public TestController(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create(typeof(Resources.Controllers_TestController));
            _localizer2 = factory.Create("Controllers.TestController",null);
        }
        public IActionResult Index()
        {
            ViewData["Message"] = _localizer["Description"] +
                _localizer2["Description"];

            return View();
        }


    }
    public class SharedResource
    {

    }
}
