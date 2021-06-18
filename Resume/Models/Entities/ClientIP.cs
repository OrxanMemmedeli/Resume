using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class ClientIP
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(20)")] 
        public string Password { get; set; }
        [Column(TypeName = "varchar(15)")] 
        public string ClientIPAdress { get; set; }
        public DateTime AtempTime { get; set; }
    }
}
