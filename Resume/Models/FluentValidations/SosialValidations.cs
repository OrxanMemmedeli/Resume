using FluentValidation;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations
{
    public class SosialValidations : AbstractValidator<Sosial>
    {
        public SosialValidations()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Səhifə adı Boş ola bilməz!").NotEmpty().WithMessage("Səhifə adı Boş ola bilməz!").MaximumLength(50).WithMessage("Səhifə adı 50 simvoldan çox ola bilməz!");
            RuleFor(x => x.Icon).NotNull().WithMessage("Icon Boş ola bilməz!").NotEmpty().WithMessage("Icon Boş ola bilməz!").MaximumLength(50).WithMessage("İcon 50 simvoldan çox ola bilməz!");
            RuleFor(x => x.PageURL).NotNull().WithMessage("PageURL Boş ola bilməz!").NotEmpty().WithMessage("PageURL Boş ola bilməz!");
        }
    }
}
