using App.Core;
using App.Core.DomainServices;
using App.Core.Messages.Commands;
using App.Core.Messages.Events;
using FakeItEasy;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace App.Tests.Core.DomainServices
{
    [TestClass]
    public class ParkingSensorServiceTests
    {
        private ILogger<ParkingSensorService> _logger;
        private IMediator _messagingMediator;
        private IDistanceToToneFrequencyConverter _distanceToToneFrequencyConverter;
        private ParkingSensorService _sut;

        private double _distance;
        private double _frequency;
        private CancellationToken _defaultCancellationToken;

        [TestInitialize]
        public void Initialize()
        {
            _logger = A.Fake<ILogger<ParkingSensorService>>();
            _messagingMediator = A.Fake<IMediator>();
            _distanceToToneFrequencyConverter = A.Fake<IDistanceToToneFrequencyConverter>();
            _sut = new ParkingSensorService(_logger, _messagingMediator, _distanceToToneFrequencyConverter);

            _distance = 15;
            _frequency = 505; 
            _defaultCancellationToken = CancellationToken.None;
        }

        [TestMethod]
        public async Task Handle_DistanceMeasured_requests_a_tone_being_played()
        {
            // Arrange
            A.CallTo(() => _distanceToToneFrequencyConverter.DistanceToFrequency(A<double>._))
                .Returns(_frequency);
            var distanceMeasured = new DistanceMeasured(_distance);

            // Act
            await _sut.Handle(distanceMeasured, _defaultCancellationToken);

            // Assert
            A.CallTo(() => _messagingMediator.Send(A<PlayTone>.That.Matches(x => x.Frequency == _frequency), A<CancellationToken>._))
                .MustHaveHappenedOnceExactly();
        }
    }
}
