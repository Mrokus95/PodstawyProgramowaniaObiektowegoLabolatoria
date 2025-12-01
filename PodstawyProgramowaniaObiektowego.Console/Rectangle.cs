namespace PodstawyProgramowaniaObiektowego.Console;

public class RectangleArea
{
    private double _a;
    private double _b;
    private double _surfaceArea;
    
    public void read_data()
    {
        System.Console.Write("Podaj długość boku a: ");
        while (!(double.TryParse(System.Console.ReadLine(), out _a) && _a > 0))
        {
            System.Console.Write("Niepoprawna wartość! Podaj dodatnią liczbę dla boku a, wartości po przecinku należy oddzielić przecinkiem: ");
        }

        System.Console.Write("Podaj długość boku b: ");
        while (!(double.TryParse(System.Console.ReadLine(), out _b) && _b > 0))
        {
            System.Console.Write("Niepoprawna wartość! Podaj dodatnią liczbę dla boku bl, wartości po przecinku należy oddzielić przecinkiem: ");
        }
    }
    
    public void process_data()
    {
        _surfaceArea = _a * _b;
    }
    
    public void show_results()
    {
        System.Console.WriteLine($"\n--- Wyniki ---");
        System.Console.WriteLine($"Bok a: {_a:F2}");
        System.Console.WriteLine($"Bok b: {_b:F2}");
        System.Console.WriteLine($"Pole prostokąta: {_surfaceArea:F2}");
    }
}