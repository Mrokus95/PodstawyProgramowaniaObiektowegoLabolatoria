import React, { useState } from 'react';
import { List, Calculator, Loader2 } from 'lucide-react';
import styles from './FibonacciGenerator.module.css';
const FibonacciGenerator = () => {
    const [numbers, setNumbers] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const fetchFibonacci = async () => {
        setLoading(true);
        setError(null);
        setNumbers([]);

        try {
            const response = await fetch('http://localhost:5123/api/Fibonacci/generate?count=10');

            if (!response.ok) {
                throw new Error('Błąd połączenia z serwerem');
            }

            const data = await response.json();
            setNumbers(data);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className={styles.container}>
            <div className={styles.card}>
                <div className={styles.header}>
                    <h1><Calculator className={styles.icon} /> Ciąg Fibonacciego</h1>
                    <span className={styles.subtitle}>Generowanie rekurencyjne (10 elementów)</span>
                </div>

                <div className={styles.content}>
                    <button
                        onClick={fetchFibonacci}
                        disabled={loading}
                        className={styles.btnPrimary}
                    >
                        {loading ? (
                            <><Loader2 className={styles.spin} size={18} /> Obliczam...</>
                        ) : (
                            <><List size={18} /> Generuj ciąg</>
                        )}
                    </button>

                    {error && (
                        <div className={styles.errorBox}>
                            {error}
                        </div>
                    )}

                    {numbers.length > 0 && (
                        <div className={styles.resultsArea}>
                            <h3>Wynik:</h3>
                            <div className={styles.numberGrid}>
                                {numbers.map((num, index) => (
                                    <div key={index} className={styles.numberCard}>
                                        <span className={styles.index}>Liczba {index+1}</span>
                                        <span className={styles.value}>{num}</span>
                                    </div>
                                ))}
                            </div>
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
};

export default FibonacciGenerator;