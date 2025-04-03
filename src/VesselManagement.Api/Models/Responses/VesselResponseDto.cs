using VesselManagement.Api.Models.Data;

namespace VesselManagement.Api.Models.Responses;

/// <summary>
/// Represents a response DTO for a vessel.
/// </summary>
/// <param name="Id">The unique identifier of the vessel.</param>
/// <param name="Name">The name of the vessel.</param>
/// <param name="IMO">The International Maritime Organization number of the vessel.</param>
/// <param name="Type">The type of the vessel.</param>
/// <param name="Capacity">The capacity of the vessel.</param>
public record VesselResponseDto(Guid Id, string Name, string IMO, VesselType Type, decimal Capacity);
