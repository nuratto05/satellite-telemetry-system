import { useState } from "react";

function SatelliteInfoBox({ satellite, missions, onClose }) {
    const [status, setStatus] = useState(satellite.status ?? "Active");

    function submit(e) {
        e.preventDefault();
        onClose();
    }

    const mission = missions.find((m) => m.id === satellite.missionId);

    return (

        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4">
            <div className="w-full max-w-sm rounded-lg border border-[#16181c] bg-black">

                <div className="flex items-center justify-between border-b border-[#16181c] px-4 py-3">
                    <h2 className="text-sm font-semibold text-white">{satellite.satelliteName}</h2>
                    <button onClick={onClose} className="text-xs text-white">
                        X
                    </button>
                </div>

                <div className="p-4">

                    <div className="mb-4 flex flex-col gap-3  pb-4">
                            <div className="flex flex-col gap-0.5">
                                <span className="text-[10px] uppercase tracking-wide text-gray-500">Satellite Status: </span>
                                <span className="text-sm text-white">{satellite.status}</span>
                            </div>
                            <div className="flex flex-col gap-0.5">
                                <span className="text-[10px] uppercase tracking-wide text-gray-500">Mission</span>
                                <span className="text-sm text-white">{mission.name}</span>
                            </div>

                            <div className="flex flex-col gap-0.5">
                                <span className="text-[10px] uppercase tracking-wide text-gray-500">Mission description</span>
                                <span className="text-sm text-white">{mission.description}</span>
                            </div>

                            <div className="flex flex-col gap-0.5">
                                <span className="text-[10px] uppercase tracking-wide text-gray-500">Mission status</span>
                                <span className="text-sm text-white">{mission.status}</span>
                            </div>

                            <div className="flex flex-col gap-0.5">
                                <span className="text-[10px] uppercase tracking-wide text-gray-500">Satellite description</span>
                                <span className="text-sm text-white">{satellite.description}</span>
                            </div>

                            <div className="flex flex-col gap-0.5">
                                <span className="text-[10px] uppercase tracking-wide text-gray-500">Country</span>
                                <span className="text-sm text-white">{satellite.country || "—"}</span>
                            </div>
                        </div>

                    <form onSubmit={submit} className="flex flex-col gap-3">
                        <label className="text-xs text-white">
                            Status
                            <select value={status} onChange={(e) => setStatus(e.target.value)} className="mt-1 w-full rounded-md border border-[#16181c] bg-vlack px-3 py-2 text-sm text-white bg-black">
                                <option key="Active" value="Active">Active</option>
                                <option key="Standby" value="Standby">Inactive</option>
                                <option key="Offline" value="Offline">Decommissioned</option>
                            </select>
                        </label>

                        <button type="submit" className="rounded-md bg-[#16181c] py-2 text-sm font-medium text-white">
                            Update status
                        </button>
                    </form>

                </div>
            </div>
        </div>


    );
}

export default SatelliteInfoBox;