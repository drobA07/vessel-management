using VesselManagement.Api.Exceptions;
using VesselManagement.Api.Handlers;
using VesselManagement.Api.Models.Commands;
using VesselManagement.Api.Models.Data;
using Xunit;
using Assert = Xunit.Assert;

namespace VesselManagement.Tests;

public class UpdateVesselCommandHandlerTests : VesselCommandHandlerTests
{
	[Fact]
	public async Task UpdateVesselCommandHandler_Should_Update_Vessel_Successfully()
	{
		using var context = CreateDbContext();

		var logger = CreateLogger<UpdateVesselCommandHandler>();

		// Pre-seed a vessel.
		var vessel = new Vessel
		{
			Id = Guid.NewGuid(),
			Name = "Old Vessel",
			IMO = "IMO0001",
			Type = VesselType.Cargo,
			Capacity = 2000m
		};

		context.Vessels.Add(vessel);

		await context.SaveChangesAsync();

		var handler = new UpdateVesselCommandHandler(context, logger);

		var command = new UpdateVesselCommand(vessel.Id, "Updated Vessel", "IMO0001", VesselType.Passenger, 2500m);

		await handler.Handle(command, CancellationToken.None);

		var updatedVessel = await context.Vessels.FindAsync(vessel.Id);

		Assert.Equal("Updated Vessel", updatedVessel.Name);
		Assert.Equal(VesselType.Passenger, updatedVessel.Type);
		Assert.Equal(2500m, updatedVessel.Capacity);
	}

	[Fact]
	public async Task UpdateVesselCommandHandler_Should_Throw_EntityNotFoundException_When_Vessel_Not_Found()
	{
		using var context = CreateDbContext();

		var logger = CreateLogger<UpdateVesselCommandHandler>();

		var handler = new UpdateVesselCommandHandler(context, logger);

		var command = new UpdateVesselCommand(Guid.NewGuid(), "Non-existent Vessel", "IMO9999999", VesselType.Tanker, 3000m);

		var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() =>
			handler.Handle(command, CancellationToken.None));

		Assert.Contains("not found", exception.Message);
	}

	[Fact]
	public async Task UpdateVesselCommandHandler_Should_Throw_When_IMO_Conflict()
	{
		using var context = CreateDbContext();

		var logger = CreateLogger<UpdateVesselCommandHandler>();

		// Pre-seed two vessels.
		var vessel1 = new Vessel
		{
			Id = Guid.NewGuid(),
			Name = "Vessel One",
			IMO = "IMO0001",
			Type = VesselType.Cargo,
			Capacity = 2000m
		};

		var vessel2 = new Vessel
		{
			Id = Guid.NewGuid(),
			Name = "Vessel Two",
			IMO = "IMO0002",
			Type = VesselType.Tanker,
			Capacity = 3000m
		};

		context.Vessels.AddRange(vessel1, vessel2);

		await context.SaveChangesAsync();

		var handler = new UpdateVesselCommandHandler(context, logger);

		// Attempt to update vessel1 with the IMO of vessel2.
		var command = new UpdateVesselCommand(vessel1.Id, "Vessel One Updated", "IMO0002", VesselType.Passenger, 2200m);

		var exception = await Assert.ThrowsAsync<EntityExistsException>(() =>
			handler.Handle(command, CancellationToken.None));

		Assert.Contains("already exists", exception.Message);
	}
}
