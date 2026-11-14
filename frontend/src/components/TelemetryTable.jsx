import BatteryBar from "./BatteryBar";

function TelemetryTable({ telemetries, filter = "all" }) {
    const data = telemetries.filter((t) => filter === "all" || t.satelliteName === filter);

    return (
        <table className="w-full text-left text-xs tabular-nums">
                <tr className="border-b border-slate-800 text-slate-400">
                    <th className="whitespace-nowrap px-4 py-2 font-medium">Time</th>
                    <th className="whitespace-nowrap px-4 py-2 font-medium">Satellite</th>
                    <th className="whitespace-nowrap px-4 py-2 font-medium">Latitude</th>
                    <th className="whitespace-nowrap px-4 py-2 font-medium">Longitude</th>
                    <th className="whitespace-nowrap px-4 py-2 font-medium">Altitude (km)</th>
                    <th className="whitespace-nowrap px-4 py-2 font-medium">Velocity (m/s)</th>
                    <th className="whitespace-nowrap px-4 py-2 font-medium">Temperature (°C)</th>
                    <th className="whitespace-nowrap px-4 py-2 font-medium">Battery</th>
                </tr>

            <tbody className="divide-y divide-slate-800/70 text-slate-200">
                {data.map((d) => (
                    <tr key={d.id} className="hover:bg-slate-800/40">
                        <td className="whitespace-nowrap px-4 py-2 text-slate-400">{d.timestamp }</td>
                        <td className="px-4 py-2 font-medium text-white">{d.satelliteName}</td>
                        <td className="px-4 py-2">{d.latitude.toFixed(4)}</td>
                        <td className="px-4 py-2">{d.longitude.toFixed(4)}</td>
                        <td className="px-4 py-2">{d.altitude.toFixed(1)}</td>
                        <td className="px-4 py-2">{d.velocity}</td>
                        <td className="px-4 py-2">{d.temperature}</td>
                        <td className="px-4 py-2">
                            <BatteryBar level={d.batteryLevel} />
                        </td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}

export default TelemetryTable;