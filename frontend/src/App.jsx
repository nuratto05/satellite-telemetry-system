import Earth from "./components/Earth";
import TelemetryTable from "./components/TelemetryTable";
import BottomPanel from "./components/BottomPanel";

import useSatellite from "./hooks/useSatellites";
import useTelemetry from "./hooks/TelemetriesHook";
import useAlerts from "./hooks/AlertHook";

function App() {

    const telemetries = useTelemetry();
    const satellites = useSatellite();
    const alerts = useAlerts();

    return (
        <section className="bg-black flex h-screen flex-col gap-4 p-4">

            <div className="relative min-h-0 flex-1">
                <div className="absolute inset-0">
                    <Earth />
                </div>
            </div>

            <BottomPanel telemetries={telemetries} alerts={alerts} satellites={satellites} />

        </section>
    );
}

export default App;