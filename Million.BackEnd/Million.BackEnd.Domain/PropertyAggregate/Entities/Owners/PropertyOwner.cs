using Million.BackEnd.Domain.Common.Models;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Owners.ValueObjects;

namespace Million.BackEnd.Domain.PropertyAggregate.Entities.Owners
{
    public class PropertyOwner : Entity<PropertyOwnerId>
    {
        public  string Name { get; private set; }
        public string Address { get; private set; }
        public string Photo { get; private set; }
        public DateTime BornOnUtc { get; private set; }

        protected PropertyOwner() : base(default) { }
        private PropertyOwner(PropertyOwnerId id, string name, string address, string photo, DateTime bornOnUtc) : base(id)
        {
            Name = name;
            Address = address;
            Photo = photo;
            BornOnUtc = bornOnUtc;
        }

        public static PropertyOwner Create(PropertyOwnerId id, string name, string address, string photo, DateTime bornOnUtc)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            return new PropertyOwner(id, name, address, photo, bornOnUtc);
        }
    }
}
