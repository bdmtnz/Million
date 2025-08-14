using Million.BackEnd.Domain.Common.Models;
using Million.BackEnd.Domain.PropertyAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.BackEnd.Domain.PropertyAggregate.Entities.Traces.ValueObjects
{
    public class PropertyTraceId : ValueObject
    {
        public string Value { get; protected set; }

        [Obsolete("Only for reflection", true)]
        protected PropertyTraceId() { }
        private PropertyTraceId(Ulid value)
        {
            Value = value.ToString();
        }

        public static PropertyTraceId CreateUnique()
        {
            return new PropertyTraceId(Ulid.NewUlid());
        }

        public static PropertyTraceId Create(Ulid value)
        {
            return new PropertyTraceId(value);
        }

        public static PropertyTraceId Create(string value)
        {
            return new PropertyTraceId(Ulid.Parse(value));
        }

        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
