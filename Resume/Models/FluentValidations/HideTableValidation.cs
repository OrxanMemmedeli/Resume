using FluentValidation;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations
{
    public class HideTableValidation : AbstractValidator<HideTable>
    {
        public HideTableValidation()
        {
            RuleFor(x => x.TableName).NotNull().WithMessage("Cədvəl adı Boş ola bilməz!").NotEmpty().WithMessage("Cədvəl adı Boş ola bilməz!").MaximumLength(50).WithMessage("Cədvəl adı 50 simvoldan çox ola bilməz!");
        }
    }
}
