﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GlobalizationLocalization.Controllers
{
    public class BookController : Controller
    {
        // GET: /<controller>/
        public IActionResult Hello(string name)
        {
            ViewBag.Message = _localizer["<b>Hello</b> <i>{0}</i>", name];
            return View();
        }

        private readonly IHtmlLocalizer<BookController> _localizer;

        public BookController(IHtmlLocalizer<BookController> localizer)
        {
            _localizer = localizer;
        }


    }
}
