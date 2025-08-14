using Million.BackEnd.Domain.Common.Models;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Owners.ValueObjects;

namespace Million.BackEnd.Domain.PropertyAggregate.Entities.Images.ValueObjects
{
    public class PropertyImageId : ValueObject
    {
        public string Value { get; protected set; }

        [Obsolete("Only for reflection", true)]
        protected PropertyImageId() { }
        private PropertyImageId(Ulid value)
        {
            Value = value.ToString();
        }

        public static PropertyImageId CreateUnique()
        {
            return new PropertyImageId(Ulid.NewUlid());
        }

        public static PropertyImageId Create(Ulid value)
        {
            return new PropertyImageId(value);
        }

        public static PropertyImageId Create(string value)
        {
            return new PropertyImageId(Ulid.Parse(value));
        }

        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
