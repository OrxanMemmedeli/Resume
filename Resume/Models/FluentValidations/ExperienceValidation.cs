using FluentValidation;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations
{
    public class ExperienceValidation : AbstractValidator<Experience>
    {
        public ExperienceValidation()
        {
            RuleFor(x => x.Company).NotNull().WithMessage("Şirkət Boş ola bilməz!").NotEmpty().WithMessage("Şirkət Boş ola bilməz!").MaximumLength(150).WithMessage("Şirkət 150 simvoldan çox ola bilməz!");
            RuleFor(x => x.Position).NotNull().WithMessage("Mövqey Boş ola bilməz!").NotEmpty().WithMessage("Mövqey Boş ola bilməz!").MaximumLength(150).WithMessage("Mövqey 150 simvoldan çox ola bilməz!");
            RuleFor(x => x.Description).NotNull().WithMessage("Açıqlama Boş ola bilməz!").NotEmpty().WithMessage("Açıqlama Boş ola bilməz!").MaximumLength(500).WithMessage("Açıqlama 500 simvoldan çox ola bilməz!");
            RuleFor(x => x.StartDate).LessThanOrEqualTo(x => x.EndDate).WithMessage("Başlama tarixi Son tarixdən kiçik olmalıdır!");
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate).WithMessage("Son tarix Balama tarixindən böyük olmalıdır!");
        }
    }
}
