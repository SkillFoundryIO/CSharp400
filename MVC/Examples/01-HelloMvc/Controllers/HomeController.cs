﻿using Microsoft.AspNetCore.Mvc;

namespace HelloMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
