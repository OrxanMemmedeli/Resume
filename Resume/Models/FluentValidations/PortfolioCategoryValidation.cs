using FluentValidation;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations
{
    public class PortfolioCategoryValidation : AbstractValidator<PortfolioCategory>
    {
        public PortfolioCategoryValidation()
        {
            RuleFor(x => x.Category).NotNull().WithMessage("Kateqoriya adı Boş ola bilməz!").NotEmpty().WithMessage("Kateqoriya adı  Boş ola bilməz!").MaximumLength(50).WithMessage("Kateqoriya adı 50 simvoldan çox ola bilməz!");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Açıqlama 500 simvoldan çox ola bilməz!");
        }
    }
}
