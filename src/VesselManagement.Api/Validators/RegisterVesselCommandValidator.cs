using FluentValidation;
using VesselManagement.Api.Extensions;
using VesselManagement.Api.Models.Commands;

namespace VesselManagement.Api.Validators;

/// <summary>
/// Validator for RegisterVesselCommand.
/// </summary>
public class RegisterVesselCommandValidator : AbstractValidator<RegisterVesselCommand>
{
	public RegisterVesselCommandValidator()
	{
		RuleFor(x => x.Name).ValidateName();
		RuleFor(x => x.IMO).ValidateIMO();
		RuleFor(x => x.Type).ValidateVesselType();
		RuleFor(x => x.Capacity).ValidateCapacity();
	}
}