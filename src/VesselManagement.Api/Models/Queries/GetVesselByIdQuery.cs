using MediatR;
using VesselManagement.Api.Models.Responses;

namespace VesselManagement.Api.Models.Queries;

/// <summary>
/// Query to retrieve a vessel by its ID.
/// </summary>
/// <param name="Id">The unique identifier of the vessel.</param>
public record GetVesselByIdQuery(Guid Id) : IRequest<VesselResponseDto>;