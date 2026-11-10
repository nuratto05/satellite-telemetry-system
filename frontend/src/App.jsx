import Earth from "./components/Earth";
import TelemetryTable from "./components/TelemetryTable";

import useSatellite from "./hooks/useSatellites";
import useTelemetry from "./hooks/TelemetriesHook";

function App() {

    const telemetries = useTelemetry();
    const satellites = useSatellite();

    return (
        <section className="bg-black flex h-screen flex-col gap-4 p-4">

            <div className="relative min-h-0 flex-1">
                <div className="absolute inset-0">
                    <Earth />
                </div>
            </div>

            <TelemetryTable telemetries={telemetries} satellites={satellites} />

        </section>
    );
}

export default App;