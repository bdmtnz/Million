using Million.BackEnd.Domain.Common.Models;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Images.ValueObjects;

namespace Million.BackEnd.Domain.PropertyAggregate.Entities.Images
{
    public class PropertyImage : Entity<PropertyImageId>
    {
        public string File { get; private set; }
        public bool Enabled { get; private set; }

        protected PropertyImage() : base(default) { }
        private PropertyImage(PropertyImageId id, string file, bool enabled) : base(id)
        {
            File = file;
            Enabled = enabled;
        }

        public static PropertyImage Create(PropertyImageId id, string file)
        {
            return new PropertyImage(id, file, true);
        }

        public PropertyImage SetEnabled(bool enable)
        {
            Enabled = enable;
            return this;
        }
    }
}
