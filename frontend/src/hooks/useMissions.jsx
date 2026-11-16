import { useEffect, useState } from "react";
import { getMissions } from "../api/Missions";

export default function useMissions() {
    const [missions, setMissions] = useState([]);

    useEffect(() => {
        async function fetchMissions() {
            try {
                const data = await getMissions();
                setMissions(data);
            } catch (error) {
                console.error(error);
            }
        }

        fetchMissions();
    }, []);

    return missions;
}