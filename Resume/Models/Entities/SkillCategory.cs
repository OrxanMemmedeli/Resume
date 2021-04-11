using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class SkillCategory
    {
        public SkillCategory()
        {
            this.Skills = new HashSet<Skill>();
        }

        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Category { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }

        public ICollection<Skill> Skills { get; set; }
    }
}
