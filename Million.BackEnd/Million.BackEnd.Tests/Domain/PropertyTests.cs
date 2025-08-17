using Million.BackEnd.Domain.PropertyAggregate;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Images;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Images.ValueObjects;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Owners;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Owners.ValueObjects;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Traces;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Traces.ValueObjects;
using Million.BackEnd.Domain.PropertyAggregate.ValueObjects;
using FluentAssertions;

namespace Million.BackEnd.Tests.Domain
{
    public class PropertyTests
    {
        private PropertyId _propertyId;
        private PropertyOwner _owner;
        private PropertyImage _image;
        private PropertyTrace _trace;

        [SetUp]
        public void Setup()
        {
            _propertyId = PropertyId.CreateUnique();
            _owner = PropertyOwner.Create(
                PropertyOwnerId.CreateUnique(),
                "John Doe",
                "123 Main St",
                "photo-url.jpg",
                new DateTime(1985, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            _image =  PropertyImage.Create(PropertyImageId.CreateUnique(), "house-photo.jpg");
            _trace = PropertyTrace.Create(PropertyTraceId.CreateUnique(), "Sale", 250000m, DateTime.UtcNow);
        }

        [Test]
        public void Constructor_WithValidData_ShouldCreateInstance()
        {
            // Arrange & Act
            var property = Property.Create(_propertyId, "House 1", "Address 1", 500000m, 2020)
                .SetOwner(_owner.Id, _owner.Name, _owner.Address, _owner.Photo, _owner.BornOnUtc)
                .SetImage(_image.Id, _image.File)
                .SetTrace(_trace.Id, _trace.Name, _trace.Value, _trace.SaledOnUtc);

            // Assert
            property.Should().NotBeNull();
            property.Id.Should().Be(_propertyId);
            property.Name.Should().Be("House 1");
            property.Address.Should().Be("Address 1");
            property.Price.Should().Be(500000m);
            property.Year.Should().Be(2020);
            property.Owner.Should().Be(_owner);
            property.Image.Should().Be(_image);
            property.Trace.Should().Be(_trace);
            property.CreatedOnUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            property.UpdatedOnUtc.Should().BeNull();
        }

        [Test]
        public void Constructor_WithoutTrace_ShouldCreateInstance()
        {
            // Arrange & Act
            var property = Property.Create(_propertyId, "House 1", "Address 1", 500000m, 2020)
                .SetOwner(_owner.Id, _owner.Name, _owner.Address, _owner.Photo, _owner.BornOnUtc)
                .SetTrace(_trace.Id, _trace.Name, _trace.Value, _trace.SaledOnUtc);

            // Assert
            property.Should().NotBeNull();
            property.Id.Should().Be(_propertyId);
            property.Name.Should().Be("House 1");
            property.Address.Should().Be("Address 1");
            property.Price.Should().Be(500000m);
            property.Year.Should().Be(2020);
            property.Owner.Should().Be(_owner);
            property.Image.Should().BeNull();
            property.Trace.Should().Be(_trace);
            property.CreatedOnUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            property.UpdatedOnUtc.Should().BeNull();
        }

        [Test]
        public void Constructor_WithoutImage_ShouldCreateInstance()
        {
            // Arrange & Act
            var property = Property.Create(_propertyId, "House 1", "Address 1", 500000m, 2020)
                .SetOwner(_owner.Id, _owner.Name, _owner.Address, _owner.Photo, _owner.BornOnUtc)
                .SetImage(_image.Id, _image.File);

            // Assert
            property.Should().NotBeNull();
            property.Id.Should().Be(_propertyId);
            property.Name.Should().Be("House 1");
            property.Address.Should().Be("Address 1");
            property.Price.Should().Be(500000m);
            property.Year.Should().Be(2020);
            property.Owner.Should().Be(_owner);
            property.Image.Should().Be(_image);
            property.Trace.Should().BeNull();
            property.CreatedOnUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            property.UpdatedOnUtc.Should().BeNull();
        }

        [TestCase("")]
        [TestCase(" ")]
        public void Constructor_WithInvalidName_ShouldThrowArgumentException(string name)
        {
            // Act & Assert
            Action act = () => Property.Create(_propertyId, name, "Address 1", 500000m, 2020);
            act.Should().Throw<ArgumentException>();
        }
    }
}
