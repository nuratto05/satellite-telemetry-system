import { useState } from "react";
import TelemetryTable from "./TelemetryTable";
import AlertsTable from "./AlertsTable";

function BottomPanel({ telemetries, alerts, satellites }) {
    const [tab, setTab] = useState("telemetry");

    return (
        <div className="flex h-64 shrink-0 flex-col overflow-hidden rounded-lg border border-gray-300">

            <div className="bg-white flex shrink-0 gap-4 px-3">
                <button key="telemetry" onClick={() => setTab("telemetry")} className="py-2 text-xs font-medium">
                    Telemetry
                </button>
                <button key="alerts" onClick={() => setTab("Alerts")} className="py-2 text-xs font-medium">
                    Alerts
                </button>
            </div>

            <div className="bg-white min-h-0 flex-1 overflow-y-auto">
                {tab === "telemetry" ? (
                    <TelemetryTable telemetries={telemetries} satellites={satellites} />
                ) : (
                    <AlertsTable alerts={alerts} satellites={satellites} />
                )}
            </div>
        </div>
    );
}

export default BottomPanel;