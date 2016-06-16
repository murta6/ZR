using Aplikacija.Model;
using Aplikacija.Logika;
using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplikacija.BP;
using System.Security.Cryptography;

namespace Aplikacija
{
    public class Program
    {
        static void Main(string[] args)
        {
            //KorisniciAdmini.dodajAdmina("alen", "pass", "Alen", "Murtic");
            //KorisniciAdmini.dodajKorisnika("alen", "pass", "Alen", "Murtic");

            while (true)
            {
                Console.WriteLine("Prijavite se na sustav! Testni korisnik i testni admin imaju username 'alen' i password 'pass'");
                Console.Write("Upišite username: ");
                string username = Console.ReadLine();
                Console.Write("Upišite password: ");
                string password = Console.ReadLine();
                Console.Write("Prijava kao admin - da ili ne? ");
                string disam = Console.ReadLine();
                if (disam.ToUpper() == "DA")
                {
                    if (KorisniciAdmini.provjeriAdmina(username, password))
                    {
                        AppTrail.AdminTrail(KorisniciAdmini.sifraAdminaZaUsername(username));
                        break;
                    }
                }
                else if (disam.ToUpper() == "NE")
                {
                    if (KorisniciAdmini.provjeriKorisnika(username, password))
                    {
                        AppTrail.KorisnikTrail(KorisniciAdmini.sifraKorisnikaZaUsername(username));
                        break;
                    }
                }
                Console.WriteLine("Prijava neuspješna! Pokušajte ponovno.");
                Console.WriteLine("----------------------------------------------------------------------------------------------");
            }
        }
    }
}
