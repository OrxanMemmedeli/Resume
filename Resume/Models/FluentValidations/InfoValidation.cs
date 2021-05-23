using FluentValidation;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations
{
    public class InfoValidation : AbstractValidator<Info>
    {
        public InfoValidation()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Ad Boş ola bilməz!").NotEmpty().WithMessage("Ad Boş ola bilməz!").MaximumLength(20).WithMessage("Ad 20 simvoldan çox ola bilməz!");
            RuleFor(x => x.Surname).NotNull().WithMessage("Soyad Boş ola bilməz!").NotEmpty().WithMessage("Soyad Boş ola bilməz!").MaximumLength(30).WithMessage("Soyad 30 simvoldan çox ola bilməz!");
            RuleFor(x => x.Speciality).NotNull().WithMessage("İxtisas Boş ola bilməz!").NotEmpty().WithMessage("İxtisas Boş ola bilməz!").MaximumLength(50).WithMessage("İxtisas 50 simvoldan çox ola bilməz!");
            RuleFor(x => x.Email).NotNull().WithMessage("Email Boş ola bilməz!").NotEmpty().WithMessage("Email Boş ola bilməz!").MaximumLength(50).WithMessage("Email 50 simvoldan çox ola bilməz!");
            RuleFor(x => x.Adress).MaximumLength(150).WithMessage("Ünvan 150 simvoldan çox ola bilməz!");
            RuleFor(x => x.Telephone).NotNull().MaximumLength(16).WithMessage("Mobil nömrə 16 simvoldan çox ola bilməz!");
            RuleFor(x => x.Coordinates).NotNull().MaximumLength(50).WithMessage("Mobil nömrə 50 simvoldan çox ola bilməz!");
        }
    }
}
