using FluentValidation;

namespace MicroServices.Catalog.Api.Features.Categories.Create
{
	public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
	{
		public CreateCategoryCommandValidator()
		{
			RuleFor(c => c.Name)
				.NotEmpty().WithMessage("{PropertyName} cannot be empty")
				.Length(2, 30).WithMessage("{PropertyName} must be between 2 and 30");
		}
	}
}
