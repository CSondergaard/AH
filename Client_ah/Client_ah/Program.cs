﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Net.Http;

namespace Client_ah
{
    class Program
    {
        static int loginchoice = 0;
        static string brugernavn = "";
        static TcpClient client;
        static StreamReader sr;
        static StreamWriter sw;
        static NetworkStream stream;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.run();
        }
        public void run()
        {
            bool con = true;
            while (con)
            {
                try
                {
                    client = new TcpClient("localhost", 20001);
                    con = false;
                }
                catch
                {
                    con = true;
                    Thread.Sleep(2000);

                }


            }





            stream = client.GetStream();
            sr = new StreamReader(stream);
            sw = new StreamWriter(stream);
            sw.AutoFlush = true;

            loginChoice();

            client.Close();
        }



        private static void loginChoice()
        {
            bool correct = false;


            Console.WriteLine("Velkommen til det bedste ah i verden.");
            Console.WriteLine("Tast 1 for login");
            Console.WriteLine("Tast 2 for oprettelse af bruger");
            while (correct == false)
            {
                string log = Console.ReadLine();
                correct = int.TryParse(log, out loginchoice);
                if (correct)
                {
                    if (loginchoice == 1 || loginchoice == 2)
                    {

                    }
                    else
                        correct = false;
                }
                if (correct == false)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Forkert indtastning");
                }

            }

            login();

        } // Login choice

        private static void login()
        {
            bool loggedIn = false;

            while (loggedIn == false)
            {
                switch (loginchoice)
                {
                    case 1:
                        bool succes = false;
                        Console.WriteLine("Indtast brugernavn:");
                        string username = Console.ReadLine();
                        Console.Write("Indtasts kodeord:");
                        string password = Console.ReadLine();


                        if (succes)
                        {
                            loggedIn = true;
                        }
                        // Check for login

                        brugernavn = username;



                        break;
                    case 2:
                        Console.WriteLine("Indtast ønskede brugernavn:");
                        string createUsername = Console.ReadLine();
                        // Check for om brugernavn eksistere

                        bool samePass = false;
                        while (samePass == false)
                        {
                            Console.Write("Indtast kodeord:");
                            string createPass1 = Console.ReadLine();
                            Console.Write("Skriv kodeord igen:");
                            string createPass2 = Console.ReadLine();

                            if (createPass1 == createPass2)
                            {
                                login log = new login();

                                string newSalt = log.GetRandomSalt();
                                string hashPass = log.HashString(createPass1, newSalt);
                                samePass = true;
                                //Skriv til something
                            }
                            else
                            {
                                Console.WriteLine("Kodeord er ikke det samme!");
                            }

                        }
                        Console.Clear();
                        Console.WriteLine("Din bruger er nu oprettet");
                        brugernavn = createUsername;

                        loggedIn = true;
                        break;

                }

                Console.WriteLine("Du bliver nu logged in...");
                Thread.Sleep(700);
                Console.WriteLine("Logger in...");
                Thread.Sleep(1300);
                Console.WriteLine("Logger in...");
                Thread.Sleep(500);
                Console.WriteLine("Du er nu logged in som: " + brugernavn);


                ahui();

            }
        }

        private static void ahui()
        {
            Console.WriteLine("Hvad vil du ??");
            Console.WriteLine("1 - Se ting på ah");
            Console.WriteLine("2  byd på ting på ah");
            Console.WriteLine("3 - log ud");

            int menu;
            string log = Console.ReadLine();
            bool correct = int.TryParse(log, out menu);
            if (correct)

                switch (menu)
                {
                    case 1:
                        ShowItems();
                        break;
                    case 2:
                        BidOnItems();
                        break;
                    case 3:
                        LogOut();
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }

            {
            }


        }

        private static void ShowItems()
        {

            sw.Write("ShowItems");
            while (true)
            {
                byte[] buffer = new byte[client.ReceiveBufferSize];
                int byte_count = stream.Read(buffer, 0, client.ReceiveBufferSize);

                if (byte_count == 0)
                {
                    break;
                }

                string data = Encoding.ASCII.GetString(buffer, 0, byte_count);
                Console.WriteLine(data);
            }

            Console.WriteLine(sr.ReadLine());
            Console.ReadLine();


        }
        private static void BidOnItems()
        {

        }

        private static void LogOut()
        {
            string ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
            sw.WriteLine(ip + " " + "Disconnected");
            Environment.Exit(0);
        }


    }
}