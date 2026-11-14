
function BatteryBar({ level }) {
    const color = level < 20 ? "bg-red-500" : level < 50 ? "bg-amber-500" : "bg-emerald-500";
    return (
        <div className="flex items-center gap-2">
            <div className="h-1.5 w-16 overflow-hidden rounded-full bg-slate-800">
                <div className={"h-full " + color} style={{ width: level + "%" }} />
            </div>
            <span className="tabular-nums">{level}%</span>
        </div>
    );
}

export default BatteryBar;