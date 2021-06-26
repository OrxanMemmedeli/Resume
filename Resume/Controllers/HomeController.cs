using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Resume.Business.Control;
using Resume.Business.Tools;
using Resume.Models;
using Resume.Models.Context;
using Resume.Models.ViewModels;
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

        [Route("Portfolio/{header}/{id}")]
        public IActionResult Details(string id)
        {
            if (id == null || IDAncryption.Decrypt(id) == "NotFound")
            {
                return NotFound();
            }
            int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

            var portfolio = db.Portfolios.Find(dID);
            return View(portfolio);
        }

        [Route("Portfolios/{category}/{id}")]
        public IActionResult Category(string id)
        {
            if (id == null || IDAncryption.Decrypt(id) == "NotFound")
            {
                return NotFound();
            }
            int dID = Convert.ToInt32(IDAncryption.Decrypt(id));

            var portfolios = db.Portfolios.Where(x => x.PortfolioCategoryID == dID);
            ViewBag.Category = db.PortfolioCategories.Find(dID).Category;
            return View(portfolios);
        }

        [Route("DownloadCV")]
        public IActionResult DownloadCV()
        {
            ResumeViewModel model = new ResumeViewModel();
            model.Info = db.Infos.SingleOrDefault();
            model.Educations = db.Educations.ToList();
            model.Experiences = db.Experiences.ToList();
            model.Skills = db.Skills.ToList();
            model.Sosials = db.Sosials.ToList();
            model.Portfolios = db.Portfolios.ToList();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
