using Microsoft.AspNetCore.Mvc;
using Resume.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.ViewComponents
{
    public class EducationViewComponent : ViewComponent
    {
        private readonly ResumeContext db;

        public EducationViewComponent(ResumeContext context)
        {
            db = context;
        }

        public IViewComponentResult Invoke()
        {
            var educations = db.Educations.OrderBy(x => x.StartDate).ThenBy(x => x.EndDate).ToList();
            return View(educations);
        }
    }
}
