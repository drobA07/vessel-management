using MediatR;
using Microsoft.EntityFrameworkCore;
using VesselManagement.Api.Data;
using VesselManagement.Api.Models.Commands;
using VesselManagement.Api.Models.Responses;
using VesselManagement.Api.Exceptions;
using VesselManagement.Api.Extensions;
using VesselManagement.Api.Models.Data;

namespace VesselManagement.Api.Handlers;

/// <summary>
/// Handler for RegisterVesselCommand.
/// </summary>
public class RegisterVesselCommandHandler(VesselDbContext dbContext, ILogger<RegisterVesselCommandHandler> logger)
	: IRequestHandler<RegisterVesselCommand, VesselResponseDto>
{
	private readonly VesselDbContext _dbContext = dbContext;
	private readonly ILogger<RegisterVesselCommandHandler> logger = logger;

	public async Task<VesselResponseDto> Handle(RegisterVesselCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Handling RegisterVesselCommand for IMO: {IMO}", request.IMO);

		if (await _dbContext.Vessels.AnyAsync(v => v.IMO == request.IMO, cancellationToken))
		{
			throw new EntityExistsException($"A vessel with IMO {request.IMO} already exists.");
		}

		var vessel = new Vessel
		{
			Id = Guid.NewGuid(),
			Name = request.Name,
			IMO = request.IMO,
			Type = request.Type,
			Capacity = request.Capacity
		};

		_dbContext.Vessels.Add(vessel);
		await _dbContext.SaveChangesAsync(cancellationToken);

		logger.LogInformation("Registered new vessel with Id: {Id}", vessel.Id);
		return vessel.ToResponseDto();
	}
}