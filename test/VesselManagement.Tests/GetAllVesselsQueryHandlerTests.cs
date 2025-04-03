using VesselManagement.Api.Models.Data;
using VesselManagement.Api.Queries;
using VesselManagement.Api.Models.Queries;
using Xunit;
using Assert = Xunit.Assert;

namespace VesselManagement.Tests;

public class GetAllVesselsQueryHandlerTests : VesselCommandHandlerTests
{
	[Fact]
	public async Task GetAllVesselsQueryHandler_Should_Return_All_Vessels()
	{
		using var context = CreateDbContext();

		var logger = CreateLogger<GetAllVesselsQueryHandler>();

		// Pre-seed vessels.
		context.Vessels.AddRange(
			new Vessel { Id = Guid.NewGuid(), Name = "Vessel A", IMO = "IMO_A", Type = VesselType.Cargo, Capacity = 1000m },
			new Vessel { Id = Guid.NewGuid(), Name = "Vessel B", IMO = "IMO_B", Type = VesselType.Tanker, Capacity = 2000m }
		);

		await context.SaveChangesAsync();

		var handler = new GetAllVesselsQueryHandler(context, logger);

		var result = await handler.Handle(new GetAllVesselsQuery(), CancellationToken.None);

		Assert.NotNull(result);
		Assert.Equal(2, result.Count);
	}
}