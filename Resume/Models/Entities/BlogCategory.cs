using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class BlogCategory
    {
        public BlogCategory()
        {
            this.Blogs = new HashSet<Blog>();
        }

        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Category { get; set; }

        public ICollection<Blog> Blogs  { get; set; }
    }
}
