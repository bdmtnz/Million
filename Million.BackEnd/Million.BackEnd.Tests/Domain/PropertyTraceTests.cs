using FluentAssertions;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Traces;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Traces.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.BackEnd.Tests.Domain
{
    [TestFixture]
    public class PropertyTraceTests
    {
        [Test]
        public void Constructor_WithValidData_ShouldCreateInstance()
        {
            // Arrange
            var saledOn = new DateTime(2023, 10, 15, 0, 0, 0, DateTimeKind.Utc);

            // Act
            var trace = PropertyTrace.Create(PropertyTraceId.CreateUnique(), "Sale Event", 350000m, saledOn);

            // Assert
            trace.Should().NotBeNull();
            trace.Name.Should().Be("Sale Event");
            trace.Value.Should().Be(350000m);
            trace.SaledOnUtc.Should().Be(saledOn);
        }

        [TestCase("")]
        [TestCase(" ")]
        public void Constructor_WithInvalidName_ShouldThrowArgumentException(string name)
        {
            // Act & Assert
            Action act = () => PropertyTrace.Create(PropertyTraceId.CreateUnique(), name, 350000m, DateTime.UtcNow);
            act.Should().Throw<ArgumentException>();
        }
    }
}
