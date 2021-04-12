using FluentValidation;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations
{
    public class PortfolioValidations : AbstractValidator<Portfolio>
    {
        public PortfolioValidations()
        {
            RuleFor(x => x.Title).NotNull().WithMessage("Başlıq Boş ola bilməz!").NotEmpty().WithMessage("Başlıq Boş ola bilməz!").MaximumLength(50).WithMessage("Başlıq 50 simvoldan çox ola bilməz!");
            RuleFor(x => x.Client).MaximumLength(50).WithMessage("Müştəri 50 simvoldan çox ola bilməz!");
            RuleFor(x => x.SiteURL).MaximumLength(50).WithMessage("SiteURL 50 simvoldan çox ola bilməz!");
            RuleFor(x => x.Type).MaximumLength(50).WithMessage("Proyekt tipləri 50 simvoldan çox ola bilməz!");
            RuleFor(x => x.PortfolioCategoryID).NotEqual(0).WithMessage("Kategoriya boş ola bilməz!");
        }
    }
}
