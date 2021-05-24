using Resume.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Entities
{
    public class Contact
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(60)")]
        public string NameSurname { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Subject { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime ResponseDate { get; set; }

        public static implicit operator Contact(ContactViewModel model)
        {
            return new Contact
            {
                NameSurname = model.NameSurname,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message,
            };
        }

        public static implicit operator ContactViewModel(Contact model)
        {
            return new ContactViewModel
            {
                NameSurname = model.NameSurname,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message,
            };
        }
    }
}
