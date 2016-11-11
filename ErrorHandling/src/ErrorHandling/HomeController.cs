using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ErrorHandling
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        [Route("/Error")]
        public string Index()
        {
            return "Customized error page.";

        }
    }
}
