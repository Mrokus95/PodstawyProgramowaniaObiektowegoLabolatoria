import React, { useState, useRef, useCallback } from 'react';
import { Play, RotateCcw, Activity, Wifi, WifiOff, AlertCircle } from 'lucide-react';
import styles from "./MonteCarloVisualization.module.css";
import {useMonteCarloSignalR} from "../hooks/useMonteCarloSignalR.js";

const MonteCarloVisualization = () => {
    const [totalPointsToGenerate, setTotalPointsToGenerate] = useState(10000);
    const [isRunning, setIsRunning] = useState(false);

    const [stats, setStats] = useState({
        totalPoints: 0,
        pointsInCircle: 0,
        estimatedPi: 0,
        ratio: 0,
        error: 0,
        duration: 0
    });

    const canvasRef = useRef(null);

    const resetCanvas = () => {
        const canvas = canvasRef.current;
        if (!canvas) return;
        const ctx = canvas.getContext('2d');
        const size = canvas.width;
        const center = size / 2;
        const radius = size / 2;

        ctx.clearRect(0, 0, size, size);
        ctx.fillStyle = '#f8fafc';
        ctx.fillRect(0, 0, size, size);

        ctx.beginPath();
        ctx.arc(center, center, radius, 0, 2 * Math.PI);
        ctx.strokeStyle = '#2563eb';
        ctx.lineWidth = 2;
        ctx.stroke();
    };

    const drawBatch = (points) => {
        const canvas = canvasRef.current;
        if (!canvas) return;
        const ctx = canvas.getContext('2d');
        const width = canvas.width;
        const height = canvas.height;

        points.forEach(p => {
            const drawX = (p.x + 1) * (width / 2);
            const drawY = (p.y + 1) * (height / 2);

            ctx.fillStyle = p.isInside ? 'rgba(37, 99, 235, 0.6)' : 'rgba(148, 163, 184, 0.4)';
            ctx.fillRect(drawX, drawY, 2, 2);
        });
    };

    const handleBatchReceived = useCallback((batchResult) => {
        drawBatch(batchResult.points);
        setStats({
            totalPoints: batchResult.totalPointsProcessed,
            pointsInCircle: batchResult.pointsInCircle,
            estimatedPi: batchResult.estimatedPi,
            ratio: batchResult.pointsInCircle / batchResult.totalPointsProcessed,
            error: Math.abs(Math.PI - batchResult.estimatedPi),
            duration: batchResult.duration
        });
    }, []);

    const handleSimulationFinished = useCallback(() => {
        setIsRunning(false);
        console.log("Symulacja zakończona");
    }, []);

    const { isConnected, error, startSimulation, setError } = useMonteCarloSignalR(
        handleBatchReceived,
        handleSimulationFinished
    );

    const handleStart = async () => {
        if (isRunning || !isConnected) return;

        if (!/^\d+$/.test(totalPointsToGenerate)) {
            setError("Podaj poprawną liczbę całkowitą większą od zera.");
            return;
        }

        const points = Number(totalPointsToGenerate);

        if (points <= 0) {
            setError("Podaj liczbę większą od zera.");
            return;
        }

        if (points > 1_000_000_000) {
            setError("Limit: maksymalnie 100 milionów punktów.");
            return;
        }

        resetCanvas();
        setStats({ totalPoints: 0, pointsInCircle: 0, estimatedPi: 0, ratio: 0, error: 0, duration: 0 });
        setError(null);
        setIsRunning(true);

        await startSimulation(points);
    };

    const handleStop = () => {
        setIsRunning(false);
    };

    const handleReset = () => {
        handleStop();
        resetCanvas();
        setStats({ totalPoints: 0, pointsInCircle: 0, estimatedPi: 0, ratio: 0, error: 0, duration: 0 });
        setError(null);
    };

    return (
        <div className={styles.container}>
            <div className={styles.card}>

                <div className={styles.sidebar}>
                    <div className={styles.header}>
                        <h1><Activity className={styles.icon} /> Monte Carlo</h1>
                        <div className={styles.status}>
                            Status:
                            {isConnected ? (
                                <span className={styles.connected}>
                                    <Wifi size={16} style={{display: 'inline', marginRight: 4}}/> Połączono
                                </span>
                            ) : (
                                <span className={styles.disconnected}>
                                    <WifiOff size={16} style={{display: 'inline', marginRight: 4}}/> Rozłączono
                                </span>
                            )}
                        </div>
                    </div>

                    <div className={styles.controlPanel}>
                        <div className={styles.inputGroup}>
                            <label className={styles.label}>Liczba punktów</label>
                            <input
                                className={styles.input}
                                type="number"
                                value={totalPointsToGenerate}
                                onChange={(e) => setTotalPointsToGenerate(e.target.value)}
                                disabled={isRunning}
                                min="1"
                            />
                        </div>

                        {error && (
                            <div className={styles.errorDiv}>
                                <AlertCircle size={18} style={{flexShrink: 0}} />
                                <span>{error}</span>
                            </div>
                        )}

                        <div className={styles.buttonGroup}>
                            <button
                                onClick={handleStart}
                                disabled={isRunning || !isConnected}
                                className={`${styles.btn} ${styles.btnPrimary}`}
                            >
                                {isRunning ? (
                                    <>Liczenie...</>
                                ) : (
                                    <><Play size={18} /> Start</>
                                )}
                            </button>

                            <button
                                onClick={handleReset}
                                className={`${styles.btn} ${styles.btnSecondary}`}
                                title="Reset"
                            >
                                <RotateCcw size={18} />
                            </button>
                        </div>
                    </div>

                    <div>
                        <div className={styles.statsGrid}>
                            <div className={`${styles.statCard} ${styles.statCardBlue}`}>
                                <span className={`${styles.statLabel} ${styles.statLabelBlue}`}>Obliczone Pi</span>
                                <div className={styles.statValue}>{stats.estimatedPi.toFixed(6)}</div>
                            </div>
                            <div className={`${styles.statCard} ${styles.statCardGray}`}>
                                <span className={`${styles.statLabel} ${styles.statLabelGray}`}>Prawdziwe Pi</span>
                                <div className={styles.statValue}>{Math.PI.toFixed(6)}</div>
                            </div>
                        </div>

                        <div className={styles.detailsList}>
                            <div className={styles.detailRow}>
                                <span>Punkty w kole:</span>
                                <span className={styles.bold}>{stats.pointsInCircle.toLocaleString()}</span>
                            </div>
                            <div className={styles.detailRow}>
                                <span>Razem punktów:</span>
                                <span>{stats.totalPoints.toLocaleString()}</span>
                            </div>
                            <div className={styles.detailRow}>
                                <span className={styles.timeDiv}>
                                    Czas obliczeń:
                                </span>
                                <span className={styles.bold}>{stats.duration} ms</span>
                            </div>
                            <div className={styles.detailRow}>
                                <span className={styles.errorText}>Błąd bezwzględny:</span>
                                <span className={styles.errorText}>{stats.error.toFixed(6)}</span>
                            </div>
                        </div>

                        <div className={styles.progressContainer}>
                            <div
                                className={styles.progressBar}
                                style={{ width: `${Math.min(100, (stats.totalPoints / Math.max(1, parseInt(totalPointsToGenerate) || 1)) * 100)}%` }}
                            ></div>
                        </div>
                    </div>
                </div>

                <div className={styles.visualizationArea}>
                    <div className={styles.canvasWrapper}>
                        <canvas
                            ref={canvasRef}
                            width={500}
                            height={500}
                            className={styles.canvas}
                        />
                    </div>
                </div>

            </div>
        </div>
    );
};

export default MonteCarloVisualization;