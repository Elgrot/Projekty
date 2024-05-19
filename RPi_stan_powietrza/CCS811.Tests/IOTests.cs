using ClientServer;
using Newtonsoft.Json;
using RPi_C_.IOdata;

namespace CCS811.Tests
{
    [TestClass]
    public class IOTests
    {
        int[] co2data = { 500, 510, 520, 530 };
        int[] tvocdata = { 0, 1, 2, 3 };
        float[] tempdata = { 19.5592f, 20.5592f, 21.5592f, 22.5592f };
        string path = "./test.csv";

        // Czy zapisane dane po odczytaniu sa takie same
        // Czy odczytywanie radzi sobie z czytaniem plików z/bez nag³ówków
        // Czas oczekiwania jak wybrano?
        // Iloœæ odczytów zgodna?
        [TestMethod]
        public void ZapisOdczytTest()
        {
            IOcsv csv = new();
            List<OdczytDane> dane = new();
            List<OdczytDane> dane1 = new();
            
            for (int i = 0; i < co2data.Length; i++)
                dane.Add(new OdczytDane(co2data[i], tvocdata[i], tempdata[i]));
            if (File.Exists(path))
                File.Delete(path);
            csv.SaveCSV(path, dane);
            csv.ReadCSV(path, dane1);
            File.Delete(path);
            Assert.AreEqual(
                JsonConvert.SerializeObject(dane),
                JsonConvert.SerializeObject(dane1));       
        }
        [TestMethod]
        public void ZapisOdczytBezNaglowkaTest()
        {
            IOcsv csv = new();
            List<OdczytDane> dane = new();
            List<OdczytDane> dane1 = new();
            for (int i = 0; i < co2data.Length; i++)
                dane.Add(new OdczytDane(co2data[i], tvocdata[i], tempdata[i]));
            if (File.Exists(path))
                File.Delete(path);
            csv.SaveCSV(path, dane, false);
            csv.ReadCSV(path, dane1, false);
            File.Delete(path);
            Assert.AreEqual(
                JsonConvert.SerializeObject(dane),
                JsonConvert.SerializeObject(dane1));
        }

    }
}