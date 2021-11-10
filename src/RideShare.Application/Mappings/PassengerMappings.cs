using AutoMapper;
using RideShare.Application.Queries.Passengers.GetPassengersByTravelPlan;
using RideShare.Domain.Entities;

namespace RideShare.Application.Mappings
{
    public class PassengerMappings : Profile
    {
        public PassengerMappings()
        {
            CreateMap<Passenger, GetPassengersByTravelPlanResponse>()
                .ForMember(res => res.PassengerName, p => p.MapFrom(x=>x.User.Username));
        }
    }
}