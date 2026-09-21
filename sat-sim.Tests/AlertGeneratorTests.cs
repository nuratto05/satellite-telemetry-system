using System;
using Xunit;
 
namespace sat_sim.AlertTests
{
    public class AlertGeneratorTests
    {
        AlertGenerator genarateAlert = new AlertGenerator();

        // BASE CASE : LONG : 0.0 || LAT : 0.0 || ALT : 500 || VEL : 20000 || TEMP : 50 || BAT : 100 - PASS NORMAL TEL

        [Fact]
        public void Null_For_Normal_Telemetry()
        {
            Telemetry tel = new Telemetry(1, 1, DateTime.UtcNow, 0.0, 0.0, 500, 20000, 50, 100);

            Alert alert = genarateAlert.Generate(tel);

            Assert.Null(alert);
        }

        [Fact]
        public void Severe_When_Velocity_Below_16000()
        {
            Telemetry tel = new Telemetry(2, 1, DateTime.UtcNow, 0.0, 0.0, 500, 25, 50, 100);

            Alert alert = genarateAlert.Generate(tel);

            Assert.NotNull(alert);
            Assert.Equal(SeverityStatus.Severe, alert.VelocityLevel);
            Assert.Equal(SeverityStatus.Severe, alert.Severity);
        }

        [Fact]
        public void Critical_When_Battery_Below_30()
        {
            Telemetry tel = new Telemetry(3, 1, DateTime.UtcNow, 0.0, 0.0, 500, 20000, 50, 0);

            Alert alert = genarateAlert.Generate(tel);

            Assert.NotNull(alert);
            Assert.Equal(SeverityStatus.Critical, alert.BatteryLevel);
            Assert.Equal(SeverityStatus.Critical, alert.Severity);
        }

        [Fact]
        public void Severe_When_Approaching_Dangerous_Temperature_Levels()
        {
            Telemetry tel = new Telemetry(4, 1, DateTime.UtcNow, 0.0, 0.0, 500, 20000, 80, 100);

            Alert alert = genarateAlert.Generate(tel);

            Assert.NotNull(alert);
            Assert.Equal(SeverityStatus.Severe, alert.TemperatureLevel);
            Assert.Equal(SeverityStatus.Severe, alert.Severity);
        }

        [Fact]
        public void Severe_Critical_Aggregates_To_Highest_Severity()
        {
            // Velocity -> Severe
            // Battery -> Critical
            // aggregate should be Critical
            Telemetry tel = new Telemetry(5, 1, DateTime.UtcNow, 0.0, 0.0, 500, 10000, 50, 0);

            Alert alert = genarateAlert.Generate(tel);

            Assert.NotNull(alert);
            Assert.Equal(SeverityStatus.Severe, alert.VelocityLevel);
            Assert.Equal(SeverityStatus.Critical, alert.BatteryLevel);
            Assert.Equal(SeverityStatus.Critical, alert.Severity);
        }
    }
}
