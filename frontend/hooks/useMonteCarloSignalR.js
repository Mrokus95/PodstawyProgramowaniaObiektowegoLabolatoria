import { useEffect, useRef, useState } from "react";
import { createSignalRClient } from "../services/signalRClient";

const HUB_URL = "http://localhost:5123/monteCarloHub";

export function useMonteCarloSignalR(onBatch, onFinished) {
    const [isConnected, setIsConnected] = useState(false);
    const [error, setError] = useState(null);

    const clientRef = useRef(null);

    useEffect(() => {
        const client = createSignalRClient(HUB_URL);
        clientRef.current = client;

        client.setConnectionListener(state => setIsConnected(state));
        client.on("ReceiveBatch", onBatch);
        client.on("SimulationFinished", onFinished);

        return () => {
            client.connection.stop();
        };
    }, []);

    const startSimulation = async (points) => {
        try {
            await clientRef.current.invoke("StartSimulation", points);
        } catch (err) {
            setError(err.message || err.toString());
        }
    };

    return {
        isConnected,
        error,
        startSimulation,
        setError
    };
}
