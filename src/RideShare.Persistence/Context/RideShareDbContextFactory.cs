using Microsoft.EntityFrameworkCore;

namespace RideShare.Persistence.Context
{
    public class RideShareDbContextFactory : DesignTimeDbContextFactoryBase<RideShareDbContext>
    {
        protected override RideShareDbContext CreateNewInstance(DbContextOptions<RideShareDbContext> options)
        {
            return new RideShareDbContext(options);
        }
    }
}