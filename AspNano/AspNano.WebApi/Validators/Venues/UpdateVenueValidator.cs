using AspNano.DTOs.VenueDTOs;
using FluentValidation;

namespace AspNano.WebApi.Validators.Venues
{
    public class UpdateVenueValidator : AbstractValidator<UpdateVenueRequest>
    {
        public UpdateVenueValidator()
        {
            RuleFor(x => x.VenueName).NotEmpty();
            RuleFor(x => x.VenueDescription).NotEmpty();
        }
    }
}
