using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class PortfolioCategory
    {
        public PortfolioCategory()
        {
            this.Portfolios = new HashSet<Portfolio>();
        }

        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Category { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }

        public ICollection<Portfolio> Portfolios { get; set; }
    }
}
