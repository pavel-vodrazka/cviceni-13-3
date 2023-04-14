using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace cviceni_13_3
{
    internal class Dilek : IEquatable<Dilek?>
    {
        public int X { get; init; }
        public int Y { get; init; }

        public Dilek(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Dilek)
                return false;
            else
                return Equals(obj as Dilek);
        }

        public bool Equals(Dilek? dilek)
        {
            if (dilek == null)
                return false;
            return X.Equals(dilek.X) && Y.Equals(dilek.Y);
        }

        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }
    }
}
