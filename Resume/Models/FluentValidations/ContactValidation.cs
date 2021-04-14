using FluentValidation;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations
{
    public class ContactValidation : AbstractValidator<Contact>
    {
        public ContactValidation()
        {
            RuleFor(x => x.NameSurname).NotNull().WithMessage("Ad və Soyad Boş ola bilməz!").NotEmpty().WithMessage("Ad və Soyad Boş ola bilməz!").MaximumLength(60).WithMessage("Ad və Soyad 60 simvoldan çox ola bilməz!");
            RuleFor(x => x.Subject).NotNull().WithMessage("Başlıq Boş ola bilməz!").NotEmpty().WithMessage("Başlıq Boş ola bilməz!").MaximumLength(20).WithMessage("Başlıq 20 simvoldan çox ola bilməz!");
            RuleFor(x => x.Email).NotNull().WithMessage("Email Boş ola bilməz!").NotEmpty().WithMessage("Email Boş ola bilməz!").MaximumLength(150).WithMessage("Email 150 simvoldan çox ola bilməz!");
            RuleFor(x => x.Message).NotNull().WithMessage("Mesaj mətni Boş ola bilməz!").NotEmpty().WithMessage("Mesaj mətni Boş ola bilməz!");
        }
    }
}
