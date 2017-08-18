using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            Console.WriteLine("yo bitheces");
            string pass = Console.ReadLine();
            string salt = login.GetRandomSalt();
            string nytpas = login.HashString(pass, salt);

            Console.WriteLine("Din salt er: " + salt);
            Console.WriteLine(login.HashString(pass, salt));
            Console.WriteLine(login.HashString(pass, salt));
            Console.WriteLine(login.HashString(pass, salt));
            Console.WriteLine(login.HashString(pass, salt));

            Console.WriteLine(nytpas);
            Console.ReadKey();



        }
    }
}
