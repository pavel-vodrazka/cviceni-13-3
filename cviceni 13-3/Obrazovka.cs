using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cviceni_13_3
{
    internal class Obrazovka
    {
        internal int Sirka { get; init; }
        internal int Vyska { get; init; }

        internal Obrazovka(int sirka, int vyska)
        {
            Sirka = sirka;
            Vyska = vyska;

            Vykresli();
        }

        internal void Vykresli()
        {
            Console.Clear();
            for (int j = 0; j < Vyska; j++)
            {
                Console.CursorTop = j;
                Console.CursorLeft = 0;
                Console.BackgroundColor = Hra.BarvaHriste;
                Console.ForegroundColor = Hra.BarvaHriste;
                for (int i = 0; i < Sirka; i++)
                {
                    Console.Write("{0}{0}", Hra.ZnakHriste);
                }
            }
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
        }

        internal void PrekresliCtverecek(Dilek dilek, ConsoleColor pozadi, ConsoleColor popredi, char znak)
        {
            int X = dilek.X;
            int Y = dilek.Y;
            Console.CursorTop = Y;
            Console.CursorLeft = 2 * X;
            Console.BackgroundColor = pozadi;
            Console.ForegroundColor = popredi;
            Console.Write("{0}{0}", znak);
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
        }
    }
}
