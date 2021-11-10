using AutoMapper;
using RideShare.Domain.Entities;
using RideShare.Application.Queries.TravelDemands.GetAwaitingTravelDemandsByTravelPlan;

namespace RideShare.Application.Mappings
{
    public class TravelDemandMappings : Profile
    {
        public TravelDemandMappings()
        {
            CreateMap<TravelDemand, GetAwaitingTravelDemandsByTravelPlanResponse>()
                .ForMember(res => res.PassengerName, t => t.MapFrom(x=>x.Passenger.User.Username));
        }
    }
}