import { useState, useEffect } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";

export default function useAlerts() {

    const [alert, setAlert] = useState([]);

    useEffect(() => {

        const connection = new HubConnectionBuilder()
            .withUrl("http://localhost:5142/simulationHub")
            .withAutomaticReconnect()
            .build();

        const startConnection = async () => {
            try {
                await connection.start();
                console.log("Connected to SignalR");
            } catch (err) {
                console.error(err);
            }
        };

        // Listen for incoming telemetry
        connection.on("SendAlert", (data) => {
            setAlert(prev => [data, ...prev].slice(0, 20));
        });

        startConnection();

        return () => {
            connection.off("SendAlert");
        };

    }, []);

    return alert;
}