using System;
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
        private TimeSpan _duration;
        private CancellationToken _defaultCancellationToken;

        [TestInitialize]
        public void Initialize()
        {
            _logger = A.Fake<ILogger<BuzzerService>>();
            _buzzer = A.Fake<IBuzzer>();
            _sut = new BuzzerService(_logger, _buzzer);

            _frequency = 15;
            _duration = TimeSpan.FromSeconds(1);
            _defaultCancellationToken = CancellationToken.None;
        }

        [TestMethod]
        public async Task Handle_PlayTone_plays_tone()
        {
            // Arrange
            var playTone = new PlayTone(_frequency, _duration);

            // Act
            await _sut.Handle(playTone, _defaultCancellationToken);

            // Assert
            A.CallTo(() => _buzzer.PlaySound(_frequency, _duration))
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
