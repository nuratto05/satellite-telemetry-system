import { useEffect, useState } from "react";
import { getSatellites } from "../api/Satellites";

export default function useSatellites() {
    const [satellites, setSatellites] = useState([]);

    useEffect(() => {
        async function fetchSatellites() {
            try {
                const data = await getSatellites();
                setSatellites(data);
            } catch (error) {
                console.error(error);
            }
        }

        fetchSatellites();
    }, []);

    return satellites;
}