const severityLevels = {
    critical: "bg-red-500/10 text-red-300 ring-red-500/30",
    warning: "bg-amber-500/10 text-amber-300 ring-amber-500/30",
    info: "bg-sky-500/10 text-sky-300 ring-sky-500/30",
    ok: "bg-emerald-500/10 text-emerald-300 ring-emerald-500/30",
};

function StatusPill({ alert }) {
    const severity = String(alert.severity).toLowerCase();
    return (
        <span className={"inline-flex items-center gap-1.5 rounded-full px-2 py-0.5 text-xs font-medium capitalize ring-1 ring-inset " + (severityLevels[severity] ?? severityLevels.info)}>
            <span className="h-1.5 w-1.5 rounded-full bg-current" />
            {severity}
        </span>
    );
}

export default StatusPill;