using ErrorOr;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Million.BackEnd.Api.Controllers;
using Million.BackEnd.Application.Properties.Query.GetById;
using Million.BackEnd.Contracts.Properties;
using Million.BackEnd.Domain.PropertyAggregate.ValueObjects;
using Moq;

namespace Million.BackEnd.Tests.Api.Properties
{
    [TestFixture]
    public class PropertyControllerGetByIdTests
    {
        private Mock<IMediator> _mockMediator;
        private PropertyController _controller;

        [SetUp]
        public void Setup()
        {
            // Mockear la dependencia de IMediator
            _mockMediator = new Mock<IMediator>();

            // Inicializar el controlador con el mock
            _controller = new PropertyController(_mockMediator.Object);
        }

        [Test]
        public async Task GetById_WithValidId_ShouldReturnOkResult()
        {
            // Arrange
            var propertyId = PropertyId.CreateUnique();
            var mockResponse = new PropertyResponse(propertyId.ToString(), "", "", 0, "", 0, null, null, null, DateTime.UtcNow);

            _mockMediator.Setup(m => m.Send(
                It.Is<GetpropertyByIdQuery>(q => q.Id == propertyId.ToString()),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(mockResponse);

            // Act
            var result = await _controller.GetById(propertyId.ToString());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(mockResponse));

            // Verificar que el mediador fue llamado con la consulta correcta
            _mockMediator.Verify(m => m.Send(
                It.Is<GetpropertyByIdQuery>(q => q.Id == propertyId.ToString()),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }

        [Test]
        public async Task GetById_WithInvalidId_ShouldReturnProblemResult()
        {
            // Arrange
            var propertyId = Guid.NewGuid().ToString();
            var error = Error.NotFound(description: "Property not found.");

            _mockMediator.Setup(m => m.Send(
                It.Is<GetpropertyByIdQuery>(q => q.Id == propertyId),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(error);

            // Act
            var result = await _controller.GetById(propertyId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var problemResult = result as ObjectResult;
            Assert.That(problemResult.StatusCode, Is.EqualTo(404));
        }
    }
}
