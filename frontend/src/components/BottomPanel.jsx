import { useState } from "react";
import TelemetryTable from "./TelemetryTable";
import AlertsTable from "./AlertsTable";

function BottomPanel({ telemetries, alerts, satellites }) {
    const [tab, setTab] = useState("telemetry");
    const [satFilter, setSatFilter] = useState("all");

    return (
        <div className="flex h-72 w-full shrink-0 flex-col border-t border-[#16181c] bg-black">
            <div className="flex shrink-0 items-center justify-between border-b border-[#16181c] px-4">
                <div className="flex gap-6">
                    <button onClick={() => setTab("telemetry")} className="-mb-px py-3 text-sm font-medium">
                        Telemetry
                    </button>
                    <button onClick={() => setTab("alert")} className="-mb-px py-3 text-sm font-medium">
                        Alerts
                    </button>
                </div>

                <select value={satFilter} onChange={(e) => setSatFilter(e.target.value)}className="rounded-md border border-white px-2 py-1 text-xs bg-black">
                    <option value="all">All satellites</option>
                    {satellites.map((s) => (
                        <option key={s.satelliteName} value={s.satelliteName}>
                            {s.satelliteName}
                        </option>
                    ))}
                </select>
            </div>

            <div className="min-h-0 flex-1 overflow-y-auto">
                {tab === "telemetry" ? (
                    <TelemetryTable telemetries={telemetries} filter={satFilter} />
                ) : (
                    <AlertsTable alerts={alerts} satellites={satellites} filter={satFilter} />
                )}
            </div>
        </div>
    );
}

export default BottomPanel;