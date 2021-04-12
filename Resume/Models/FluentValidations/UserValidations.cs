using FluentValidation;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations
{
    public class UserValidations : AbstractValidator<User>
    {
        public UserValidations()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Email Boş ola bilməz!").NotEmpty().WithMessage("Email Boş ola bilməz!").MaximumLength(100).WithMessage("Email 100 simvoldan çox ola bilməz!");
            RuleFor(x => x.Password).NotNull().WithMessage("Parol Boş ola bilməz!").NotEmpty().WithMessage("Parol Boş ola bilməz!");
            RuleFor(x => x.ConfirmPassword).NotNull().WithMessage("Təkrar Parol Boş ola bilməz!").NotEmpty().WithMessage("Təkrar Parol Boş ola bilməz!");
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Parol ilə təkrarı arasında uyğunsuzluq var.").When(x => !String.IsNullOrWhiteSpace(x.Password)).WithMessage("Parol ilə təkrarı arasında uyğunsuzluq var.");
        }
    }
}
