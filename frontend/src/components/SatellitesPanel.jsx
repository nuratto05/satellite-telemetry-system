
function SatellitesPanel({ satellites, telemetries, alerts, onSelect }) {
    return (
        <div className="pointer-events-auto w-64 rounded-lg border border-[#16181c] bg-black">
            <div className="flex items-center justify-between border-b border-[#16181c] px-4 py-2.5">
                <h2 className="text-sm font-semibold text-white">Satellites</h2>
                <span className="text-xs text-white">{alerts.length} open alerts</span>
            </div>

            <ul className="max-h-60 overflow-auto scrollbar-thin">
                {satellites.map((s) => {
                    const t = telemetries.find((t) => t.satelliteName === s.satelliteName);
                    return (
                        <li key={s.id}>
                            <button onClick={() => onSelect(s)} className="w-full px-4 py-2.5 text-left outline-none hover:bg-[#16181c]">
                                <div className="flex items-center justify-between">
                                    <span className="flex items-center gap-2 text-[12px]">
                                        {s.satelliteName}
                                    </span>
                                    <span className="text-xs tabular-nums text-white">
                                        {t ? t.altitude + " km" : "No signal"}
                                    </span>
                                </div>
                            </button>
                        </li>
                    );
                })}
            </ul>
        </div>
    );
}

export default SatellitesPanel;