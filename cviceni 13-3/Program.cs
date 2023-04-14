using static System.Net.WebRequestMethods;

namespace cviceni_13_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleColor puvodniPozadi = Console.BackgroundColor;
            ConsoleColor puvodniPopredi = Console.ForegroundColor;
            Hra hra = new Hra(5, 0.7);
            hra.Hraj();
            Console.SetCursorPosition(0, 0);
            Console.ReadKey();
            Console.BackgroundColor = puvodniPozadi;
            Console.ForegroundColor = puvodniPopredi;
            Console.Clear();
        }
    }
}