using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using VesselManagement.Api.Data;

namespace VesselManagement.Tests;

public class VesselCommandHandlerTests
{
	// Creates a new in-memory VesselDbContext with a unique database name.
	protected VesselDbContext CreateDbContext([CallerMemberName] string testName = "")
	{
		var dbName = $"{testName}_{Guid.NewGuid()}";
		var options = new DbContextOptionsBuilder<VesselDbContext>()
		.UseInMemoryDatabase(databaseName: dbName)
		.Options;
		return new VesselDbContext(options);
	}

	// Creates a generic logger mock for the specified type.
	protected static ILogger<T> CreateLogger<T>()
	{
		return new Mock<ILogger<T>>().Object;
	}
}
