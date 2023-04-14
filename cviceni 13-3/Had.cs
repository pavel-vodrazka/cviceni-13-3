using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cviceni_13_3
{
    internal class Had
    {
        internal List<Dilek> Telo { get; private set; }
        internal bool Zivy { get; private set; }
        private Hra Hra { get; init; }
        private int Delka { get; init; }

        internal Dilek? Pohyb { get; private set; }
        internal int Smer { get; private set; }

        internal Had(Hra hra) : this(hra, 5) { }
        internal Had(Hra hra, int delka)
        {
            Hra = hra;
            Delka = delka;

            Telo = new List<Dilek>();

            Dilek hlava = new(Hra.random.Next(0, hra.Sirka), Hra.random.Next(0, hra.Vyska));
            Telo.Add(hlava);
            Dilek? novyDilek;
            for (int i = 1; i < Delka; i++)
            {
                novyDilek = GenerujDalsiDilek(true);
                if (novyDilek == null) // není kde vytvořit další dílek těla, had bude kratší 
                    break;
                Telo.Add(novyDilek);
            }
            Pohyb = GenerujDalsiDilek(false);
            Zivy = true;
            Vykresli();
        }

        internal void Vymaz()
        {
            ConsoleColor barva = Hra.BarvaHriste;
            foreach (Dilek dilek in Telo)
                Hra.Obrazovka.PrekresliCtverecek(dilek, barva, barva, Hra.ZnakHriste);
        }

        internal void Vykresli()
        {
            ConsoleColor barva;
            barva = Zivy ? Hra.BarvaHadaZiveho : Hra.BarvaHadaMrtveho;
            for (int i = 0; i < Telo.Count; i++)
            {
                Dilek dilek = Telo[i];
                Hra.Obrazovka.PrekresliCtverecek(dilek, barva, ConsoleColor.White, i.ToString().Last());
                //Hra.Obrazovka.PrekresliCtverecek(dilek, barva, ConsoleColor.White, znak);
            }
        }

        internal bool Lez()
        {
            Dilek predchozi;

            Vymaz();
            if (NarazilBy(Pohyb))
            {
                Zivy = false;
            }
            else
            {
                Telo.Insert(0, Pohyb);
                if (!SezralBy())
                    Telo.RemoveAt(Telo.Count - 1);
                else
                    Hra.Zradlo.Sezrane = true;
                predchozi = Pohyb;
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo klavesa = Console.ReadKey();
                    if (klavesa.Key == ConsoleKey.UpArrow)
                        Smer = 0;
                    if (klavesa.Key == ConsoleKey.RightArrow)
                        Smer = 1;
                    if (klavesa.Key == ConsoleKey.DownArrow)
                        Smer = 2;
                    if (klavesa.Key == ConsoleKey.LeftArrow)
                        Smer = 3;
                }
                Pohyb = GenerujDalsiZPredchozihoASmeru(predchozi, Smer);
            }
            Hra.Zradlo.Vykresli(); // žrádlo v hadovi
            Vykresli();

            return Zivy;
        }

        private Dilek? GenerujDalsiDilek(bool keKonci)
        {
            Dilek? novy = null;
            Dilek posledni;
            if (keKonci)
                posledni = Telo.Last();
            else
                posledni = Telo.First();
            bool zkusitZnovu = true;
            HashSet<Dilek> vyzkousene = new HashSet<Dilek>();
            while (zkusitZnovu)
            {
                Smer = Hra.random.Next(0, 4);
                novy = GenerujDalsiZPredchozihoASmeru(posledni, Smer);
                if (NarazilBy(novy))
                {
                    vyzkousene.Add(novy);
                    if (vyzkousene.Count == 4)
                    {
                        novy = null;
                        zkusitZnovu = false;
                    }
                }
                else
                    zkusitZnovu = false;
            }
            return novy;
        }

        private static Dilek GenerujDalsiZPredchozihoASmeru(Dilek predchozi, int smer)
        {
            Dilek dalsi;
            switch (smer)
            {
                case 0:
                    dalsi = new(predchozi.X, predchozi.Y - 1);
                    break;
                case 1:
                    dalsi = new(predchozi.X + 1, predchozi.Y);
                    break;
                case 2:
                    dalsi = new(predchozi.X, predchozi.Y + 1);
                    break;
                case 3:
                    dalsi = new(predchozi.X - 1, predchozi.Y);
                    break;
                default:
                    throw new SpatnySmerVyjimka();
            }
            return dalsi;
        }

        private bool NarazilBy(Dilek budouciRustNeboPohyb)
        {
            if (Telo.Contains(budouciRustNeboPohyb)
               || budouciRustNeboPohyb.X < 0
               || budouciRustNeboPohyb.X >= Hra.Sirka
               || budouciRustNeboPohyb.Y < 0
               || budouciRustNeboPohyb.Y >= Hra.Vyska
                )
                return true;
            else
                return false;

        }

        private bool SezralBy()
        {
            return Pohyb.Equals(Hra.Zradlo);
        }

    }

    class SpatnySmerVyjimka : Exception
    {
        public SpatnySmerVyjimka() :
            base("Byl předán špatný směr. Musí být 0, 1, 2 nebo 3.")
        { }
    }
}
