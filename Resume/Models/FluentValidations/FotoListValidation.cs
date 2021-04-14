using FluentValidation;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations
{
    public class FotoListValidation : AbstractValidator<FotoList>
    {
        public FotoListValidation()
        {
            RuleFor(x => x.FotoURL).NotNull().WithMessage("URL Boş ola bilməz!").NotEmpty().WithMessage("URL Boş ola bilməz!");
        }
    }
}
