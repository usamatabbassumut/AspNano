using AspNano.DTOs.VenueDTOs;
using AspNano.Domain.Entities;
using FluentValidation;

namespace AspNano.WebApi.Validators
{
    public class CreateVenueValidator:AbstractValidator<CreateVenueRequest>
    {
        public CreateVenueValidator()
        {
            RuleFor(x => x.VenueName).NotEmpty();
            RuleFor(x => x.VenueDescription).NotEmpty();
        }
    }
}
