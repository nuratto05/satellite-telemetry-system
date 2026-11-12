const rowStyle = {
    critical: "bg-red-100",
    severe: "bg-yellow-100",
};

const norm = (v) => String(v).toLowerCase();

function AlertsTable({ alerts, satellites }) {
    return (
        <table className="bg-white w-full text-left text-xs">
            <tr className="border-b border-gray-300">
                <th className="px-3 py-2 font-medium">Day</th>
                <th className="px-3 py-2 font-medium">Satellite</th>
                <th className="px-3 py-2 font-medium">Severity</th>
                <th className="px-3 py-2 font-medium">Velocity</th>
                <th className="px-3 py-2 font-medium">Temperature</th>
                <th className="px-3 py-2 font-medium">Battery</th>
                <th className="px-3 py-2 font-medium">Types</th>
            </tr>

            {alerts.map((a) => (
                <tr
                    key={a.id}
                    className={rowStyle[norm(a.severity)]}
                >
                    <td className="px-3 py-2">{new Date(a.timestamp).toLocaleString()}</td>
                    <td className="px-3 py-2">
                        {satellites.find((s) => s.id === a.satelliteId)?.satelliteId}
                    </td>
                    <td className="px-3 py-2 font-medium">{a.severity}</td>
                    <td className="px-3 py-2"><Level value={a.velocityLevel} /></td>
                    <td className="px-3 py-2"><Level value={a.temperatureLevel} /></td>
                    <td className="px-3 py-2"><Level value={a.batteryLevel} /></td>
                    <td className="px-3 py-2">{(a.severityTypes ?? []).join(", ")}</td>
                </tr>
            ))}

            {alerts.length === 0 && (
                <tr>
                    <td colSpan={10} className="px-3 py-4 text-center text-gray-500">
                        Waiting for Alerts...
                    </td>
                </tr>
            )}
        </table>
    );
}

export default AlertsTable;