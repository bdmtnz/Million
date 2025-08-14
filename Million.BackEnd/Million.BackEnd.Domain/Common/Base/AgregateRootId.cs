namespace Million.BackEnd.Domain.Common.Models
{
    public abstract class AggregateRootId<AId> : ValueObject
    {
        public abstract AId Value { get; protected set; }
    }
}
