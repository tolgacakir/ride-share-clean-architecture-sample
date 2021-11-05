using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideShare.Domain.Entities;

namespace RideShare.Persistence.Configurations
{
    public class TravelPlanConfiguration : IEntityTypeConfiguration<TravelPlan>
    {
        public void Configure(EntityTypeBuilder<TravelPlan> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Id)
                .IsRequired();

            builder.Property(x=>x.Caption)
                .IsRequired()
                .HasDefaultValue("My Travel Plan")
                .HasMaxLength(50);

            builder.Property(x=>x.From)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x=>x.To)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x=>x.StartAt)
                .IsRequired()
                .HasPrecision(0);
            builder.Property(x=>x.Capacity)
                .IsRequired()
                .HasDefaultValue(1)
                .HasMaxLength(50);
            
            builder.Property(x=>x.AwaitingDemandCapacity)
                .IsRequired()
                .HasDefaultValue(5)
                .HasMaxLength(100);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.HasOne(t => t.Driver)
                .WithMany(d => d.TravelPlans);

            builder.Ignore(x=>x.EmptySeatCount);
            builder.Ignore(x=>x.AcceptedDemands);
            builder.Ignore(x=>x.AwaitingDemands);
            builder.Ignore(x=>x.RejectedDemands);
            builder.Ignore(x=>x.Passengers);
        }
    }
}