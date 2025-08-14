using Million.BackEnd.Domain.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.BackEnd.Domain.Common.Models
{
    public abstract partial class Entity<EId> : IEntity<EId>, IEquatable<Entity<EId>>
        where EId : ValueObject
    {

        [Obsolete("Only for reflection", true)]
        protected Entity() { }

        public override bool Equals(object? obj)
        {
            return obj is Entity<EId> entity && Id.Equals(entity.Id);
        }

        public bool Equals(Entity<EId>? other)
        {
            return Equals((object?)other);
        }

        public static bool operator ==(Entity<EId> left, Entity<EId> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<EId> left, Entity<EId> right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
