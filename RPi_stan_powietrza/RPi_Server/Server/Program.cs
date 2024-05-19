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
    public class OdczyDane
    {
        public int co2;
        public int tvoc;
        public float temp;
    }
    public class Odczyt
    {
        public int port = 65000;
        public UdpClient udpServer;
        public List<OdczyDane> Dane = new List<OdczyDane>();
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
                    continue;
            }
            return klient;
        }
        public void Stop()
        {
            udpServer.Close();
        }

        public void GetReadings(int readings, IPEndPoint klient)
        {
            for (var i = 0; i < readings; i++)
            {
                byte[] data = udpServer.Receive(ref klient); // listen on port 65000
                Dane[i].co2 = BitConverter.ToInt32(data, 0);
                Dane[i].tvoc = BitConverter.ToInt32(data, 4);
                Dane[i].temp = BitConverter.ToSingle(data, 8);
            }
        }
        
        void Main(string[] args)
        {
            bool received = false;
            IOcsv file = new IOcsv();
            UdpClient udpServer = new UdpClient(65000);
            string hostname = Dns.GetHostName();
            Console.WriteLine($"Starting UDP server on {Dns.GetHostByName(hostname).AddressList[1]} port 65000\n");

            Console.WriteLine("How many readings would you like?");
            string rd = Console.ReadLine();
            Console.WriteLine("How many seconds between readings?");
            string sl = Console.ReadLine();

            int readings = Convert.ToInt32(rd);
            int sleep = Convert.ToInt32(sl);
            byte[] bytes = { (byte)readings, (byte)sleep };
            while (received != true)
            {
                Console.WriteLine("Waiting for hello");
                var remoteEP = new IPEndPoint(IPAddress.Any, 65000);
                var data = udpServer.Receive(ref remoteEP); // listen on port 65000
                Console.WriteLine($"received data({data.Length}) from {remoteEP}");
                if (data.Length == 1)
                {
                    Console.WriteLine($"Sending data {bytes}({bytes.Length})");
                    udpServer.Send(bytes, bytes.Length, remoteEP);
                    received = true;
                }
                else
                    continue;
            }

            DateTime start = DateTime.Now;
            for (var i = 0; i < readings; i++)
            {
                Console.WriteLine("Waiting for results");
                var remoteEP = new IPEndPoint(IPAddress.Any, 65000);
                var data = udpServer.Receive(ref remoteEP); // listen on port 65000
                Console.Write($"received data({data.Length}) from {remoteEP}: ");
                foreach (var item in data) Console.Write(item); Console.WriteLine();
                int co2 = BitConverter.ToInt32(data, 0);
                float tvoc = BitConverter.ToInt32(data, 4);
                float temp = BitConverter.ToSingle(data, 8);
                Console.WriteLine($"data: {co2} {tvoc} {temp}");
                udpServer.Send(new byte[] { 1 }, 1, remoteEP); // reply back
                //file.SaveCSV(co2, tvoc, temp);
            
            }

            Console.WriteLine($"Czas:  {(DateTime.Now - start).TotalSeconds}s");
            udpServer.Close();
            Console.ReadKey();
        }
    }
}