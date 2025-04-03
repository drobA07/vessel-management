using VesselManagement.Api.Models.Data;
using VesselManagement.Api.Models.Responses;

namespace VesselManagement.Api.Extensions;

public static class VesselMappingExtensions
{
	public static VesselResponseDto ToResponseDto(this Vessel vessel)
	{
		if (vessel == null) throw new ArgumentNullException(nameof(vessel));
		return new VesselResponseDto(
			vessel.Id,
			vessel.Name,
			vessel.IMO,
			vessel.Type,
			vessel.Capacity);
	}
}

