using MediatR;
using VesselManagement.Api.Models.Data;
using VesselManagement.Api.Models.Responses;

namespace VesselManagement.Api.Models.Commands;

/// <summary>
/// Command to register a new vessel.
/// </summary>
/// <param name="Name">The name of the vessel.</param>
/// <param name="IMO">The International Maritime Organization number of the vessel.</param>
/// <param name="Type">The type of the vessel.</param>
/// <param name="Capacity">The capacity of the vessel.</param>
public record RegisterVesselCommand(string Name, string IMO, VesselType Type, decimal Capacity) : IRequest<VesselResponseDto>;