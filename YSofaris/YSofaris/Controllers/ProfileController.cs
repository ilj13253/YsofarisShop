﻿using Microsoft.AspNetCore.Mvc;

namespace YSofaris.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
