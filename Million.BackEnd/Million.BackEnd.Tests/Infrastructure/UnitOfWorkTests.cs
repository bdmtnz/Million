using Microsoft.Extensions.Options;
using Million.BackEnd.Domain.Common.Contracts.Persistence;
using Million.BackEnd.Domain.PropertyAggregate;
using Million.BackEnd.Infrastructure.Common.Persistence;
using MongoDB.Driver;
using Moq;

namespace Million.BackEnd.Tests.Infrastructure
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        private Mock<IMongoClient> _mockClient;
        private Mock<IMongoDatabase> _mockDatabase;
        private Mock<IOptions<PersistenceSettings>> _mockSettings;
        private UnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            // 1. Mockea las dependencias
            _mockClient = new Mock<IMongoClient>();
            _mockDatabase = new Mock<IMongoDatabase>();
            _mockSettings = new Mock<IOptions<PersistenceSettings>>();

            // 2. Configura los mocks para simular el comportamiento
            var settings = new PersistenceSettings { Collection = "TestCollectionName" };
            _mockSettings.Setup(s => s.Value).Returns(settings);

            _mockClient.Setup(c => c.GetDatabase(
                It.Is<string>(dbName => dbName == settings.Collection),
                null
            )).Returns(_mockDatabase.Object);

            // 3. Inicializa la clase a probar con los mocks
            _unitOfWork = new UnitOfWork(_mockClient.Object, _mockSettings.Object);
        }

        [Test]
        public void GenericRepository_ShouldReturnInstanceOfGenericRepository()
        {
            // Act
            var repository = _unitOfWork.GenericRepository<Property>();

            // Assert
            Assert.That(repository, Is.Not.Null);
            Assert.That(repository, Is.InstanceOf<IGenericRepository<Property>>());
        }

        [Test]
        public void GenericRepository_ShouldCallGetDatabaseWithCorrectCollectionName()
        {
            // Act
            _unitOfWork.GenericRepository<Property>();

            // Assert
            _mockClient.Verify(
                c => c.GetDatabase(
                    It.Is<string>(dbName => dbName == "TestCollectionName"),
                    null
                ),
                Times.Once(),
                "GetDatabase debe ser llamado exactamente una vez con el nombre de la colección correcto."
            );
        }
    }
}
