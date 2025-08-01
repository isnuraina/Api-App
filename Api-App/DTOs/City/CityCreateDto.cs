using Api_App.DTOs.Country;
using FluentValidation;

namespace Api_App.DTOs.City
{
    public class CityCreateDto
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public int CountryId { get; set; }
    }
    public class CityCreateDtoValidator : AbstractValidator<CityCreateDto>
    {
        public CityCreateDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required").MaximumLength(20);
            RuleFor(p => p.Population).GreaterThan(20).WithMessage("Price must be greater than 20");
        }
    }
}
