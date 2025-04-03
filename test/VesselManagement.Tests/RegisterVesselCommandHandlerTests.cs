using Xunit;
using Assert = Xunit.Assert;
using VesselManagement.Api.Handlers;
using VesselManagement.Api.Exceptions;
using VesselManagement.Api.Models.Data;
using VesselManagement.Api.Models.Commands;

namespace VesselManagement.Tests;

public class RegisterVesselCommandHandlerTests : VesselCommandHandlerTests
{
	[Fact]
	public async Task RegisterVesselCommandHandler_Should_Register_New_Vessel()
	{
		using var context = CreateDbContext();
		
		var logger = CreateLogger<RegisterVesselCommandHandler>();
		
		var handler = new RegisterVesselCommandHandler(context, logger);
		
		var command = new RegisterVesselCommand("Test Vessel", "IMO1234567", VesselType.Cargo, 5000m);

		var result = await handler.Handle(command, CancellationToken.None);

		Assert.NotEqual(Guid.Empty, result.Id);
		Assert.Equal("Test Vessel", result.Name);
		Assert.Equal("IMO1234567", result.IMO);
		Assert.Equal(VesselType.Cargo, result.Type);
		Assert.Equal(5000m, result.Capacity);
	}

	[Fact]
	public async Task RegisterVesselCommandHandler_Should_Throw_When_IMO_Already_Exists()
	{
		using var context = CreateDbContext();
		
		var logger = CreateLogger<RegisterVesselCommandHandler>();
		
		// Pre-seed a vessel with the same IMO.
		context.Vessels.Add(new Vessel
		{
			Id = Guid.NewGuid(),
			Name = "Existing Vessel",
			IMO = "IMO1234567",
			Type = VesselType.Tanker,
			Capacity = 3000m
		});
		
		await context.SaveChangesAsync();

		var handler = new RegisterVesselCommandHandler(context, logger);
		
		var command = new RegisterVesselCommand("New Vessel", "IMO1234567", VesselType.Cargo, 4000m);

		var exception = await Assert.ThrowsAsync<EntityExistsException>(() =>
			handler.Handle(command, CancellationToken.None));
		
		Assert.Contains("already exists", exception.Message);
	}
}
