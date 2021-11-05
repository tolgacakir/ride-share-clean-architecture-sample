using System;

namespace RideShare.Domain.Common
{
    public abstract class AuditableEntity : Entity<Guid>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}