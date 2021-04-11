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
        [Column(TypeName = "nvarchar(max)")]
        public string Category { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string MyBlog { get; set; }

        public ICollection<Blog> Blogs  { get; set; }
    }
}
