using MediatR;
using Microsoft.EntityFrameworkCore;
using VesselManagement.Api.Exceptions;
using VesselManagement.Api.Models.Commands;
using VesselManagement.Api.Data;

namespace VesselManagement.Api.Handlers;

/// <summary>
/// Handler for UpdateVesselCommand.
/// </summary>
public class UpdateVesselCommandHandler(VesselDbContext dbContext, ILogger<UpdateVesselCommandHandler> logger)
	: IRequestHandler<UpdateVesselCommand>
{
	private readonly VesselDbContext _dbContext = dbContext;
	private readonly ILogger<UpdateVesselCommandHandler> logger = logger;

	public async Task Handle(UpdateVesselCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Handling UpdateVesselCommand for Vessel Id: {Id}", request.Id);

		var vessel = await _dbContext.Vessels.FindAsync(request.Id, cancellationToken)
			?? throw new EntityNotFoundException($"Vessel with id {request.Id} not found.");

		// If the IMO is being updated, ensure uniqueness.
		if (vessel.IMO != request.IMO &&
			await _dbContext.Vessels.AnyAsync(v => v.IMO == request.IMO, cancellationToken))
		{
			throw new EntityExistsException($"A vessel with IMO {request.IMO} already exists.");
		}

		vessel.Name = request.Name;
		vessel.IMO = request.IMO;
		vessel.Type = request.Type;
		vessel.Capacity = request.Capacity;

		await _dbContext.SaveChangesAsync(cancellationToken);

		logger.LogInformation("Updated vessel with Id: {Id}", vessel.Id);
	}
}