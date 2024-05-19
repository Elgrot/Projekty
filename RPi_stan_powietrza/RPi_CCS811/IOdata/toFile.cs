using System.Text;
using System.IO;
using System.Linq;
using System.Globalization;
using ClientServer;
using System.Collections.Generic;

namespace RPi_C_.IOdata
{
    public class IOcsv
    {

        public void SaveCSV(string path, List<OdczytDane> Dane, bool header=true)
        {
            char separator = ',';
            StringBuilder sb = new StringBuilder();

            if (header)
            {
                string[] headings = { "CO2", "TVOC", "Temperature" };
                sb.AppendLine(string.Join(separator, headings));
            }
            foreach (var dana in Dane)
            {
                string[] values = { dana.co2.ToString(), dana.tvoc.ToString(), dana.temp.ToString(CultureInfo.InvariantCulture) };
                sb.AppendLine(string.Join(separator, values));
            }
            try
            {
                File.WriteAllText(path, sb.ToString());
            }
            catch { }

        }

        public void ReadCSV(string path, List<OdczytDane> Dane, bool header=true)
        {
            char separator = ',';
            List<string> odczytane = File.ReadAllLines(path).ToList();
            int co2, tvoc;
            float temp;
            bool CzyCo2, CzyTvoc, CzyTemp;
            
            if (header)
                odczytane = odczytane.Skip(1).ToList();

            foreach(var odczyt in odczytane)
            {
                string[] dane = odczyt.Split(separator);
                CzyCo2 = int.TryParse(dane[0], out co2);
                CzyTvoc = int.TryParse(dane[1], out tvoc);
                CzyTemp = float.TryParse(dane[2].Replace('.', separator), out temp);
                if (CzyCo2 && CzyTvoc && CzyTemp)
                    Dane.Add(new OdczytDane(co2, tvoc, temp));
            }
        }
    }
}
