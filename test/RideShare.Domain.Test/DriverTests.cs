using System;
using RideShare.Domain.Entities;
using Xunit;

namespace RideShare.Domain.Test
{
    public class DriverTests
    {
        private readonly Driver _driver;
        private readonly CreateTravelPlanParameter _createTravelPlanParameter;
        public DriverTests()
        {
            var user = new User("goodUser","123456");
            _driver = user.CreateDriver();

            _createTravelPlanParameter =new CreateTravelPlanParameter();
        }

        [Fact]
        public void ThrowsArgumentException_WhenCreateTravelPlan_WithStartAtBeforeNow()
        {
            _createTravelPlanParameter.StartAt = DateTime.Now.AddMinutes(-1);
            var c = _createTravelPlanParameter;

            Assert.Throws<ArgumentException>(()=>
                _driver.CreateTravelPlan(c.Caption,
                    c.From,
                    c.To,
                    c.StartAt,
                    c.Capacity,
                    c.AwaitingDemandCapacity));
            
        }

        [Fact]
        public void ThrowsArgumentException_WhenCreateTravelPlan_WithAwaitingDemandCapacityLowerThanOne()
        {
            _createTravelPlanParameter.AwaitingDemandCapacity = 0;
            var c = _createTravelPlanParameter;
            
            Assert.Throws<ArgumentException>(()=>
                _driver.CreateTravelPlan(c.Caption,
                    c.From,
                    c.To,
                    c.StartAt,
                    c.Capacity,
                    c.AwaitingDemandCapacity));
            
        }

        [Fact]
        public void ThrowsArgumentException_WhenCreateTravelPlan_WithCapacityLowerThanOne()
        {
            _createTravelPlanParameter.Capacity = 0;
            var c = _createTravelPlanParameter;
            
            Assert.Throws<ArgumentException>(()=>
                _driver.CreateTravelPlan(c.Caption,
                    c.From,
                    c.To,
                    c.StartAt,
                    c.Capacity,
                    c.AwaitingDemandCapacity));
            
        }

        [Fact]
        public void Success_WhenCreateTravelPlan_WithGoodParameters()
        {
            var c = _createTravelPlanParameter;
            
            var plan = _driver.CreateTravelPlan(c.Caption,
                    c.From,
                    c.To,
                    c.StartAt,
                    c.Capacity,
                    c.AwaitingDemandCapacity);
            
            Assert.NotNull(plan);
            Assert.Equal(c.Caption, plan.Caption);
            Assert.Equal(c.From, plan.From);
            Assert.Equal(c.To, plan.To);
            Assert.Equal(c.StartAt, plan.StartAt);
            Assert.Equal(c.Capacity, plan.Capacity);
            Assert.Equal(c.AwaitingDemandCapacity, plan.AwaitingDemandCapacity);
        }

        [Fact]
        public void ThrowsInvalidOperationException_WhenFinishTravelPlan_WithNotOwnPlan()
        {
            var c = _createTravelPlanParameter;
            var plan = _driver.CreateTravelPlan(c.Caption,
                    c.From,
                    c.To,
                    c.StartAt,
                    c.Capacity,
                    c.AwaitingDemandCapacity);


            var newUser = new User("goodUser","123456");
            var newDriver = newUser.CreateDriver();
            
            Assert.Throws<InvalidOperationException>(()=>
                newDriver.FinishTravelPlan(plan));
        }
        
        [Fact]
        public void Success_WhenFinishTravelPlan_WithOwnPlan()
        {
            var c = _createTravelPlanParameter;
            var plan = _driver.CreateTravelPlan(c.Caption,
                    c.From,
                    c.To,
                    c.StartAt,
                    c.Capacity,
                    c.AwaitingDemandCapacity);

            _driver.FinishTravelPlan(plan);

            Assert.Equal(TravelPlanStatuses.Done, plan.Status);
        }

