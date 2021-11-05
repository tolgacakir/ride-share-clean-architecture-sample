using System;
using System.Collections.Generic;
using System.Linq;
using RideShare.Domain.Common;

namespace RideShare.Domain.Entities
{
    public class Passenger : Entity<int>
    {
        public User User { get; private set; }
        
        public ICollection<TravelDemand> Demands { get; private set; } = new List<TravelDemand>(); //TODO: get engellenmeli

        public IReadOnlyCollection<TravelDemand> AllDemands => Demands.ToList();
        public IReadOnlyCollection<TravelDemand> AwaitingDemands => Demands.Where(x=>x.Status == DemandStatuses.Awaiting).ToList();
        public IReadOnlyCollection<TravelDemand> AcceptedDemands => Demands.Where(x=>x.Status == DemandStatuses.Accepted).ToList();
        public bool IsOwnDemand(TravelDemand demand) => Demands.Any(x=>x.Id == demand.Id);

        public Passenger(User user)
        {
            User = user;
        }

        public Passenger()
        {
            
        }

        public TravelDemand CreateTravelDemand(TravelPlan plan)
        {
            var demand = new TravelDemand(plan,this);
            Demands.Add(demand);
            return demand;
        }

        public void CancelDemand(TravelDemand demand)
        {
            if (! IsOwnDemand(demand))
            {
                throw new InvalidOperationException();
            }
            demand.Cancel();
        }
    }
}