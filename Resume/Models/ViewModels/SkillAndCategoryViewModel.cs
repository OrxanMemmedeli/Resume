using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.ViewModels
{
    public class SkillAndCategoryViewModel
    {
        public SkillCategory Category { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
