using Million.BackEnd.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.BackEnd.Domain.PropertyAggregate.ValueObjects
{
    public class PropertyId : AggregateRootId<string>
    {
        public override string Value { get; protected set; }

        private PropertyId() { }
        private PropertyId(Ulid value)
        {
            Value = value.ToString();
        }

        public static PropertyId CreateUnique()
        {
            return new PropertyId(Ulid.NewUlid());
        }

        public static PropertyId Create(Ulid value)
        {
            return new PropertyId(value);
        }

        public static PropertyId Create(string value)
        {
            return new PropertyId(Ulid.Parse(value));
        }

        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
