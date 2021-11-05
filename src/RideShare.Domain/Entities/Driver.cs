using System;
using System.Collections.Generic;
using System.Linq;
using RideShare.Domain.Common;

namespace RideShare.Domain.Entities
{
    public class Driver : Entity<int>
    {
        public User User { get; private set; }
        public ICollection<TravelPlan> TravelPlans { get; private set; } = new List<TravelPlan>(); //TODO: Travel planın get edilmesi, her elemana erişip ekleme/silme imkanı tanır. engellenmeli.
        public IReadOnlyList<TravelPlan> AllTravelPlans => TravelPlans.ToList();
        public IReadOnlyList<TravelPlan> ActiveTravelPlans => TravelPlans.Where(x=>x.Status == TravelPlanStatuses.Active).ToList();

        public bool IsOwnPlan(TravelPlan plan) => TravelPlans.Any(x=>x.Id == plan.Id);

        public Driver(User user)
        {
            User = user;
        }

        public Driver()
        {
            
        }

        public TravelPlan CreateTravelPlan(string caption, string from, string to, DateTime startAt,ushort awaitingDemandCapacity, byte capacity)
        {
            var plan = new TravelPlan(caption,from,to,startAt,this,awaitingDemandCapacity,capacity);
            TravelPlans.Add(plan);
            return plan;
        }

        public void FinishTravelPlan(TravelPlan plan)
        {
            if (! IsOwnPlan(plan))
            {
                throw new InvalidOperationException();
            }
            plan.Done();
        }

        public void CancelTravelPlan(TravelPlan plan)
        {
            if (! IsOwnPlan(plan))
            {
                throw new InvalidOperationException();
            }
            plan.Cancel();
        }

        public void AcceptDemand(TravelPlan plan, TravelDemand demand)
        {
            if (! IsOwnPlan(plan))
            {
                throw new InvalidOperationException();
            }
            plan.AcceptDemand(demand);
        }

        public void RejectDemand(TravelPlan plan, TravelDemand demand)
        {
            if (! IsOwnPlan(plan))
            {
                throw new InvalidOperationException();
            }
            plan.RejectDemand(demand);
        }
    }
}