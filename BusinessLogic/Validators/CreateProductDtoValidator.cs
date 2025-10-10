using BusinessLogic.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Title)
              .NotEmpty()
              .MinimumLength(3);

            RuleFor(x => x.Discount)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);

            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);

            RuleFor(x => x.CategoryId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Description)
                .MinimumLength(10)
                .MaximumLength(3000);
        }
    }
}
