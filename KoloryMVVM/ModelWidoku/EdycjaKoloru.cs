using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoloryMVVM.ModelWidoku
{
    using Model;
    using System.Windows.Input;
    using System.Windows.Media;

    public class EdycjaKoloru : ObservedObject
    {
        private readonly Kolor model = Ustawienia.Czytaj();
        // Start kolorów
        public byte R
        {
            get
            {
                return model.R;
            }
            set
            {
                model.R = value;
                onPropertyChanged(nameof(R));//, nameof(Kolor));
            }
        }
        public byte G
        {
            get
            {
                return model.G;
            }
            set
            {
                model.G = value;
                onPropertyChanged(nameof(G));//, nameof(Kolor));
            }
        }
        public byte B
        {
            get
            {
                return model.B;
            }
            set
            {
                model.B = value;
                onPropertyChanged(nameof(B));//, nameof(Kolor));
            }
        }
        // Koniec kolorów
        //public Color Kolor
        //{
        //    get
        //    {
        //        return Color.FromRgb(model.R, model.G, model.B);
        //    }
        //}


        private ICommand resetuj = null;
        public ICommand Resetuj
        {
            get
            {
                if (resetuj == null) resetuj = new RelayCommand(
                    (object p) => 
                    {
                        model.Resetuj();
                        onPropertyChanged(nameof(R), nameof(G), nameof(B));
                    },
                    (object p) =>
                    {
                        return model.R != 0 || model.G != 0 || model.B != 0;
                    });
                return resetuj;
            }
        }

        public void _Zapisz()
        {
            Ustawienia.Zapisz(model);
        }

        public ICommand Zapisz
        {
            get
            {
                return new RelayCommand((object p) => { _Zapisz(); });
            }
        }
    }
}
