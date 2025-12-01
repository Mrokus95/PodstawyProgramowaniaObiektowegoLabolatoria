import { useEffect, useState } from "react";
import axios from "axios";
import styles from "./CurrencyRates.module.css";

export default function CurrencyRates() {
    const [rates, setRates] = useState({});
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const loadRates = async () => {
            try {
                const response = await axios.get("http://localhost:5123/api/calculator/rates");

                const wanted = ["EUR", "CHF", "GBP"];

                const filtered = Object.entries(response.data)
                    .filter(([code]) => wanted.includes(code))
                    .reduce((acc, [code, rate]) => {
                        acc[code] = rate;
                        return acc;
                    }, {});

                setRates(filtered);
            } catch (err) {
                console.error("Nie udało się pobrać kursów:", err);
            } finally {
                setLoading(false);
            }
        };

        loadRates();
    }, []);

    if (loading) {
        return <p className={styles.loading}>Ładowanie tabeli kursów...</p>;
    }

    return (
        <div className={styles.wrapper}>
            <h3 className={styles.title}>Kursy Walut (NBP – Tabela A)</h3>

            <table className={styles.table}>
                <thead>
                <tr>
                    <th>Kod</th>
                    <th>Kurs (PLN)</th>
                </tr>
                </thead>

                <tbody>
                {Object.entries(rates).map(([code, rate]) => (
                    <tr key={code}>
                        <td>{code}</td>
                        <td>{rate.toFixed(4)}</td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
}
