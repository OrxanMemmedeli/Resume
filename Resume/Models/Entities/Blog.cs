using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class Blog
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Text { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string FotoURL { get; set; }
        public DateTime Datetime { get; set; }


        //public int BlogCategoryID { get; set; }
        public BlogCategory BlogCategory { get; set; }
    }
}
