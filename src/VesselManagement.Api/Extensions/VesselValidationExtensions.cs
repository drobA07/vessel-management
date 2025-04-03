using FluentValidation;
using VesselManagement.Api.Models.Data;

namespace VesselManagement.Api.Extensions;

public static class VesselValidationExtensions
{
	public static IRuleBuilderOptions<T, string> ValidateName<T>(this IRuleBuilder<T, string> ruleBuilder)
	{
		return ruleBuilder
			.NotEmpty().WithMessage("Name is required.")
			.MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
	}

	public static IRuleBuilderOptions<T, string> ValidateIMO<T>(this IRuleBuilder<T, string> ruleBuilder)
	{
		return ruleBuilder
			.NotEmpty().WithMessage("IMO is required.")
			.MaximumLength(20).WithMessage("IMO must not exceed 20 characters.");
	}

	public static IRuleBuilderOptions<T, VesselType> ValidateVesselType<T>(this IRuleBuilder<T, VesselType> ruleBuilder)
	{
		return ruleBuilder
			.IsInEnum().WithMessage("Invalid vessel type provided.");
	}

	public static IRuleBuilderOptions<T, decimal> ValidateCapacity<T>(this IRuleBuilder<T, decimal> ruleBuilder)
	{
		return ruleBuilder
			.GreaterThan(0).WithMessage("Capacity must be greater than zero.");
	}
}