using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class Portfolio
    {
        public Portfolio()
        {
            this.fotoLists = new HashSet<FotoList>();
        }

        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime EndDate{ get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Client { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string SiteURL { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Type { get; set; }


        //public int PortfolioCategoryID { get; set; }
        public PortfolioCategory PortfolioCategory { get; set; }
        public ICollection<FotoList> fotoLists { get; set; }
    }
}
