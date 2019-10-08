using System.Threading;
using System.Threading.Tasks;
using App.Core.Devices;
using App.Core.DomainServices;
using App.Core.Messages.Commands;
using App.Core.Messages.Events;
using FakeItEasy;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests.Core.DomainServices
{
    [TestClass]
    public class MotionDetectionServiceTests
    {
        private ILogger<MotionDetectionService> _logger;
        private IMediator _mediator;
        private IMotionDetectionSensor _motionDetectionSensor;
        private MotionDetectionService _sut;

        private CancellationToken _defaultCancellationToken;

        [TestInitialize]
        public void Initialize()
        {
            _logger = A.Fake<ILogger<MotionDetectionService>>();
            _mediator = A.Fake<IMediator>();
            _motionDetectionSensor = A.Fake<IMotionDetectionSensor>();
            _sut = new MotionDetectionService(_logger, _mediator, _motionDetectionSensor);

            _defaultCancellationToken = CancellationToken.None;
        }

        [TestMethod]
        public async Task Handle_StartMotionDetection_starts_listening_for_motion_detections()
        {
            // Arrange
            var startMotionDetection = new StartMotionDetection();

            // Act
            await _sut.Handle(startMotionDetection, _defaultCancellationToken);

            // Assert
            Assert.IsTrue(_sut.Started);
        }

        [TestMethod]
        public async Task Handle_MotionDetected_requests_a_sound_effect_being_played()
        {
            // Arrange
            A.CallTo(() => _sut.Started).Returns(true);

            var motionDetected = new MotionDetected();

            // Act
            await _sut.Handle(motionDetected, _defaultCancellationToken);

            // Assert
            A.CallTo(() => _mediator.Send(A<StartPlayingSoundEffect>._, A<CancellationToken>._))
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public async Task Handle_MotionStopped_requests_a_played_sound_effect_being_stopped()
        {
            // Arrange
            A.CallTo(() => _sut.Started).Returns(true);

            var motionStopped = new MotionStopped();

            // Act
            await _sut.Handle(motionStopped, _defaultCancellationToken);

            // Assert
            A.CallTo(() => _mediator.Send(A<StopPlayingSoundEffect>._, A<CancellationToken>._))
                .MustHaveHappenedOnceExactly();
        }
    }
}
