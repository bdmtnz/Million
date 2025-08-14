using Million.BackEnd.Domain.Common.Contracts.Persistence;
using Million.BackEnd.Domain.Common.Models;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Images;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Owners;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Traces;
using Million.BackEnd.Domain.PropertyAggregate.ValueObjects;


namespace Million.BackEnd.Domain.PropertyAggregate
{
    public partial class Property : AggregateRoot<PropertyId>, IAuditable
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public decimal Price { get; private set; }
        public string Code { get; private set; }
        public int Year { get; private set; }
        public PropertyOwner Owner { get; private set; }
        public PropertyImage? Image { get; private set; }
        public PropertyTrace? Trace { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? UpdatedOnUtc { get; private set; }
    }
}
