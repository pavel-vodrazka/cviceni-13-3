using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cviceni_13_3
{
    internal class Hra
    {
        /// <summary>
        /// Šířka hřiště
        /// (polovina šířky okna ve znacích - dílek má rozměr 2x1 znak, aby měl přibližně čtvercový tvar)
        /// </summary>
        public int Sirka { get; init; }
        /// <summary>
        /// Výška hřiště
        /// (výška okna ve znacích)
        /// </summary>
        public int Vyska { get; init; }
        /// <summary>
        /// Barva hřiště (pozadí)
        /// </summary>
        public static ConsoleColor BarvaHriste { get; } = ConsoleColor.Green;
        public static char ZnakHriste { get; } = '█';
        public static char ZnakZradla { get; } = '█';
        /// <summary>
        /// Barva živého hada
        /// </summary>
        public static ConsoleColor BarvaHadaZiveho { get; } = ConsoleColor.Magenta;
        /// <summary>
        /// Barva mrtvého hada
        /// </summary>
        public static ConsoleColor BarvaHadaMrtveho { get; } = ConsoleColor.Gray;
        /// <summary>
        /// Barva žrádla
        /// </summary>
        public static ConsoleColor BarvaZradla { get; } = ConsoleColor.Red;
        /// <summary>
        /// Doba čekání mezi vykresleními v násobcích 50 ms
        /// </summary>
        public double Rychlost { get; init; }
        /// <summary>
        /// Počáteční délka hada (dílků)
        /// </summary>
        public int PocatecniDelkaHada { get; init; }
        /// <summary>
        /// Obrazovka
        /// </summary>
        internal Obrazovka Obrazovka { get; init; }
        internal Had Had { get; init; }
        internal Zradlo Zradlo { get; set; }

        internal static Random random = new Random();
        private double cekaniMs;

        /// <summary>
        /// Inicializuje hru s délkou hada 5 a rychlostí 1
        /// </summary>
        internal Hra() : this(5, 1) { }
        /// <summary>
        /// Inicializuje hru
        /// </summary>
        /// <param name="pocatecniDelkaHada">Počáteční délka hada (dílků)</param>
        /// <param name="rychlost">Rychlost hry (1 odpovídá čekání 50 ms)</param>
        internal Hra(int pocatecniDelkaHada, double rychlost)
        {
            if ((Console.WindowWidth % 2) != 0)
                Console.WindowWidth--;
            Sirka = Console.WindowWidth / 2;
            Vyska = Console.WindowHeight;
            PocatecniDelkaHada = pocatecniDelkaHada;
            Rychlost = rychlost;
            cekaniMs = 50 / Rychlost;

            Obrazovka = new(Sirka, Vyska);
            do
            {
                Had = new(this, pocatecniDelkaHada);
                if (Had.Pohyb == null)
                    Had.Vymaz();
            }
            while (Had.Pohyb == null);
            Zradlo = new(this);
        }

        internal void Hraj()
        {
            while (Had.Zivy)
            {
                Had.Lez();
                if (Zradlo.Sezrane)
                    Zradlo = new(this);
                Thread.Sleep((int)cekaniMs);
            }
        }
    }
}
