using ClientServer;
using Microsoft.Win32;
using RPi_C_.IOdata;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;

namespace RPi_CCS811
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Odczyt Server = new Odczyt();
        bool Zapisano = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Odczyt_Click(object sender, RoutedEventArgs e)
        {
            if (OdczytIlosc.Text != "" && OdczytCzas.Text != "")
            {
                int OIlosc, OCzas;
                bool CzyIlosc = int.TryParse(OdczytIlosc.Text, out OIlosc);
                bool CzyCzas = int.TryParse(OdczytCzas.Text, out OCzas);
                if (CzyIlosc && CzyCzas)
                {
                    Zapisano = false;
                    UDP_Listen(OIlosc, OCzas);
                    Pokaz_Dane(Server.Dane);
                    TEXT.Text = "Dane odebrane z czujnika.";
                }
                else
                {
                    TEXT.Text = "Wartości odczytu muszą być liczbami całkowitymi.";
                }
            }
            else
            {
                TEXT.Text = "Podaj wartości dla odczytu.";
            }
        }
        private void UDP_Listen(int Ilosc, int Czas)
        {
            UDPListen udp = new UDPListen();
            IPEndPoint klient = Server.Start(Ilosc, Czas);
            Server.CzytajZCzujnika(Ilosc, klient);
            udp.Close();
            Server.Stop();
        }

        public void Zapisz_dane_Click(object sender, RoutedEventArgs e)
        {
            if (Server.Dane.Count != 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "dane";
                sfd.DefaultExt = ".csv";
                sfd.Filter = "Comma Separated Values (.csv)|*.csv";
                Nullable<bool> result = sfd.ShowDialog();
                if (result == true)
                {
                    Zapisano = true;
                    string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                    path = System.IO.Path.Combine(path, sfd.FileName);
                    IOcsv csv = new IOcsv();
                    csv.SaveCSV(path, Server.Dane);
                    TEXT.Text = "Dane zostały zapisane do pliku: " + sfd.FileName;
                }
            }
            else
                TEXT.Text = "Brak danych z odczytu!";
        }

        private void Wczytaj_dane_Click(object sender, RoutedEventArgs e)
        {
            Server.Dane.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Comma Separated Values (.csv)|*.csv|Text files (.txt)|*.txt";
            ofd.FilterIndex = 0;
            ofd.RestoreDirectory = true;
            Nullable<bool> result = ofd.ShowDialog();
            if (result == true)
            {
                Zapisano = true;
                string path = ofd.FileName;
                IOcsv csv = new IOcsv();
                csv.ReadCSV(path, Server.Dane);
                TEXT.Text = "Odczytano dane z pliku: " + path;
                Pokaz_Dane(Server.Dane);
            }
        }
        private void Pokaz_Dane(List<OdczytDane> Dane)
        {
            OdczytDane.Visibility = Visibility.Visible;
            OdczytDane.ItemsSource = Dane;
            OdczytDane.Items.Refresh();
        }

        private void Wyjscie_Click(object sender, RoutedEventArgs e)
        {
            if (Zapisano==false)
            {
                //pytaj czy zapisać
                SaveWindow saveWindow = new SaveWindow();
                if(saveWindow.ShowDialog() == true)
                    Zapisz_dane_Click(sender, e);
            }
            Application.Current.Shutdown();
        }
    }
}
