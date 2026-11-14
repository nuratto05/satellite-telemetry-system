import StatusPill from "./StatusPill";

function AlertsTable({ alerts, filter = "all" }) {
    const data = alerts.filter((a) => filter === "all" || a.satelliteName === filter);

    return (
        <table className="w-full text-left text-xs">
            <thead className="sticky top-0 z-10 bg-[#0b1220]">
                <tr className="border-b border-slate-800 text-slate-400">
                    <th className="px-4 py-2 font-medium">Time</th>
                    <th className="px-4 py-2 font-medium">Satellite</th>
                    <th className="px-4 py-2 font-medium">Severity</th>
                    <th className="px-4 py-2 font-medium">Message</th>
                </tr>
            </thead>

            <tbody className="divide-y divide-slate-800/70 text-slate-200">
                {data.map((d) => (
                    <tr key={d.id} className="hover:bg-slate-800/40">
                        <td className="whitespace-nowrap px-4 py-2 tabular-nums text-slate-400">
                            {d.timestamp}
                        </td>
                        <td className="px-4 py-2 font-medium text-white">{d.satelliteName}</td>
                        <td className="px-4 py-2">
                            <StatusPill alert={d} />
                        </td>
                        <td className="px-4 py-2">{d.message}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}

export default AlertsTable;