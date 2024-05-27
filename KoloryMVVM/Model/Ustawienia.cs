using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KoloryMVVM.Model
{
    static class Ustawienia
    {
        public static Kolor Czytaj()
        {
            Properties.Settings ustawienia = Properties.Settings.Default;
            return new Kolor(ustawienia.R, ustawienia.G, ustawienia.B);
        }

        public static void Zapisz(Kolor model)
        {
            Properties.Settings ustawienia = Properties.Settings.Default;
            ustawienia.R = model.R;
            ustawienia.G = model.G;
            ustawienia.B = model.B;
            ustawienia.Save();
        }
    }
}
