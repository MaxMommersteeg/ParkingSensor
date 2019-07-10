using System;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Devices;
using App.Core.DomainServices;
using App.Core.Messages.Commands;
using FakeItEasy;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests.Core.DomainServices
{
    [TestClass]
    public class DistanceMeasurementServiceTests
    {
        private ILogger<DistanceMeasurementService> _logger;
        private IMediator _messagingMediator;
        private IMeasureSensor _measureSensor;
        private DistanceMeasurementService _sut;

        private TimeSpan _measureInterval;
        private CancellationToken _defaultCancellationToken;

        [TestInitialize]
        public void Initialize()
        {
            _logger = A.Fake<ILogger<DistanceMeasurementService>>();
            _messagingMediator = A.Fake<IMediator>();
            _measureSensor = A.Fake<IMeasureSensor>();
            _sut = new DistanceMeasurementService(_logger, _messagingMediator, _measureSensor);

            _measureInterval = TimeSpan.FromSeconds(1);
            _defaultCancellationToken = CancellationToken.None;
        }

        [TestMethod]
        public async Task Handle_StartDistanceMeasurement_starts_distance_measuring()
        {
            // Arrange
            var startDistanceMeasurement = new StartDistanceMeasurement(_measureInterval);

            // Act
            await _sut.Handle(startDistanceMeasurement, _defaultCancellationToken);

            // Assert
            Assert.IsTrue(_sut.Started);
        }

        [TestMethod]
        public async Task Handle_StopDistanceMeasurement_stops_distance_measuring()
        {
            // Arrange
            var startDistanceMeasurement = new StartDistanceMeasurement(_measureInterval);
            await _sut.Handle(startDistanceMeasurement, _defaultCancellationToken);

            var stopDistanceMeasurement = new StopDistanceMeasurement();

            // Act
            await _sut.Handle(stopDistanceMeasurement, _defaultCancellationToken);

            // Assert
            Assert.IsFalse(_sut.Started);
        }

        [TestMethod]
        public void Dispose_disposes_measure_sensor()
        {
            // Arrange, Act
            _sut.Dispose();

            // Assert
            A.CallTo(() => _measureSensor.Dispose())
                .MustHaveHappenedOnceExactly();
        }
    }
}
