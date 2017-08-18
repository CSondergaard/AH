using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server_ah
{
    class Program
    {

        string ipAddress;
        static void Main(string[] args)
        {

            Program p = new Program();
            p.run();

        }
        public string GetIPAddress()
        {

            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipAddress = Convert.ToString(IP);
                }
            }
            return ipAddress;

        }


        private void run()
        {
            bool loggedIn = false;
            bool correct = false;
            int loginchoice = 0;

            TcpListener server = new TcpListener(IPAddress.Any, 20001);
            server.Start();

            Socket client = server.AcceptSocket();
            NetworkStream stream = new NetworkStream(client);
            StreamReader sr = new StreamReader(stream);
            StreamWriter sw = new StreamWriter(stream);
            sw.AutoFlush = true;

            ipAddress = GetIPAddress();

            Console.WriteLine("Der er nu forbindelse til: " + ipAddress);

            while(true)
            {

            }

            sr.Close();
            sw.Close();
            client.Close();

        }


    }
}

