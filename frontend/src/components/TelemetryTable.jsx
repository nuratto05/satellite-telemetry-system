function TelemetryTable({ telemetries, satellites }) {
    return (
        <div className=" bg-white h-64 shrink-0 overflow-hidden rounded-lg border border-gray-300">
            <div className="h-full overflow-y-auto">
                <table className="w-full text-left text-xs">
                        <tr className="border-b border-gray-300">
                            <th className="px-3 py-2 font-medium">Day</th>
                            <th className="px-3 py-2 font-medium">Satellite</th>
                            <th className="px-3 py-2 font-medium">Latitude</th>
                            <th className="px-3 py-2 font-medium">Longitude</th>
                            <th className="px-3 py-2 font-medium">Altitude (km)</th>
                            <th className="px-3 py-2 font-medium">Velocity (ms)</th>
                            <th className="px-3 py-2 font-medium">Temperature (°C)</th>
                            <th className="px-3 py-2 font-medium">Battery Level</th>
                        </tr>

                        {telemetries.map((t) => (
                            <tr key={t.id} className="odd:bg-white even:bg-gray-400">
                                <td className="px-3 py-2">{new Date(t.timestamp ?? Date.now()).toLocaleString()}</td>
                                <td className="px-3 py-2">{satellites.find((s) => s.id === t.satelliteId)?.satelliteId ?? t.satelliteId}</td>
                                <td className="px-3 py-2">{t.latitude.toFixed(4)}</td>
                                <td className="px-3 py-2">{t.longitude.toFixed(4)}</td>
                                <td className="px-3 py-2">{t.altitude.toFixed(1)}</td>
                                <td className="px-3 py-2">{t.velocity}</td>
                                <td className="px-3 py-2">{t.temperature}</td>
                                <td className="px-3 py-2">{t.batteryLevel}%</td>
                            </tr>
                        ))}

                        {telemetries.length === 0 && (
                            <tr>
                                <td colSpan={10} className="px-3 py-4 text-center text-gray-500">
                                    Waiting for telemetry...
                                </td>
                            </tr>
                        )}
                </table>
            </div>
        </div>
    );
}

export default TelemetryTable;