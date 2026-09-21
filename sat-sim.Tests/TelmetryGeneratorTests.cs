using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sat_sim.TelemetryTests
{
    public class TelmetryGeneratorTests
    {

        TelemetryGenerator generateTelemetry = new TelemetryGenerator();

        [Fact]
        public void Throw_Exception_When_Satellite_is_Null() {

            Assert.Throws<ArgumentNullException>(() =>
            {
                generateTelemetry.generate(null);
            });
        }

        [Fact]
        public void Generate_Telemetry_When_Satellite_is_Valid()
        {

            Mission mission = new Mission(1, "explorer", "test");
            Satellite satellite = new Satellite(1, "Satellite-1", mission, "US");

            Telemetry tel = generateTelemetry.generate(satellite);

            Assert.NotNull(tel);
             
        }
    }
}
 