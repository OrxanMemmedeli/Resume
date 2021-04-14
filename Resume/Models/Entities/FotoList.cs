using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class FotoList
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string FotoURL { get; set; }

        public int PortfolioID { get; set; }
        public Portfolio Portfolio { get; set; }
    }
}
