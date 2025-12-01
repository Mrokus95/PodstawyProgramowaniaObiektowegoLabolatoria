import { useState } from "react";
import axios from "axios";
import styles from "./CurrencyForm.module.css";

export default function CurrencyForm() {
    const [amount, setAmount] = useState("");
    const [currency, setCurrency] = useState("EUR");
    const [loading, setLoading] = useState(false);
    const [result, setResult] = useState(null);
    const [error, setError] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setError("");
        setResult(null);

        try {
            const response = await axios.get("http://localhost:5123/api/calculator/convert", {
                params: { amount: Number(amount), currency }
            });

            setResult(response.data);

        } catch (err) {
            if (err.response?.data?.errors) {
                const messages = [];
                for (const key in err.response.data.errors) {
                    messages.push(...err.response.data.errors[key]);
                }
                setError(messages.join("\n"));
            } else {
                setError(err.response?.data || "Błąd połączenia z API");
            }

        } finally {
            setLoading(false);
        }
    };

    return (
        <div className={styles.container}>
            <h2 className={styles.title}>
                💱 Przelicznik Walut<br />
                <span className={styles.small}>PLN → USD → Waluta docelowa</span>
            </h2>

            <form onSubmit={handleSubmit} className={styles.form}>

                <label className={styles.label}>Kwota w PLN:</label>
                <input
                    className={styles.input}
                    type="number"
                    min="0"
                    step="0.01"
                    value={amount}
                    onChange={(e) => setAmount(e.target.value)}
                    required
                />

                <label className={styles.label}>Waluta docelowa:</label>
                <select
                    className={styles.select}
                    value={currency}
                    onChange={(e) => setCurrency(e.target.value)}
                >
                    <option value="EUR">EUR</option>
                    <option value="CHF">CHF</option>
                    <option value="GBP">GBP</option>
                </select>

                <button className={styles.button} type="submit" disabled={loading}>
                    {loading ? "Przeliczam..." : "Przelicz"}
                </button>
            </form>

            {error && (
                <div className={styles.errorBox}>
                    <strong>Błąd:</strong><br />
                    {error}
                </div>
            )}

            {result && (
                <div className={styles.resultBox}>
                    <h3>Wynik</h3>
                    <p><strong>Waluta:</strong> {result.currency}</p>
                    <p><strong>Kwota:</strong> {result.amount.toFixed(2)}</p>
                    <p><strong>Kurs PLN → USD → {result.currency}:</strong> {result.rate.toFixed(4)}</p>
                    <p><strong>Data kursu:</strong> {result.rateDate}</p>
                </div>
            )}
        </div>
    );
}
