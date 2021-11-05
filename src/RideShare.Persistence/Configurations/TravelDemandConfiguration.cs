using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideShare.Domain.Entities;

namespace RideShare.Persistence.Configurations
{
    public class TravelDemandConfiguration : IEntityTypeConfiguration<TravelDemand>
    {
        public void Configure(EntityTypeBuilder<TravelDemand> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Id)
                .IsRequired();
            
            builder.Property(x=>x.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.HasOne(t => t.Passenger)
                .WithMany(p => p.Demands);

            builder.HasOne(d => d.Plan)
                .WithMany(p => p.Demands);
        }
    }
}