# Podstawy Programowania Obiektowego --- Laboratoria

Repozytorium zawiera rozwiązania zadań realizowanych w ramach
laboratoriów z przedmiotu **Podstawy Programowania Obiektowego**.\
Projekty podzielono na część backendową (**API .NET 9**, aplikacje
konsolowe) oraz frontend (**React** --- tylko dla zadań 1a, 1b, 1c).

------------------------------------------------------------------------

# Spis treści

1.  [Struktura repozytorium](#struktura-repozytorium)
2.  [Zadania](#zadania)
    -   [Zadania 1a, 1b, 1c](#zadania-1a-1b-1c)
    -   [Frontend](#frontend)
    -   [Zadania 2a i 2b](#zadania-2a-i-2b)
3.  [Wymagania wstępne](#wymagania-wstępne)
4.  [Autor](#autor)

------------------------------------------------------------------------

# Struktura repozytorium

    PodstawyProgramowaniaObiektowegoLabolatoria/
    │
    ├── PodstawyProgramowaniaObiektowego/                # Zadania 1a, 1b, 1c
    │
    ├── PodstawyProgramowaniaObiektowego.Console/        # Zadania 2a, 2b
    │
    └── frontend/                                         # Warstwa wizualna dla zadań 1a, 1b, 1c (React)

------------------------------------------------------------------------

# Zadania

## Zadania 1a, 1b, 1c

Projekt backendu znajduje się w folderze:

    PodstawyProgramowaniaObiektowego

Zadania zostały wykonane jako API w technologii **.NET 9**.

### Uruchamianie API

``` bash
cd PodstawyProgramowaniaObiektowego
dotnet run
```

Aplikacja powinna wystartować pod adresem:

    http://localhost:5123

------------------------------------------------------------------------

## Frontend

Frontend jest warstwą wizualną **tylko dla zadań 1a, 1b, 1c**.

Znajduje się w katalogu:

    frontend

### Uruchamianie frontendu

``` bash
cd frontend
npm install
npm run dev
```

Aplikacja uruchomi się pod:

    http://localhost:5173/

------------------------------------------------------------------------

## Zadania 2a i 2b

Znajdują się w projekcie:

    PodstawyProgramowaniaObiektowego.Console

### Uruchomienie

``` bash
cd PodstawyProgramowaniaObiektowego.Console
dotnet run
```

Z wyświetlonego menu należy wybrać zadanie, które ma podlegać
uruchomieniu.

------------------------------------------------------------------------

# Wymagania wstępne

-   **.NET SDK 9.0**
-   **Node.js 18+**
-   **npm 9+**
-   IDE do uruchamiania .NET i React\
    (np. Visual Studio / Rider / VS Code / WebStorm)

------------------------------------------------------------------------

# Autor

Łukasz Mroczkowski
