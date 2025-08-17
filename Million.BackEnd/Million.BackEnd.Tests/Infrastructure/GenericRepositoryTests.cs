using FluentAssertions;
using Million.BackEnd.Domain.PropertyAggregate;
using Million.BackEnd.Domain.PropertyAggregate.ValueObjects;
using Million.BackEnd.Infrastructure.Common.Persistence;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.BackEnd.Tests.Infrastructure
{
    [TestFixture]
    public class GenericRepositoryTests
    {
        private Mock<IMongoDatabase> _mockContext;
        private Mock<IMongoCollection<Property>> _mockDbSet;
        private GenericRepository<Property> _repository;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<IMongoDatabase>();
            _mockDbSet = new Mock<IMongoCollection<Property>>();

            // Configura el mock para que GetCollection devuelva nuestro _mockDbSet
            _mockContext.Setup(c => c.GetCollection<Property>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
                .Returns(_mockDbSet.Object);

            // Configura el mock del DbSet para simular datos y consultas
            var dummyData = new List<Property>
        {
            new Property { Id = 1 },
            new Property { Id = 2 },
            new Property { Id = 3 },
        }.AsQueryable();

            var mockAsyncCursor = new Mock<IAsyncCursor<Property>>();
            mockAsyncCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
                           .Returns(Task.FromResult(true))
                           .Returns(Task.FromResult(false));
            mockAsyncCursor.Setup(x => x.Current).Returns(dummyData);

            _mockDbSet.As<IQueryable<Property>>().Setup(x => x.Provider).Returns(dummyData.Provider);
            _mockDbSet.As<IQueryable<Property>>().Setup(x => x.Expression).Returns(dummyData.Expression);
            _mockDbSet.As<IQueryable<Property>>().Setup(x => x.ElementType).Returns(dummyData.ElementType);
            _mockDbSet.As<IQueryable<Property>>().Setup(x => x.GetEnumerator()).Returns(dummyData.GetEnumerator());

            // Mock para el método ToListAsync en IQueryable
            _mockDbSet.As<IMongoQueryable<Property>>()
                .Setup(x => x.ToListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(dummyData.ToList());

            // Inicializa el repositorio con los mocks
            _repository = new GenericRepository<Property>(_mockContext.Object);
        }

        [Test]
        public async Task FirstOrDefaultAsync_WithPredicate_ShouldReturnFirstMatchingEntity()
        {
            // Act
            var result = await _repository.FirstOrDefaultAsync(e => e.Id == 2);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(2);
        }

        [Test]
        public async Task Where_WithPredicateAndNoPaginationOrOrderBy_ShouldReturnAllMatchingEntities()
        {
            // Act
            var result = await _repository.Where(e => e.Id > 0);

            // Assert
            result.Count().Should().Be(3);
        }

        [Test]
        public async Task Count_WithoutPredicate_ShouldReturnTotalCount()
        {
            // Act
            var count = await _repository.Count();

            // Assert
            count.Should().Be(3);
        }

        // Pruebas para el método Where con FilterDefinition y SortDefinition
        [Test]
        public async Task Where_WithFilterDefinition_ShouldReturnFilteredEntities()
        {
            // Arrange
            var mockFindFluent = new Mock<IFindFluent<Property, Property>>();
            var findOptions = new FindOptions<Property, Property>();

            _mockDbSet.Setup(x => x.Find(It.IsAny<FilterDefinition<Property>>(), It.IsAny<FindOptions>()))
                      .Returns(mockFindFluent.Object);
            mockFindFluent.Setup(x => x.ToListAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new List<Property> { Property.Create(PropertyId.CreateUnique(), "name", "address", 100000, 2023) });

            // Act
            var result = await _repository.Where(Builders<Property>.Filter.Eq(x => x.Id, 2));

            // Assert
            result.Count().Should().Be(1);
            result.First().Id.Should().Be(2);

            // Verifica que el método Find fue llamado con el filtro correcto
            _mockDbSet.Verify(x => x.Find(It.IsAny<FilterDefinition<Property>>(), It.IsAny<FindOptions>()), Times.Once());
        }
    }
}
