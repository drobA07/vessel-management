using VesselManagement.Api.Exceptions;
using VesselManagement.Api.Models.Data;
using VesselManagement.Api.Models.Queries;
using VesselManagement.Api.Queries;
using Xunit;
using Assert = Xunit.Assert;

namespace VesselManagement.Tests;

public class GetVesselByIdQueryHandlerTests : VesselCommandHandlerTests
{
	[Fact]
	public async Task GetVesselByIdQueryHandler_Should_Return_Vessel_If_Found()
	{
		using var context = CreateDbContext();

		var logger = CreateLogger<GetVesselByIdQueryHandler>();

		// Pre-seed a vessel.
		var vessel = new Vessel
		{
			Id = Guid.NewGuid(),
			Name = "Test Vessel",
			IMO = "IMO_TEST",
			Type = VesselType.Passenger,
			Capacity = 1500m
		};

		context.Vessels.Add(vessel);

		await context.SaveChangesAsync();

		var handler = new GetVesselByIdQueryHandler(context, logger);

		var result = await handler.Handle(new GetVesselByIdQuery(vessel.Id), CancellationToken.None);

		Assert.NotNull(result);
		Assert.Equal(vessel.Id, result.Id);
	}

	[Fact]
	public async Task GetVesselByIdQueryHandler_Should_Throw_EntityNotFoundException_When_Not_Found()
	{
		using var context = CreateDbContext();

		var logger = CreateLogger<GetVesselByIdQueryHandler>();

		var handler = new GetVesselByIdQueryHandler(context, logger);

		var query = new GetVesselByIdQuery(Guid.NewGuid());

		var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() =>
			handler.Handle(query, CancellationToken.None));

		Assert.Contains("not found", exception.Message);
	}
}
