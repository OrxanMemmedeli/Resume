using FluentValidation;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations
{
    public class BlogCategoryValidation : AbstractValidator<BlogCategory>
    {
        public BlogCategoryValidation()
        {
            RuleFor(x => x.Category).NotNull().WithMessage("Kateqoriya Boş ola bilməz!").NotEmpty().WithMessage("Kateqoriya Boş ola bilməz!").MaximumLength(50).WithMessage("Kateqoriya 50 simvoldan çox ola bilməz!");
        }
    }
}
