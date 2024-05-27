using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoloryMVVM.Model
{
    public class Kolor
    {
        public byte R, G, B;

        public Kolor(byte r, byte g, byte b) 
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }
        public void Resetuj()
        {
            R = 0;
            G = 0;
            B = 0;
        }
    }

}
