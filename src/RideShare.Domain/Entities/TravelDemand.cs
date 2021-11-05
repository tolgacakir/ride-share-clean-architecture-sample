using System;
using RideShare.Domain.Common;

namespace RideShare.Domain.Entities
{
    public class TravelDemand : Entity<int>
    {
        public DemandStatuses Status { get; private set; } = DemandStatuses.Awaiting;
        public Passenger Passenger { get; private set; }
        public TravelPlan Plan { get; private set; }
        
        public TravelDemand(TravelPlan plan, Passenger demandant)
        {
            Passenger = demandant;
            plan.AddDemand(this);
            Plan = plan;
        }

        public TravelDemand()
        {
            
        }

        public void Accept()
        {
            if (Status == DemandStatuses.Awaiting)
            {
                Status = DemandStatuses.Accepted;    
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        public void Reject()
        {
            if (Status == DemandStatuses.Awaiting || Status == DemandStatuses.Accepted)
            {
                Status = DemandStatuses.Rejected;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        public void Cancel()
        {
            if (Status == DemandStatuses.Accepted || Status == DemandStatuses.Awaiting)
            {
                Status = DemandStatuses.Canceled;    
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void Done()
        {
            if (Status == DemandStatuses.Accepted)
            {
                Status = DemandStatuses.Done;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public enum DemandStatuses
    {
        None,
        Awaiting,
        Accepted,
        Rejected,
        Canceled,
        Done
    }
}