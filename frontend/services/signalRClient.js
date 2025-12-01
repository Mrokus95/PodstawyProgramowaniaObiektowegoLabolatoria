import * as signalR from "@microsoft/signalr";

export function createSignalRClient(hubUrl) {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl(hubUrl)
        .withAutomaticReconnect()
        .build();

    let isConnected = false;
    let onConnectionChange = () => {};

    connection.onreconnecting(() => {
        isConnected = false;
        onConnectionChange(false);
    });

    connection.onreconnected(() => {
        isConnected = true;
        onConnectionChange(true);
    });

    connection.onclose(() => {
        isConnected = false;
        onConnectionChange(false);
    });

    const start = async () => {
        try {
            await connection.start();
            isConnected = true;
            onConnectionChange(true);
        } catch (err) {
            console.warn("SignalR start failed, retrying...", err);
            setTimeout(start, 2000);
        }
    };

    start();

    return {
        connection,
        isConnected: () => isConnected,

        setConnectionListener(callback) {
            onConnectionChange = callback;
        },

        on(eventName, callback) {
            connection.on(eventName, callback);
        },

        async invoke(method, ...args) {
            if (!isConnected) {
                console.warn("SignalR not connected yet.");
            }

            return await connection.invoke(method, ...args);

        }
    };
}
