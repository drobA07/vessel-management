namespace VesselManagement.Api.Models.Data;

/// <summary>
/// Represents a vessel with specific details.
/// </summary>
public class Vessel
{
	/// <summary>
	/// Gets or sets the unique identifier for the vessel.
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Gets or sets the name of the vessel.
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// Gets or sets the International Maritime Organization (IMO) number of the vessel.
	/// </summary>
	public required string IMO { get; set; }

	/// <summary>
	/// Gets or sets the type of the vessel.
	/// </summary>
	public VesselType Type { get; set; }

	/// <summary>
	/// Gets or sets the capacity of the vessel.
	/// </summary>
	public decimal Capacity { get; set; }
}
