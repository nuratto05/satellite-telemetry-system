const severityLevels = {
    critical: "text-red-400",
    severe: "text-yellow-400"
};

function AlertsTable({ alerts, filter = "all" }) {
    const data = alerts.filter((a) => filter === "all" || a.satelliteName === filter);

    return (
        <table className="w-full text-left text-xs">
            <thead className="sticky top-0 z-10 bg-black">
                <tr className="text-white">
                    <th className="px-4 py-2 font-medium">Time</th>
                    <th className="px-4 py-2 font-medium">Satellite</th>
                    <th className="px-4 py-2 font-medium">Severity</th>
                    <th className="px-4 py-2 font-medium">Message</th>
                </tr>
            </thead>

            <tbody className="text-white">
                {data.map((d) => (
                    <tr key={d.id} className="hover:bg-[#16181c]">
                        <td className="whitespace-nowrap px-4 py-2 tabular-nums">
                            {d.timestamp}
                        </td>
                        <td className="px-4 py-2 font-medium text-white">{d.satelliteName}</td>
                        <td className="px-4 py-2">
                            <span className={"inline-flex items-center gap-1.5 rounded-full px-2 py-0.5 text-xs font-medium capitalize " + (severityLevels[String(d.severity).toLowerCase()])}>          
                                {String(d.severity).toLowerCase()}
                            </span>
                        </td>
                        <td className="px-4 py-2">{d.message}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}

export default AlertsTable;