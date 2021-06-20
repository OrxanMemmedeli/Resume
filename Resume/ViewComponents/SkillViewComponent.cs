using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resume.Models.Context;
using Resume.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.ViewComponents
{
    public class SkillViewComponent : ViewComponent
    {
        private readonly ResumeContext db;
        public SkillViewComponent(ResumeContext context)
        {
            db = context;
        }
        public IViewComponentResult Invoke()
        {
            var skills = db.Skills.Include(x => x.SkillCategory);
            return View(skills.ToList());
        }
    }
}
