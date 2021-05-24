using FluentValidation;
using Resume.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations.ViewModelsValidations
{
    public class ContactViewModelValidation : AbstractValidator<ContactViewModel>
    {
        public ContactViewModelValidation()
        {
            RuleFor(x => x.NameSurname).NotNull().WithMessage("Ad və Soyad sahəsi Boş ola bilməz!").NotEmpty().WithMessage("Ad və Soyad sahəsi Boş ola bilməz!").MaximumLength(60).WithMessage("Ad və Soyad sahəsi 60 simvoldan çox ola bilməz!");
            RuleFor(x => x.Subject).NotNull().WithMessage("Başlıq sahəsi Boş ola bilməz!").NotEmpty().WithMessage("Başlıq sahəsi Boş ola bilməz!").MaximumLength(20).WithMessage("Başlıq sahəsi 20 simvoldan çox ola bilməz!");
            RuleFor(x => x.Email).NotNull().WithMessage("E-mail sahəsi Boş ola bilməz!").NotEmpty().WithMessage("E-mail sahəsi Boş ola bilməz!").MaximumLength(100).WithMessage("E-mail sahəsi 100 simvoldan çox ola bilməz!").EmailAddress().WithMessage("E-mail düzgün daxil edilməmişdir!");
            RuleFor(x => x.Message).NotNull().WithMessage("Mesaj sahəsi Boş ola bilməz!").NotEmpty().WithMessage("Mesaj sahəsi Boş ola bilməz!");
        }
    }
}
