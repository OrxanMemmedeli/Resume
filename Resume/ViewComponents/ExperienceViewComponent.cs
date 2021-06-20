using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Resume.Models.Context;

namespace Resume.ViewComponents
{
    public class ExperienceViewComponent : ViewComponent
    {
        private readonly ResumeContext db;

        public ExperienceViewComponent(ResumeContext context)
        {
            db = context;
        }

        public IViewComponentResult Invoke()
        {
            var experiences = db.Experiences.OrderBy(x => x.StartDate).ThenBy(x => x.EndDate).ToList();
            return View(experiences);
        }
    }
}
