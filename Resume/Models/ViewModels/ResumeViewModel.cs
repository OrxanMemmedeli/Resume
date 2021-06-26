using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.ViewModels
{
    public class ResumeViewModel
    {
        public Info Info { get; set; }
        public IEnumerable<Education> Educations { get; set; }
        public IEnumerable<Experience> Experiences { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<Sosial> Sosials { get; set; }
        public IEnumerable<Portfolio> Portfolios { get; set; }
    }
}
