using FluentValidation;
using VesselManagement.Api.Extensions;
using VesselManagement.Api.Models.Requests;

namespace VesselManagement.Api.Validators;

/// <summary>
/// Validator for UpdateVesselCommand.
/// </summary>
public class UpdateVesselRequestValidator : AbstractValidator<UpdateVesselRequest>
{
	public UpdateVesselRequestValidator()
	{
		RuleFor(x => x.Name).ValidateName();
		RuleFor(x => x.IMO).ValidateIMO();
		RuleFor(x => x.Type).ValidateVesselType();
		RuleFor(x => x.Capacity).ValidateCapacity();
	}
}