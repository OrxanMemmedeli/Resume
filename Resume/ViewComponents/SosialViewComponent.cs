using Microsoft.AspNetCore.Mvc;
using Resume.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.ViewComponents
{
    public class SosialViewComponent : ViewComponent
    {
        private readonly ResumeContext db;

        public SosialViewComponent(ResumeContext context)
        {
            db = context;
        }

        public IViewComponentResult Invoke()
        {
            var sosial = db.Sosials.ToList();
            return View(sosial);
        }
    }
}
