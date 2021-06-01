using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class Sosial
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Icon { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string PageURL { get; set; }
    }
}
