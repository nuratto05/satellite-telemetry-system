
function SatellitesPanel({ satellites, telemetries, alerts }) {
    return (
        <div className="pointer-events-auto w-64 rounded-lg border border-slate-700/60 bg-[#0b1220]/80 backdrop-blur">
            <div className="flex items-center justify-between border-b border-slate-700/60 px-4 py-2.5">
                <h2 className="text-sm font-semibold text-white">Satellites</h2>
                <span className="text-xs text-slate-400">{alerts.length} open alerts</span>
            </div>

            <ul className="max-h-60 divide-y divide-slate-800 overflow-y-auto">
                {satellites.map((s) => {
                    const t = telemetries.find((t) => t.satelliteName === s.satelliteName);
                    return (
                        <li key={s.id} className="px-4 py-2.5">
                            <div className="flex items-center justify-between">
                                <span className="text-[12px] text-slate-100">{s.satelliteName}</span>
                                <span className="text-xs tabular-nums text-slate-400">
                                    {t ? t.altitude + " km" : "No signal"}
                                </span>
                            </div>
                        </li>
                    );
                })}
            </ul>
        </div>
    );
}

export default SatellitesPanel;