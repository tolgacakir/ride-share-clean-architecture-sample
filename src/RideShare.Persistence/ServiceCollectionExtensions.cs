using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RideShare.Persistence.Context;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideShare.Application.Common;

namespace RideShare.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RideShareDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRideShareDbContext>(provider => provider.GetService<RideShareDbContext>());
        }
    }
}
