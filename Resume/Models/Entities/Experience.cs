using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class Experience
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Position { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }

        public bool ShowYear { get; set; } = true;
        public bool ShowDay { get; set; } = true;
        public bool ShowMonth { get; set; } = true;
        public bool Present { get; set; } = false;
    }
}
