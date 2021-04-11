using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class Education
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string EducationCenter { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Faculty { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Specialty { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }
    }
}
