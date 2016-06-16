using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija.Logika
{
    public static class AppTrail
    {
        public static void AdminTrail(int sifraAdmina)
        {
            Console.WriteLine("###################################");
            Console.WriteLine("Uspješno ste prijavljeni kao admin!");
            Console.ReadLine();
        }

        public static void KorisnikTrail(int sifraKorisnika)
        {
            Console.WriteLine("######################################");
            Console.WriteLine("Uspješno ste prijavljeni kao korisnik!");
            Console.ReadLine();
        }
    }
}
