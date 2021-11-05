using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideShare.Domain.Entities;

namespace RideShare.Persistence.Configurations
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.Property(x => x.Id)
                .IsRequired();

            builder.Ignore(x=>x.AllTravelPlans);
            builder.Ignore(x=>x.ActiveTravelPlans);
        }
    }
}