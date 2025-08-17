using FluentAssertions;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Owners;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Owners.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.BackEnd.Tests.Domain
{
    [TestFixture]
    public class PropertyOwnerTests
    {
        [Test]
        public void Constructor_WithValidData_ShouldCreateInstance()
        {
            // Arrange
            var bornOn = new DateTime(1990, 5, 20, 0, 0, 0, DateTimeKind.Utc);

            // Act
            var owner = PropertyOwner.Create(
                PropertyOwnerId.CreateUnique(),
                "Jane Doe",
                "456 Oak Ave",
                "jane-photo.jpg",
                bornOn);

            // Assert
            owner.Should().NotBeNull();
            owner.Name.Should().Be("Jane Doe");
            owner.Address.Should().Be("456 Oak Ave");
            owner.Photo.Should().Be("jane-photo.jpg");
            owner.BornOnUtc.Should().Be(bornOn);
        }

        [TestCase("")]
        [TestCase(" ")]
        public void Constructor_WithInvalidName_ShouldThrowArgumentException(string name)
        {
            // Act & Assert
            Action act = () => PropertyOwner.Create(PropertyOwnerId.CreateUnique(), name, "456 Oak Ave", "photo.jpg", DateTime.UtcNow);
            act.Should().Throw<ArgumentException>();
        }
    }
}
