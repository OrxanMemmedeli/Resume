﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Resume.Business.Control;
using Resume.Models;
using Resume.Models.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ResumeContext db;

        public HomeController(ILogger<HomeController> logger, ResumeContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {
            FrontSide frontSide = new FrontSide();
            TempData["FrontList"] = frontSide.Control(db);
            return View();
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var portfolio = db.Portfolios.Find(id);
            return View(portfolio);
        }

        public IActionResult Category(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var portfolios = db.Portfolios.Where(x => x.PortfolioCategoryID == id);
            ViewBag.Category = db.PortfolioCategories.Find(id).Category;
            return View(portfolios);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
