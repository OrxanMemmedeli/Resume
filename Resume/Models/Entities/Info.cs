using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class Info
    {

        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string Surname { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Speciality { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(16)")]
        public string Telephone { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Adress { get; set; }
        public DateTime BirthDate { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Profil { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string FotoURL { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Coordinates { get; set; }


        [NotMapped]
        public IFormFile Foto { get; set; }
    }
}
