namespace PodstawyProgramowaniaObiektowego.Console
{
    public class Sorter
    {
        private readonly double[] _liczby = new double[6]; 
        
        public void read_data()
        {
            for (int i = 0; i < _liczby.Length; i++)
            {
                bool isValid = false;
                while (!isValid)
                {
                    System.Console.Write($"Podaj liczbę nr {i + 1}: ");
                    string? input = System.Console.ReadLine();
                    
                    if (double.TryParse(input, out double result))
                    {
                        _liczby[i] = result;
                        isValid = true;
                    }
                    else
                    {
                        System.Console.WriteLine("Błąd! To nie jest liczba. Spróbuj ponownie.");
                    }
                }
            }
        }
        
        public void process_data()
        {
            int n = _liczby.Length;
            
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (_liczby[j] > _liczby[j + 1])
                    {
                        (_liczby[j], _liczby[j + 1]) = (_liczby[j + 1], _liczby[j]);
                    }
                }
            }
        }
        
        public void show_results()
        {
            System.Console.WriteLine("\n--- Posortowana tablica (rosnąco) ---");
            
            for (int i = 0; i < _liczby.Length; i++)
            {
                if (i < _liczby.Length - 1)
                {
                    System.Console.Write($"{_liczby[i]}, ");
                }
                else
                {
                    System.Console.Write($"{_liczby[i]}");
                }
            }
            System.Console.WriteLine();
        }
    }
}