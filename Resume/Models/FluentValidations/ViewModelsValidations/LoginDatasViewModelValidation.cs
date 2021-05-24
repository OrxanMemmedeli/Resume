using FluentValidation;
using Resume.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations.ViewModelsValidations
{
    public class LoginDatasViewModelValidation : AbstractValidator<LoginDatasViewModel>
    {
        public LoginDatasViewModelValidation()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("E-mail sahəsi Boş ola bilməz!").NotEmpty().WithMessage("E-mail sahəsi Boş ola bilməz!").MaximumLength(100).WithMessage("E-mail sahəsi 100 simvoldan çox ola bilməz!").EmailAddress().WithMessage("E-mail düzgün daxil edilməmişdir!");
            RuleFor(x => x.Password).NotNull().WithMessage("Şifrə sahəsi Boş ola bilməz!").NotEmpty().WithMessage("Şifrə sahəsi Boş ola bilməz!").MinimumLength(6).WithMessage("Şifrə minimum 6 simvol ola bilər.").MaximumLength(20).WithMessage("Şifrə maksimum 20 simvol ola bilər.");
        }
    }
}
