using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RideShare.Domain.Entities;

namespace RideShare.Application.Common
{
    public interface IRideShareDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Driver> Drivers { get; set; }
        DbSet<Passenger> Passengers { get; set; }
        DbSet<TravelPlan> TravelPlans { get; set; }
        DbSet<TravelDemand> TravelDemands { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}