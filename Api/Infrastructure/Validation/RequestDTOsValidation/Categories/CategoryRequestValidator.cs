using System;
using Api.Infrastructure.RequestDTOs.Categories;
using FluentValidation;

namespace Api.Infrastructure.Validation.RequestDTOsValidation.Categories;

public class CategoryRequestValidator:AbstractValidator<CategoryRequestDto>
{
    public CategoryRequestValidator()
    {
        RuleFor(ct=>ct.CategoryId).NotEmpty();
        RuleFor(ct=>ct.Name).NotEmpty().MaximumLength(20).WithMessage("Name cannot be longer than 20 characters");
    }
}
