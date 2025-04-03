using Microsoft.EntityFrameworkCore;
using VesselManagement.Api.Models.Data;

namespace VesselManagement.Api.Data;

/// <summary>
/// EF Core DbContext with a unique index on IMO.
/// </summary>
public class VesselDbContext : DbContext
{
	public VesselDbContext(DbContextOptions<VesselDbContext> options) : base(options) { }

	public DbSet<Vessel> Vessels { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Vessel>(entity =>
		{
			entity.HasKey(v => v.Id);

			entity.Property(v => v.Name)
				  .IsRequired()
				  .HasMaxLength(100);

			entity.Property(v => v.IMO)
				  .IsRequired()
				  .HasMaxLength(20);

			entity.HasIndex(v => v.IMO)
				  .IsUnique();

			entity.Property(v => v.Type)
				  .IsRequired();

			entity.Property(v => v.Capacity)
				  .IsRequired();
		});

	}
}