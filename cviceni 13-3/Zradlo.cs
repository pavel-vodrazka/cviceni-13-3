using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace cviceni_13_3
{
    internal class Zradlo : Dilek
    {
        private Hra Hra { get; init; }
        internal bool Sezrane { get; set; }

        internal Zradlo(Hra hra) : base(Hra.random.Next(0, hra.Sirka), Hra.random.Next(0, hra.Vyska))
        {
            Hra = hra;
            Sezrane = false;
            Vykresli();
        }

        internal void Vykresli()
        {
            Hra.Obrazovka.PrekresliCtverecek(this, Hra.BarvaZradla, Hra.BarvaZradla, Hra.ZnakZradla);
        }
    }
}
