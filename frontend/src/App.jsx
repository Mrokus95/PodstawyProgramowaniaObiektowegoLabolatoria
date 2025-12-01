import { BrowserRouter, Routes, Route, Link } from "react-router-dom";
import styles from "./App.module.css";
import Exercise1Page from "../pages/Exercise1Page.jsx";
import Exercise1bPage from "../pages/Exercise1bPage.jsx";
import Exercise1cPage from "../pages/Exercise1cPage.jsx";

function App() {
    return (
        <BrowserRouter>
            <nav className={styles.menu}>
                <Link className={styles.link} to="/">Kursy walut - 1a</Link>
                <Link className={styles.link} to="/1b">Monte Carlo - 1b</Link>
                <Link className={styles.link} to="/1c">Fibonacci - 1c</Link>
            </nav>
            <Routes>
                <Route path="/" element={<Exercise1Page/>} />
                <Route path="/1b" element={<Exercise1bPage/>} />
                <Route path="/1c" element={<Exercise1cPage/>} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;