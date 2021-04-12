using FluentValidation;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations
{
    public class SkillValidation : AbstractValidator<Skill>
    {
        public SkillValidation()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Bacarıq&Qabiliyyət Boş ola bilməz!").NotEmpty().WithMessage("Bacarıq&Qabiliyyət Boş ola bilməz!").MaximumLength(50).WithMessage("Bacarıq&Qabiliyyət 50 simvoldan çox ola bilməz!");
            RuleFor(x => x.Percent).LessThanOrEqualTo(100).WithMessage("Faiz maksimum 100 ola bilər!").GreaterThanOrEqualTo(0).WithMessage("Faiz minimum 0 ola bilər!");
            RuleFor(x => x.SkillCategoryID).NotEqual(0).WithMessage("Kategoriya boş ola bilməz!");
        }
    }
}
