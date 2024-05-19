using RPi_C_.IOdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientServer
{
    public class OdczytDane
    {
        public int co2 { get; set; }
        public int tvoc { get; set; }
        public float temp { get; set; }

        public OdczytDane(int co2, int tvoc, float temp)
        {
            this.co2 = co2;
            this.tvoc = tvoc;
            this.temp = temp;
        }
    }
    public class Odczyt
    {
        public List<OdczytDane> Dane = new List<OdczytDane>();
        public UdpClient? udpServer;

        public IPEndPoint Start(int readings, int sleep)
        {
            udpServer = new UdpClient(65000);
            bool received = false;
            var klient = new IPEndPoint(IPAddress.Any, 65000);
            byte[] bytes = { (byte)readings, (byte)sleep };

            while (received != true)
            {
                var data = udpServer.Receive(ref klient); // listen on port 65000
                if (data.Length == 1)
                {
                    udpServer.Send(bytes, bytes.Length, klient);
                    received = true;
                }
                else
                    received=true;
            }
            return klient;
        }
        public void Stop()
        {
            udpServer.Close();
        }

        public void CzytajZCzujnika(int readings, IPEndPoint klient)
        {
            for (var i = 0; i < readings; i++)
            {
                byte[] data = udpServer.Receive(ref klient); // listen on port 65000
                Console.WriteLine($"received data({data.Length}) from {klient}");
                var co2 = BitConverter.ToInt32(data, 0);
                var tvoc = BitConverter.ToInt32(data, 4);
                var temp = BitConverter.ToSingle(data, 8);
                Dane.Add(new OdczytDane(co2,tvoc,temp));
            }
        }
    }
}