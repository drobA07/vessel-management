using VesselManagement.Api.Models.Data;

namespace VesselManagement.Api.Models.Requests;

public record UpdateVesselRequest(string Name, string IMO, VesselType Type, decimal Capacity);