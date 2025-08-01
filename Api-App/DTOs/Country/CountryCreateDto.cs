using FluentValidation;

namespace Api_App.DTOs.Country
{
    public class CountryCreateDto
    {
        public string Name { get; set; }
    }
    public class CountryCreateDtoValidator : AbstractValidator<CountryCreateDto>
    {
        public CountryCreateDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required").MaximumLength(20);
        }
    }
}
