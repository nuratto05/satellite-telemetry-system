import { useState } from 'react';

import Earth from "./components/Earth";
import BottomPanel from "./components/BottomPanel";
import SatellitesPanel from "./components/SatellitesPanel";
import SatelliteInfoBox from './components/SatelliteInfoBox';
import useSatellite from "./hooks/useSatellites";
import useTelemetry from "./hooks/TelemetriesHook";
import useAlerts from "./hooks/AlertHook";
import useMission from "./hooks/useMissions";

function App() {
    const telemetries = useTelemetry();
    const satellites = useSatellite();
    const alerts = useAlerts();
    const missions = useMission();

    const [satFilter, setSatFilter] = useState("");
    const [selected, setSelected] = useState(null);

    return (
        <section className="flex h-screen flex-col bg-black text-white">
            <div className="relative min-h-0 flex-1">
                <div className="absolute inset-0 z-0">
                    <Earth />
                </div>

                <header className="pointer-events-none absolute inset-x-0 top-0 flex items-start justify-between gap-4 p-4">
                    <SatellitesPanel satellites={satellites} telemetries={telemetries} alerts={alerts} onSelect={setSelected}/>
                </header>
            </div>

            <BottomPanel telemetries={telemetries} alerts={alerts} satellites={satellites} satFilter={satFilter} />
            {selected && (
                <SatelliteInfoBox satellite={selected} missions={missions} onClose={() => setSelected(null)} />
            )}
        </section>
    );
}

export default App;