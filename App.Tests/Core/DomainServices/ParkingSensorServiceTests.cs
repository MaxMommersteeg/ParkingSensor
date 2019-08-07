using System.Threading;
using System.Threading.Tasks;
using App.Core;
using App.Core.DomainServices;
using App.Core.Messages.Commands;
using App.Core.Messages.Events;
using App.Core.Model;
using FakeItEasy;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests.Core.DomainServices
{
    [TestClass]
    public class ParkingSensorServiceTests
    {
        private ILogger<ParkingSensorService> _logger;
        private IMediator _messagingMediator;
        private IDistanceToSoundEffectConverter _distanceToSoundEffectConverter;
        private ParkingSensorService _sut;

        private double _distance;
        private double _frequency;
        private CancellationToken _defaultCancellationToken;

        [TestInitialize]
        public void Initialize()
        {
            _logger = A.Fake<ILogger<ParkingSensorService>>();
            _messagingMediator = A.Fake<IMediator>();
            _distanceToSoundEffectConverter = A.Fake<IDistanceToSoundEffectConverter>();
            _sut = new ParkingSensorService(_logger, _messagingMediator, _distanceToSoundEffectConverter);

            _distance = 15;
            _frequency = 505;
            _defaultCancellationToken = CancellationToken.None;
        }

        [TestMethod]
        public async Task Handle_DistanceMeasured_requests_a_sound_effect_being_played()
        {
            // Arrange
            A.CallTo(() => _distanceToSoundEffectConverter.DistanceToSoundEffect(A<double>._))
                .Returns(new SoundEffect(_frequency));
            var distanceMeasured = new DistanceMeasured(_distance);

            // Act
            await _sut.Handle(distanceMeasured, _defaultCancellationToken);

            // Assert
            A.CallTo(() => _messagingMediator.Send(A<StartPlayingSoundEffect>.That.Matches(x => x.Frequency == _frequency), A<CancellationToken>._))
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public async Task Handle_DistanceMeasured_requests_a_played_sound_effect_being_stopped()
        {
            // Arrange
            A.CallTo(() => _distanceToSoundEffectConverter.DistanceToSoundEffect(A<double>._))
                .Returns(null);
            var distanceMeasured = new DistanceMeasured(_distance);

            // Act
            await _sut.Handle(distanceMeasured, _defaultCancellationToken);

            // Assert
            A.CallTo(() => _messagingMediator.Send(A<StopPlayingSoundEffect>._, A<CancellationToken>._))
                .MustHaveHappenedOnceExactly();
        }
    }
}
