using MediatR;
using VesselManagement.Api.Models.Responses;

namespace VesselManagement.Api.Models.Queries;

/// <summary>
/// Query to retrieve all vessels.
/// </summary>
public record GetAllVesselsQuery() : IRequest<List<VesselResponseDto>>;