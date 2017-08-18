using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server_ah
{
    class Program
    {
        static void Main(string[] args)
        {

            Program p = new Program();
            p.run();

        }

        private void run()
        {
            bool loggedIn = false;
            bool correct = false;
            int loginchoice = 0;


            Console.WriteLine("Velkommen til det bedste ah i verden.");
            Console.WriteLine("Tast 1 for login");
            Console.WriteLine("Tast 2 for oprettelse af bruger");
            while (correct == false)
            {
                string log = Console.ReadKey().KeyChar.ToString();
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
                                string newSalt = login.GetRandomSalt();
                                string hashPass = login.HashString(createPass1, newSalt);
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
                        Console.WriteLine("Du bliver nu logged in...");
                        Thread.Sleep(1000);
                        Console.WriteLine("Logger in...");
                        Thread.Sleep(1000);
                        Console.WriteLine("Logger in...");
                        Thread.Sleep(500);
                        Console.WriteLine("Du er nu logged in som: " + createUsername);

                        loggedIn = true;
                        break;
                }

            }


        }
    }
}
