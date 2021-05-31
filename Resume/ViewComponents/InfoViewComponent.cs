using Microsoft.AspNetCore.Mvc;
using Resume.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.ViewComponents
{
    public class InfoViewComponent : ViewComponent
    {
        private readonly ResumeContext db;

        public InfoViewComponent(ResumeContext context)
        {
            db = context;
        }
        public IViewComponentResult Invoke()
        {
            var info = db.Infos.Single();
            return View(info);
        }
    }
}
