using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class Skill
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        public int Percent { get; set; }
        //public int SkillCategoryID { get; set; }
        public SkillCategory SkillCategory { get; set; }
    }
}
