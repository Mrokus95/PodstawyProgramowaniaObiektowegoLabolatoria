namespace PodstawyProgramowaniaObiektowego.Console
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                System.Console.Clear();
                System.Console.WriteLine("=== MENU GŁÓWNE ===");
                System.Console.WriteLine("1. Oblicz pole prostokąta (zadanie 2a)");
                System.Console.WriteLine("2. Sortowanie liczb (zadanie 2b)");
                System.Console.WriteLine("3. Wyjście");
                System.Console.Write("\nWybierz opcję: ");

                string? wybor = System.Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        UruchomProstokat();
                        break;
                    case "2":
                        UruchomSortowanie();
                        break;
                    case "3":
                        return;
                    default:
                        System.Console.WriteLine("Nieznana opcja!");
                        break;
                }

                System.Console.WriteLine("\nNaciśnij dowolny klawisz, aby wrócić do menu...");
                System.Console.ReadKey();
            }
        }
        
        static void UruchomProstokat()
        {
            RectangleArea prostokat = new RectangleArea();
            prostokat.read_data();
            prostokat.process_data();
            prostokat.show_results();
        }
        
        static void UruchomSortowanie()
        {
            Sorter sorter = new Sorter();
            sorter.read_data();
            sorter.process_data();
            sorter.show_results();
        }
    }
}