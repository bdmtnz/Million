using FluentAssertions;
using Million.BackEnd.Application.Properties.Query.GetById;
using Million.BackEnd.Domain.Common.Contracts.Persistence;
using Million.BackEnd.Domain.PropertyAggregate;
using Million.BackEnd.Domain.PropertyAggregate.ValueObjects;
using Moq;

namespace Million.BackEnd.Tests.Application.Properties.Query
{
    [TestFixture]
    public class GetpropertyByIdQueryHandlerTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IGenericRepository<Property>> _mockPropertyRepository;
        private GetpropertyByIdQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            // Mockear las dependencias
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockPropertyRepository = new Mock<IGenericRepository<Property>>();

            // Configurar el mock de IUnitOfWork para que devuelva el repositorio mockeado
            _mockUnitOfWork.Setup(u => u.GenericRepository<Property>())
                           .Returns(_mockPropertyRepository.Object);

            // Inicializar el manejador con los mocks
            _handler = new GetpropertyByIdQueryHandler(_mockUnitOfWork.Object);
        }

        [Test]
        public async Task Handle_WithValidId_ShouldReturnPropertyResponse()
        {
            // Arrange
            var propertyId = PropertyId.CreateUnique();
            var propertyName = "House 1";
            var property = Property.Create(propertyId, propertyName, "Address 1", 500000m, 2020);
            var query = new GetpropertyByIdQuery(propertyId.ToString());

            // Configurar el repositorio para que devuelva la propiedad
            _mockPropertyRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Property, bool>>>()))
                                   .ReturnsAsync(property);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            Assert.That(result.IsError, Is.False);
            Assert.That(result.Value.Id, Is.EqualTo(propertyId.ToString()));
            Assert.That(result.Value.Name, Is.EqualTo(propertyName));
        }

        [Test]
        public async Task Handle_WithInvalidId_ShouldReturnNotFoundError()
        {
            // Arrange
            var propertyId = PropertyId.CreateUnique();
            var query = new GetpropertyByIdQuery(propertyId.ToString());

            // Configurar el repositorio para que devuelva null (no se encontró la propiedad)
            _mockPropertyRepository.Setup(r => r.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Property, bool>>>()))
                                   .ReturnsAsync(null as Property);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            Assert.That(result.IsError, Is.True);
            Assert.That(result.FirstError.Description, Is.EqualTo("Property not found"));
        }
    }
}
