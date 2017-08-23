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

        static readonly object _lock = new object();
        static readonly Dictionary<int, TcpClient> list_clients = new Dictionary<int, TcpClient>();
        int count = 1;
        static Dictionary<string, int> list_Items = new Dictionary<string, int>();
        static void Main(string[] args)
        {

            Program p = new Program();

            list_Items.Add("Skovl", 80);
            list_Items.Add("Tavle", 599);
            list_Items.Add("Bil", 155666);
            list_Items.Add("Computer", 4000);
            list_Items.Add("Hest", 8000);
            list_Items.Add("hus", 2515000);

            p.run();

        }
        //public string GetIPAddress()
        //{

        //    IPHostEntry Host = default(IPHostEntry);
        //    string Hostname = null;
        //    Hostname = System.Environment.MachineName;
        //    Host = Dns.GetHostEntry(Hostname);
        //    foreach (IPAddress IP in Host.AddressList)
        //    {
        //        if (IP.AddressFamily == AddressFamily.InterNetwork)
        //        {
        //            ipAddress = Convert.ToString(IP);
        //        }
        //    }
        //    return ipAddress;
        //}

        private void run()
        {

            TcpListener ServerSocket = new TcpListener(IPAddress.Any, 20001);
            ServerSocket.Start();
            while (true)
            {
                TcpClient client = ServerSocket.AcceptTcpClient();
                lock (_lock) list_clients.Add(count, client);
                Console.WriteLine("Someone connected!!");


                Thread t = new Thread(handle_clients);
                t.Start(count);
                count++;

            }

            Console.ReadLine();



        }

        public static void handle_clients(object o)
        {
            int id = (int)o;
            TcpClient client;

            lock (_lock) client = list_clients[id];

            while (true)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int byte_count = stream.Read(buffer, 0, buffer.Length);

                    if (byte_count == 0)
                    {
                        break;
                    }

                    string data = Encoding.ASCII.GetString(buffer, 0, byte_count);

                    Console.WriteLine(data);
                    string checkdata = data.Trim();
                    if (checkdata == "ShowItems")
                    {
                        ShowItemsOnClient(client);
                    }
                }
                catch
                {
                    
                }

            }

            lock (_lock) list_clients.Remove(id);
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        private static void ShowItemsOnClient(TcpClient client)
        {
            string sItems = "Item | Pris \r\n";
            foreach (KeyValuePair<string, int> item in list_Items)
            {
                sItems += item.Key + "      |      " + item.Value + "\r\n";
            }
            broadcastSingle(sItems, client);
        }

        public static void broadcast(string data)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

            lock (_lock)
            {
                foreach (TcpClient c in list_clients.Values)
                {
                    NetworkStream stream = c.GetStream();

                    stream.Write(buffer, 0, buffer.Length);            
                }
            }
        }

        public static void broadcastSingle(string write, TcpClient c)
        {

            byte[] buffer = Encoding.ASCII.GetBytes(write + Environment.NewLine);
            byte[] buffe = Encoding.ASCII.GetBytes("&exp" + Environment.NewLine);
            lock (_lock)
            {
                
                    NetworkStream stream = c.GetStream();

                    stream.Write(buffer, 0, buffer.Length);
                    stream.Write(buffe, 0, buffe.Length);
          
                
            }

        }


    }
}

