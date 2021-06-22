using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume.Areas.Admin.ViewComponents;
using Resume.Models.Context;

namespace Resume.ViewComponents
{
    public class PortfoliosViewComponent : ViewComponent
    {
        private readonly ResumeContext db;

        public PortfoliosViewComponent(ResumeContext context)
        {
            db = context;
        }

        public IViewComponentResult Invoke()
        {
            PortofiloAndCategories model = new PortofiloAndCategories();
            model.Portfolios = db.Portfolios.Include(x => x.PortfolioCategory).OrderByDescending(x => x.EndDate).ToList().Take(9);
            model.Categories = db.PortfolioCategories.ToList();
            return View(model);
        }
    }
}
