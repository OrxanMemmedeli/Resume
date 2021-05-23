using FluentValidation;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.FluentValidations
{
    public class RoleControlValidation : AbstractValidator<RoleControl>
    {
        public RoleControlValidation()
        {
            RuleFor(x => x.ControllerName).NotNull().WithMessage("Controller Boş ola bilməz!").NotEmpty().WithMessage("Controller Boş ola bilməz!").MaximumLength(100).WithMessage("Controller 100 simvoldan çox ola bilməz!");
            RuleFor(x => x.ActionName).NotNull().WithMessage("Action Boş ola bilməz!").NotEmpty().WithMessage("Action Boş ola bilməz!").MaximumLength(100).WithMessage("Action 100 simvoldan çox ola bilməz!");
        }
    }
}
