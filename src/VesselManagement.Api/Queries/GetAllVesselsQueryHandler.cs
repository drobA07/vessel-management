using MediatR;
using Microsoft.EntityFrameworkCore;
using VesselManagement.Api.Data;
using VesselManagement.Api.Extensions;
using VesselManagement.Api.Models.Queries;
using VesselManagement.Api.Models.Responses;

namespace VesselManagement.Api.Queries;

/// <summary>
/// Handler for GetAllVesselsQuery.
/// </summary>
public class GetAllVesselsQueryHandler(VesselDbContext dbContext, ILogger<GetAllVesselsQueryHandler> logger) :
	IRequestHandler<GetAllVesselsQuery, List<VesselResponseDto>>
{
	private readonly VesselDbContext _dbContext = dbContext;
	private readonly ILogger<GetAllVesselsQueryHandler> logger = logger;

	public async Task<List<VesselResponseDto>> Handle(GetAllVesselsQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Handling GetAllVesselsQuery.");
		return await _dbContext
			.Vessels
			.AsNoTracking()
			.Select(v => v.ToResponseDto())
			.ToListAsync(cancellationToken);
	}
}