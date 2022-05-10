﻿using GovUk.Frontend.ExampleApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GovUk.Frontend.ExampleApp.Controllers
{
    public class RadiosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post(RadiosViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Response.StatusCode = 303;
                Response.GetTypedHeaders().Location = new Uri("/panel", UriKind.Relative);
            }

            return View("Index", viewModel);
        }
    }
}
