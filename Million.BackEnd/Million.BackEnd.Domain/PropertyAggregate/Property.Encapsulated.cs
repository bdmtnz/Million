using Million.BackEnd.Domain.PropertyAggregate.Entities.Images;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Images.ValueObjects;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Owners;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Owners.ValueObjects;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Traces;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Traces.ValueObjects;
using Million.BackEnd.Domain.PropertyAggregate.ValueObjects;

namespace Million.BackEnd.Domain.PropertyAggregate
{
    public sealed partial class Property
    {
        protected Property() : base(default) { }
        private Property(PropertyId id, string name, string address, decimal price, string code, int year, DateTime createdOnUtc) : base(id)
        {
            Name = name;
            Address = address;
            Price = price;
            Code = code;
            Year = year;
            CreatedOnUtc = createdOnUtc;
        }

        public static Property Create(PropertyId id, string name, string address, decimal price, int year)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
            ArgumentException.ThrowIfNullOrWhiteSpace(address, nameof(address));
            ArgumentNullException.ThrowIfNull(id, nameof(id));

            var code = Ulid.NewUlid().ToString();
            return new Property(id, name, address, price, code, year, DateTime.UtcNow);
        }

        public Property SetOwner(PropertyOwnerId id, string name, string address, string photo, DateTime bornOnUtc)
        {
            Owner = PropertyOwner.Create(id, name, address, photo, bornOnUtc);
            return this;
        }

        public Property SetImage(PropertyImageId id, string file)
        {
            Image = PropertyImage.Create(id, file);
            return this;
        }

        public Property SetTrace(PropertyTraceId id, string name, decimal value, DateTime saledOnUtc)
        {
            Trace = PropertyTrace.Create(id, name, value, saledOnUtc);
            return this;
        }
    }
}
