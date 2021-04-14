using FluentValidation;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations
{
    public class BlogValidation : AbstractValidator<Blog>
    {
        public BlogValidation()
        {
            RuleFor(x => x.Title).NotNull().WithMessage("Başlıq Boş ola bilməz!").NotEmpty().WithMessage("Başlıq Boş ola bilməz!").MaximumLength(100).WithMessage("Başlıq 100 simvoldan çox ola bilməz!");
            RuleFor(x => x.Text).NotNull().WithMessage("Başlıq Boş ola bilməz!").NotEmpty().WithMessage("Başlıq Boş ola bilməz!");
            RuleFor(x => x.BlogCategoryID).NotEqual(0).WithMessage("Kategoriya boş ola bilməz!");
        }
    }
}
