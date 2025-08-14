using Million.BackEnd.Domain.Common.Contracts;
using Million.BackEnd.Domain.Common.Contracts.Persistence;

namespace Million.BackEnd.Domain.Common.Models
{
    public class AggregateRoot<AId> : Entity<AId>, IAggregateRoot, ISoftDeletable where AId : ValueObject
    {
        private DateTime? _deletedOnUtc = null;
        public DateTime? DeletedOnUtc
        {
            get
            {
                return _deletedOnUtc;
            }
            set
            {
                _deletedOnUtc = _deletedOnUtc ?? value;
            }
        }

        public void ApplySoftDelete()
        {
            DeletedOnUtc = DateTime.UtcNow;
        }

        public AggregateRoot(AId id) : base(id) { }
    }
}
