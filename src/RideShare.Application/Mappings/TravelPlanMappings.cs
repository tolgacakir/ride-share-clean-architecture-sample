using System.Linq;
using AutoMapper;
using RideShare.Application.Queries.TravelPlans.GetActiveTravelPlans;
using RideShare.Application.Queries.TravelPlans.GetTravelPlansByDriver;
using RideShare.Application.Queries.TravelPlans.GetTravelPlansByPassenger;
using RideShare.Domain.Entities;

namespace RideShare.Application.Mappings
{
    public class TravelPlanMappings : Profile
    {
        public TravelPlanMappings()
        {
            CreateMap<TravelPlan, GetActiveTravelPlansResponse>()
                .ForMember(res => res.DriverName, t => t.MapFrom(x=>x.Driver.User.Username));

            CreateMap<TravelPlan, GetTravelPlansByPassengerResponse>()
                .ForMember(res => res.PassengerNames, t => t.MapFrom(x=>x.Passengers.Select(p => p.User.Username)))
                .ForMember(res => res.DriverName, t => t.MapFrom(x => x.Driver.User.Username));

            CreateMap<TravelPlan, GetTravelPlansByDriverResponse>()
                .ForMember(res => res.PassengerNames, t => t.MapFrom(x => x.Passengers.Select(p => p.User.Username)))
                .ForMember(res => res.DriverName, t => t.MapFrom(x => x.Driver.User.Username));
        }
    }
}