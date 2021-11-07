using System;
using System.Collections.Generic;
using System.Linq;
using RideShare.Domain.Common;

namespace RideShare.Domain.Entities
{
    public class TravelPlan : Entity<int>
    {
        public string Caption { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public DateTime StartAt { get; private set; }
        public byte Capacity { get; private set; }
        public ushort AwaitingDemandCapacity { get; private set; } = 5;
        public TravelPlanStatuses Status { get; private set; } = TravelPlanStatuses.Active;
        
        public Driver Driver { get; private set; }
        public ICollection<TravelDemand> Demands { get; private set; } = new List<TravelDemand>(); //get engellenmeli


        public uint EmptySeatCount => Capacity - (uint)Passengers.Count;
        public IReadOnlyCollection<TravelDemand> AcceptedDemands =>  Demands.Where(x=>x.Status == DemandStatuses.Accepted).ToList();
        public IReadOnlyCollection<TravelDemand> AwaitingDemands => Demands.Where(x=>x.Status == DemandStatuses.Awaiting).ToList();
        public IReadOnlyCollection<TravelDemand> RejectedDemands => Demands.Where(x=>x.Status == DemandStatuses.Rejected).ToList();
        public IReadOnlyCollection<Passenger> Passengers => AcceptedDemands.Select(x=>x.Passenger).ToList();



        public TravelPlan(string caption, string from, string to, DateTime startAt, Driver driver, byte capacity=1, ushort? awaitingDemandCapacity=5)
        {
            Caption = caption;
            From = from;
            To = to;

            if (awaitingDemandCapacity == null)
            {
                awaitingDemandCapacity = 5;
            }

            if (startAt <= DateTime.Now)
            {
                throw new ArgumentException();
            }
            StartAt = startAt;

            if (awaitingDemandCapacity < 1)
            {
                throw new ArgumentException();
            }

            if (capacity < 1)
            {
                throw new ArgumentException();
            }
            Capacity = capacity;
            Driver = driver;
        }

        public TravelPlan()
        {
            
        }
        public bool IsOwnDemand(TravelDemand demand) => Demands.Any(x=>x.Id == demand.Id);
        public bool IsOwnPassenger(Passenger passenger) => AcceptedDemands.Any(x=>x.Passenger.Id == passenger.Id) || AwaitingDemands.Any(x=>x.Passenger.Id == passenger.Id);
        public void Done()
        {
            if (Status == TravelPlanStatuses.Active)
            {
                Status = TravelPlanStatuses.Done;    
            }
            else
            {
                throw new InvalidOperationException();
            }

            foreach (var demand in AcceptedDemands)
            {
                demand.Done();
            }
        }

        public void Cancel()
        {
            if (Status == TravelPlanStatuses.Active)
            {
                Status = TravelPlanStatuses.Canceled;    
            }
        }

        public void AcceptDemand(TravelDemand demand)
        {
            if (! IsOwnDemand(demand))
            {
                throw new InvalidOperationException();
            }

            if(Status == TravelPlanStatuses.Active 
                && EmptySeatCount > 0
                && DateTime.Now < StartAt)
            {
                demand.Accept();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void RejectDemand(TravelDemand demand)
        {
            if (! IsOwnDemand(demand))
            {
                throw new InvalidOperationException();
            }

            demand.Reject();
        }

        public void AddDemand(TravelDemand demand)
        {
            if (IsOwnDemand(demand) || IsOwnPassenger(demand.Passenger))
            {
                throw new InvalidOperationException();
            }

            var demandCount = Demands.Count();
            if (demand.Status == DemandStatuses.Awaiting 
                && AwaitingDemands.Count < AwaitingDemandCapacity
                && DateTime.Now < StartAt)
            {
                Demands.Add(demand);
            }
            else
            {
                throw new InvalidOperationException(); //TODO: özel domain exceptionlar yazılacak...
            }
        }
    }

    public enum TravelPlanStatuses
    {
        None,
        Active,
        Done,
        Canceled
    }
}