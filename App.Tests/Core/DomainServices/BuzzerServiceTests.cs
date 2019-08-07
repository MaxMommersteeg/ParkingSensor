using System.Threading;
using System.Threading.Tasks;
using App.Core.Devices;
using App.Core.DomainServices;
using App.Core.Messages.Commands;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests.Core.DomainServices
{
    [TestClass]
    public class BuzzerServiceTests
    {
        private ILogger<BuzzerService> _logger;
        private IBuzzer _buzzer;
        private BuzzerService _sut;

        private double _frequency;
        private CancellationToken _defaultCancellationToken;

        [TestInitialize]
        public void Initialize()
        {
            _logger = A.Fake<ILogger<BuzzerService>>();
            _buzzer = A.Fake<IBuzzer>();
            _sut = new BuzzerService(_logger, _buzzer);

            _frequency = 15;
            _defaultCancellationToken = CancellationToken.None;
        }

        [TestMethod]
        public async Task Handle_StartPlayingSoundEffect_starts_playing_sound_effect()
        {
            // Arrange
            var command = new StartPlayingSoundEffect(_frequency);

            // Act
            await _sut.Handle(command, _defaultCancellationToken);

            // Assert
            A.CallTo(() => _buzzer.StartPlaying(_frequency))
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public async Task Handle_StopPlayingSoundEffect_stops_playing_sound_effect()
        {
            // Arrange
            var command = new StopPlayingSoundEffect();

            // Act
            await _sut.Handle(command, _defaultCancellationToken);

            // Assert
            A.CallTo(() => _buzzer.StopPlaying())
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void Dispose_disposes_buzzer()
        {
            // Arrange, Act
            _sut.Dispose();

            // Assert
            A.CallTo(() => _buzzer.Dispose())
                .MustHaveHappenedOnceExactly();
        }
    }
}
