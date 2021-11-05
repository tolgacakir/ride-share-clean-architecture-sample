using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideShare.Domain.Entities;

namespace RideShare.Persistence.Configurations
{
    public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.Property(x => x.Id)
                .IsRequired();
            
            builder.Ignore(x=>x.AcceptedDemands);
            builder.Ignore(x=>x.AllDemands);
            builder.Ignore(x=>x.AwaitingDemands);
        }
    }
}