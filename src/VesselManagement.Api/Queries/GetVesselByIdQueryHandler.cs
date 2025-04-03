using MediatR;
using VesselManagement.Api.Exceptions;
using Microsoft.EntityFrameworkCore;
using VesselManagement.Api.Models.Queries;
using VesselManagement.Api.Data;
using VesselManagement.Api.Models.Responses;
using VesselManagement.Api.Extensions;

namespace VesselManagement.Api.Queries;

/// <summary>
/// Handler for GetVesselByIdQuery.
/// </summary>
public class GetVesselByIdQueryHandler(VesselDbContext dbContext, ILogger<GetVesselByIdQueryHandler> logger) :
	IRequestHandler<GetVesselByIdQuery, VesselResponseDto>
{
	private readonly VesselDbContext _dbContext = dbContext;
	private readonly ILogger<GetVesselByIdQueryHandler> logger = logger;

	public async Task<VesselResponseDto> Handle(GetVesselByIdQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Handling GetVesselByIdQuery for Vessel Id: {Id}", request.Id);

		return await _dbContext
			.Vessels
			.AsNoTracking()
			.Where(v => v.Id.Equals(request.Id))
			.Select(v => v.ToResponseDto())
			.FirstOrDefaultAsync(cancellationToken)
			?? throw new EntityNotFoundException($"Vessel with id {request.Id} not found.");
	}
}