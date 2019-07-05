using System.Linq;
using App.Core.Entities;
using App.Core.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests.Core
{
    [TestClass]
    public class DistanceMeasurementTests
    {
        private const double Distance = 15;

        [TestMethod]
        public void SetsIsMeasuredToTrue()
        {
            // Arrange
            var measurement = new DistanceMeasurement(Distance);

            // Act
            measurement.MarkMeasured();

            // Assert
            Assert.IsTrue(measurement.IsMeasured);
        }

        [TestMethod]
        public void MarkMeasured_RaisesDistanceMeasured()
        {
            // Arrange
            var measurement = new DistanceMeasurement(Distance);

            // Act
            measurement.MarkMeasured();

            // Assert
            Assert.AreEqual(1, measurement.Events.Count);

            var distanceMeasured = (DistanceMeasured)measurement.Events.First();
            Assert.IsNotNull(distanceMeasured);
            Assert.AreEqual(Distance, distanceMeasured.DistanceMeasurement.Distance);
        }
    }
}
