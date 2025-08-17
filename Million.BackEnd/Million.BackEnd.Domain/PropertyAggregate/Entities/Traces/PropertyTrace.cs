using Million.BackEnd.Domain.Common.Models;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Traces.ValueObjects;

namespace Million.BackEnd.Domain.PropertyAggregate.Entities.Traces
{
    public class PropertyTrace : Entity<PropertyTraceId>
    {
        public string Name { get; private set; }
        public decimal Value { get; private set; }
        public DateTime SaledOnUtc { get; private set; }

        protected PropertyTrace() : base(default) { }
        private PropertyTrace(PropertyTraceId id, string name, decimal value, DateTime saledOnUtc) : base(id)
        {
            Name = name;
            Value = value;
            SaledOnUtc = saledOnUtc;
        }

        public static PropertyTrace Create(PropertyTraceId id, string name, decimal value, DateTime saledOnUtc)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            return new PropertyTrace(id, name, value, saledOnUtc);
        }
    }
}
