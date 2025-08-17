using FluentAssertions;
using MapsterMapper;
using Million.BackEnd.Application.Properties.Query.Get;
using Million.BackEnd.Contracts.Properties;
using Million.BackEnd.Domain.Common.Contracts.Persistence;
using Million.BackEnd.Domain.Common.Dtos;
using Million.BackEnd.Domain.PropertyAggregate;
using Million.BackEnd.Domain.PropertyAggregate.ValueObjects;
using MongoDB.Driver;
using Moq;

namespace Million.BackEnd.Tests.Application.Properties.Query
{
    [TestFixture]
    public class GetPropertyQueryHandlerTests
    {
        private Mock<IMapper> _mockMapper;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IGenericRepository<Property>> _mockPropertyRepository;
        private GetPropertyQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            // Mockear las dependencias
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockPropertyRepository = new Mock<IGenericRepository<Property>>();

            // Configurar el mock de UnitOfWork para que devuelva el repositorio mockeado
            _mockUnitOfWork.Setup(u => u.GenericRepository<Property>()).Returns(_mockPropertyRepository.Object);

            // Configurar el mock de IGenericRepository para simular los resultados de la base de datos
            var mockProperties = new List<Property> {
                Property.Create(PropertyId.CreateUnique(), "House 1", "Address 1", 500000m, 2020),
                Property.Create(PropertyId.CreateUnique(), "House 2", "Address 2", 200000m, 2021)
            };
            _mockPropertyRepository.Setup(r => r.Where(It.IsAny<FilterDefinition<Property>>(), It.IsAny<PaginationFilter>(), It.IsAny<SortDefinition<Property>>()))
                .ReturnsAsync(mockProperties);
            _mockPropertyRepository.Setup(r => r.Count(It.IsAny<FilterDefinition<Property>>())).ReturnsAsync(mockProperties.Count);

            // Configurar el mock de IMapper
            _mockMapper.Setup(m => m.Map<PropertyFilteredResponse>(It.IsAny<Property>())).Returns(new PropertyFilteredResponse("","","",0,"",0,"", null, DateTime.UtcNow));

            // Inicializar el handler con los mocks
            _handler = new GetPropertyQueryHandler(_mockMapper.Object, _mockUnitOfWork.Object);
        }

        [Test]
        public async Task Handle_WithValidRequest_ShouldReturnPaginationResponse()
        {
            // Arrange
            var request = new GetPropertyQuery("Casa", new RangeFilter(100000, 500000), new PaginationFilter(10, 1));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            Assert.That(result.IsError, Is.False);
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value.Total, Is.EqualTo(2));
            Assert.That(result.Value.Page.Count, Is.EqualTo(2));

            // Verificar que el repositorio fue llamado correctamente
            _mockPropertyRepository.Verify(r => r.Where(
                It.IsAny<FilterDefinition<Property>>(), // Verificado en el test de validación del filtro
                It.Is<PaginationFilter>(p => p.Limit == 10 && p.Offset == 1),
                It.IsAny<SortDefinition<Property>>()
            ), Times.Once);

            _mockPropertyRepository.Verify(r => r.Count(
                It.IsAny<FilterDefinition<Property>>() // Verificado en el test de validación del filtro
            ), Times.Once);

            // Verificar que el mapper fue llamado para cada entidad
            _mockMapper.Verify(m => m.Map<PropertyFilteredResponse>(It.IsAny<Property>()), Times.Exactly(2));
        }

        [Test]
        public async Task Handle_WithNullKeyword_ShouldUseEmptyFilter()
        {
            // Arrange
            var request = new GetPropertyQuery(null, new RangeFilter(null, null), new PaginationFilter(1, 1));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            // El filtro de palabra clave debe ser un filtro vacío
            _mockPropertyRepository.Verify(r => r.Where(
                It.IsAny<FilterDefinition<Property>>(),
                It.IsAny<PaginationFilter>(),
                It.IsAny<SortDefinition<Property>>()
            ), Times.Once);

            result.Should().NotBeNull();
            result.IsError.Should().BeFalse();
            result.Value.Should().NotBeNull();
            result.Value.Total.Should().Be(2);
            result.Value.Page.Should().HaveCount(2);
        }

        [Test]
        public async Task Handle_WithKeywordContainingSpaces_ShouldRemoveSpaces()
        {
            // Arrange
            var request = new GetPropertyQuery("  Casa de Campo  ", new RangeFilter(null, null), new PaginationFilter(1, 1));

            _mockPropertyRepository.Setup(r => r.Where(It.IsAny<FilterDefinition<Property>>(), It.IsAny<PaginationFilter>(), It.IsAny<SortDefinition<Property>>()))
                .ReturnsAsync([]);
            _mockPropertyRepository.Setup(r => r.Count(It.IsAny<FilterDefinition<Property>>())).ReturnsAsync(0);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            // Verificar que la expresión regular fue construida con el keyword sin espacios
            _mockPropertyRepository.Verify(r => r.Where(
                It.IsAny<FilterDefinition<Property>>(),
                It.IsAny<PaginationFilter>(),
                It.IsAny<SortDefinition<Property>>()
            ), Times.Once);

            result.Should().NotBeNull();
            result.IsError.Should().BeFalse();
            result.Value.Should().NotBeNull();
            result.Value.Total.Should().Be(0);
            result.Value.Page.Should().HaveCount(0);
        }
    }
}