        [Fact]
        public void ThrowsInvalidOperationException_WhenCancelTravelPlan_WithNotOwnPlan()
        {
            var c = _createTravelPlanParameter;
            var plan = _driver.CreateTravelPlan(c.Caption,
                    c.From,
                    c.To,
                    c.StartAt,
                    c.Capacity,
                    c.AwaitingDemandCapacity);


            var newUser = new User("goodUser","123456");
            var newDriver = newUser.CreateDriver();
            
            Assert.Throws<InvalidOperationException>(()=>
                newDriver.CancelTravelPlan(plan));
        }

        [Fact]
        public void Success_WhenCancelTravelPlan_WithOwnPlan()
        {
            var c = _createTravelPlanParameter;
            var plan = _driver.CreateTravelPlan(c.Caption,
                    c.From,
                    c.To,
                    c.StartAt,
                    c.Capacity,
                    c.AwaitingDemandCapacity);

            _driver.CancelTravelPlan(plan);

            Assert.Equal(TravelPlanStatuses.Canceled, plan.Status);
        }

        [Fact]
        public void ThrowsInvalidOperationException_WhenAcceptTravelDemand_WithNotOwnPlan()
        {
            var c = _createTravelPlanParameter;
            var plan = _driver.CreateTravelPlan(c.Caption,
                    c.From,
                    c.To,
                    c.StartAt,
                    c.Capacity,
                    c.AwaitingDemandCapacity);
            var passenger = _driver.User.CreatePassenger();
            var demand = passenger.CreateTravelDemand(plan);

            var newUser = new User("goodUser","123456");
            var newDriver = newUser.CreateDriver();
            
            Assert.Throws<InvalidOperationException>(()=>
                newDriver.AcceptDemand(plan,demand));
        }

        [Fact]
        public void Success_WhenAcceptTravelDemand_WithOwnPlan()
        {
            var c = _createTravelPlanParameter;
            var plan = _driver.CreateTravelPlan(c.Caption,
                    c.From,
                    c.To,
                    c.StartAt,
                    c.Capacity,
                    c.AwaitingDemandCapacity);            

            var newUser = new User("goodUser","123456");
            var newPassenger = newUser.CreatePassenger();
            var demand = newPassenger.CreateTravelDemand(plan);

            _driver.AcceptDemand(plan,demand);

            Assert.Equal(DemandStatuses.Accepted, demand.Status);
        }

        [Fact]
        public void ThrowsInvalidOperationException_WhenRejectTravelDemand_WithNotOwnPlan()
        {
            var c = _createTravelPlanParameter;
            var plan = _driver.CreateTravelPlan(c.Caption,
                    c.From,
                    c.To,
                    c.StartAt,
                    c.Capacity,
                    c.AwaitingDemandCapacity);
            var passenger = _driver.User.CreatePassenger();
            var demand = passenger.CreateTravelDemand(plan);

            var newUser = new User("goodUser","123456");
            var newDriver = newUser.CreateDriver();
            
            Assert.Throws<InvalidOperationException>(()=>
                newDriver.RejectDemand(plan,demand));
        }

        [Fact]
        public void Success_WhenRejectTravelDemand_WithOwnPlan()
        {
            var c = _createTravelPlanParameter;
            var plan = _driver.CreateTravelPlan(c.Caption,
                    c.From,
                    c.To,
                    c.StartAt,
                    c.Capacity,
                    c.AwaitingDemandCapacity);            

            var newUser = new User("goodUser","123456");
            var newPassenger = newUser.CreatePassenger();
            var demand = newPassenger.CreateTravelDemand(plan);

            _driver.RejectDemand(plan,demand);

            Assert.Equal(DemandStatuses.Rejected, demand.Status);
        }
    }

    class CreateTravelPlanParameter
    {
        public string Caption { get; set; } = "NewPlan";
        public string From { get; set; } = "Eskisehir";
        public string To { get; set; } = "Istanbul";
        public DateTime StartAt { get; set; } = DateTime.Now.AddHours(1);
        public byte Capacity { get; set; } = 1;
        public ushort? AwaitingDemandCapacity { get; set; } = 1;
    }
}