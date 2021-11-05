namespace RideShare.Domain.Common
{
    public abstract class Entity<T>
    {
        public T Id { get;  set; }
    }
}