using AspNano.DTOs.TenantDTOs;
using FluentValidation;

namespace AspNano.WebApi.Validators.Tenants
{
    public class UpdateTenantValidator: AbstractValidator<UpdateTenantRequest>
    {
        public UpdateTenantValidator()
        {
            RuleFor(x => x.Key).NotEmpty();
        }
    }
}
