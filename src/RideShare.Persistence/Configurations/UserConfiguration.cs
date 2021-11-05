using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RideShare.Domain.Entities;

namespace RideShare.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Id)
                .IsRequired();

            builder.HasIndex(x => x.Username)
                .IsUnique();
            builder.Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(10);
            
            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasPrecision(0);
                //.HasDefaultValueSql("getdate()"); This value will be generated when ef is saving changes cause of DB independency

            builder.Property(x => x.ModifiedAt)
                .IsRequired(false)
                .HasPrecision(0);

            builder.HasOne(u => u.Driver)
                .WithOne(d => d.User)
                .HasForeignKey<Driver>();


            builder.HasOne(u => u.Passenger)
                .WithOne(p => p.User)
                .HasForeignKey<Passenger>();

        }
    }
}