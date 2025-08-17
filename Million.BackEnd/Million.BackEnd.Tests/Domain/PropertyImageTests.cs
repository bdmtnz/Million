using FluentAssertions;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Images;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Images.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.BackEnd.Tests.Domain
{
    [TestFixture]
    public class PropertyImageTests
    {
        [Test]
        public void Constructor_WithValidFile_ShouldCreateEnabledImage()
        {
            // Arrange & Act
            var image = PropertyImage.Create(PropertyImageId.CreateUnique(), "house.jpg");

            // Assert
            image.Should().NotBeNull();
            image.File.Should().Be("house.jpg");
            image.Enabled.Should().BeTrue();
        }

        [TestCase("")]
        [TestCase(" ")]
        public void Constructor_WithInvalidFile_ShouldThrowArgumentException(string file)
        {
            // Act & Assert
            Action act = () => PropertyImage.Create(PropertyImageId.CreateUnique(), file);
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Disable_ShouldSetEnabledToFalse()
        {
            // Arrange
            var image = PropertyImage.Create(PropertyImageId.CreateUnique(), "house.jpg");

            // Act
            image.SetEnabled(false);

            // Assert
            image.Enabled.Should().BeFalse();
        }
    }
}
