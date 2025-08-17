using ErrorOr;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Million.BackEnd.Api.Controllers;
using Million.BackEnd.Application.Properties.Query.Get;
using Million.BackEnd.Contracts.Common;
using Million.BackEnd.Contracts.Properties;
using Moq;

namespace Million.BackEnd.Tests.Api.Properties
{
    [TestFixture]
    public class GetPropertyTests
    {
        private Mock<IMediator> _mockMediator;
        private PropertyController _controller;

        [SetUp]
        public void Setup()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new PropertyController(_mockMediator.Object);
        }

        [Test]
        public async Task Get_WithValidParameters_ShouldReturnOkResult()
        {
            // Arrange
            var keyword = "house";
            var limit = 10;
            var offset = 1;
            var from = 100000m;
            var to = 500000m;

            var mockResponse = new PaginationResponse<List<PropertyFilteredResponse>>(0, []);

            _mockMediator.Setup(m => m.Send(
                It.IsAny<GetPropertyQuery>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(mockResponse);

            // Act
            var result = await _controller.Get(keyword, limit, offset, from, to);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be(mockResponse);

            // Verificar que el mediador fue llamado con la consulta correcta
            _mockMediator.Verify(m => m.Send(
                It.Is<GetPropertyQuery>(q =>
                    q.Keyword == keyword &&
                    q.Pagination.Limit == limit &&
                    q.Pagination.Offset == offset),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }

        [Test]
        public async Task Get_WithMediatorError_ShouldReturnProblemResult()
        {
            // Arrange
            var keyword = "invalid";
            var limit = 10;
            var offset = 1;
            var from = 100000m;
            var to = 500000m;

            var error = Error.NotFound(description: "No properties found.");

            _mockMediator.Setup(m => m.Send(
                It.IsAny<GetPropertyQuery>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(error);

            // Act
            var result = await _controller.Get(keyword, limit, offset, from, to);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var problemResult = result as ObjectResult;
            problemResult.Value.Should().BeOfType<ProblemDetails>();
            problemResult.Value.As<ProblemDetails>().Title.Should().Be("No properties found.");
        }
    }
}
