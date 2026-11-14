import Earth from "./components/Earth";
import BottomPanel from "./components/BottomPanel";
import SatellitesPanel from "./components/SatellitesPanel";

import useSatellite from "./hooks/useSatellites";
import useTelemetry from "./hooks/TelemetriesHook";
import useAlerts from "./hooks/AlertHook";

function App() {
    const telemetries = useTelemetry();
    const satellites = useSatellite();
    const alerts = useAlerts();

    return (
        <section className="flex h-screen flex-col bg-[#05080f] font-['IBM_Plex_Sans',system-ui,sans-serif] text-slate-200">
            <div className="relative min-h-0 flex-1">
                <div className="absolute inset-0">
                    <Earth />
                </div>

                <header className="pointer-events-none absolute inset-x-0 top-0 flex items-start justify-between gap-4 p-4">
                    <SatellitesPanel satellites={satellites} telemetries={telemetries} alerts={alerts} />

                    <div className="pointer-events-auto rounded-lg border border-slate-700/60 bg-[#0b1220]/80 px-4 py-2 text-right backdrop-blur">
                        <p className="text-sm font-semibold text-white">Ground Control</p>
                        <p className="text-xs text-slate-400">{satellites.length} satellites tracked</p>
                    </div>
                </header>
            </div>

            <BottomPanel telemetries={telemetries} alerts={alerts} satellites={satellites} />
        </section>
    );
}

export default App;