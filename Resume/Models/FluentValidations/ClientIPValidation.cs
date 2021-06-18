using FluentValidation;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations
{
    public class ClientIPValidation : AbstractValidator<ClientIP>
    {
        public ClientIPValidation()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Email Boş ola bilməz!").NotEmpty().WithMessage("Email Boş ola bilməz!").MaximumLength(100).WithMessage("Email 100 simvoldan çox ola bilməz!");
            RuleFor(x => x.Password).NotNull().WithMessage("Şifrə Boş ola bilməz!").NotEmpty().WithMessage("Şifrə Boş ola bilməz!").MinimumLength(6).WithMessage("Şifrə minimum 6 simvol ola bilər.").MaximumLength(20).WithMessage("Parol maksimum 20 simvol ola bilər.");
            RuleFor(x => x.ClientIPAdress).NotNull().WithMessage("IP adressi Boş ola bilməz!").NotEmpty().WithMessage("IP adressi Boş ola bilməz!").MaximumLength(15).WithMessage("IP adressi maksimum 15 simvol ola bilər.");
        }
    }
}
