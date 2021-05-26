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
        private readonly SkillAndCategoryViewModel model;
        public SkillViewComponent(ResumeContext context, SkillAndCategoryViewModel _model)
        {
            db = context;
            model = _model;
        }
        public IViewComponentResult Invoke()
        {
            //model.Category = 
            //var skills = db.Skills.Include(c => c.SkillCategory).ToList();
            return View(/*skills*/);
        }
    }
}
