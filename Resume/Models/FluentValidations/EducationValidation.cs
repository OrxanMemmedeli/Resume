using FluentValidation;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations
{
    public class EducationValidation : AbstractValidator<Education>
    {
        public EducationValidation()
        {
            RuleFor(x => x.EducationCenter).NotNull().WithMessage("Təhsil mərkəzi Boş ola bilməz!").NotEmpty().WithMessage("Təhsil mərkəzi Boş ola bilməz!").MaximumLength(150).WithMessage("Təhsil mərkəzi 150 simvoldan çox ola bilməz!");
            RuleFor(x => x.Specialty).NotNull().WithMessage("İxtisas Boş ola bilməz!").NotEmpty().WithMessage("İxtisas Boş ola bilməz!").MaximumLength(150).WithMessage("İxtisas 150 simvoldan çox ola bilməz!");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Açıqlama 500 simvoldan çox ola bilməz!");
            RuleFor(x => x.Faculty).MaximumLength(150).WithMessage("Fakültə 150 simvoldan çox ola bilməz!");
            RuleFor(x => x.StartDate).LessThanOrEqualTo(x => x.EndDate).WithMessage("Başlama tarixi Son tarixdən kiçik olmalıdır!");
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate).WithMessage("Son tarix Balama tarixindən böyük olmalıdır!");
        }
    }
}
