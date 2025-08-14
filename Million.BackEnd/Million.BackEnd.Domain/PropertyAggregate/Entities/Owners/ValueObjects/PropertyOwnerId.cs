using Million.BackEnd.Domain.Common.Models;

namespace Million.BackEnd.Domain.PropertyAggregate.Entities.Owners.ValueObjects
{
    public class PropertyOwnerId : ValueObject
    {
        public string Value { get; protected set; }

        [Obsolete("Only for reflection", true)]
        protected PropertyOwnerId() { }
        private PropertyOwnerId(Ulid value)
        {
            Value = value.ToString();
        }

        public static PropertyOwnerId CreateUnique()
        {
            return new PropertyOwnerId(Ulid.NewUlid());
        }

        public static PropertyOwnerId Create(Ulid value)
        {
            return new PropertyOwnerId(value);
        }

        public static PropertyOwnerId Create(string value)
        {
            return new PropertyOwnerId(Ulid.Parse(value));
        }

        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
