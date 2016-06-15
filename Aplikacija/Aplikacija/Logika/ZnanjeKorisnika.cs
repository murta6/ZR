using Aplikacija.BP;
using Aplikacija.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija.Logika
{
    public static class ZnanjeKorisnika
    {
        public static readonly int sifraPreduvjeta = 1;


        public static Boolean dodajNovogKorisnika(String username, String password, String ime, String prezime)
        {
            Boolean dodan = KorisniciAdmini.dodajKorisnika(username, password, ime, prezime);
            if (!dodan)
            {
                throw new ArgumentException("Već postoji korisnik s navedenim usernameom");
            }
            return true;
        }

        public static void dodajKorisnikaPredmetu(int sifraKorisnika, int sifraPredmeta)
        {
            //dodaj Korisnik Predmet
            foreach (var koncept in Koncepti.vratiSveKonceptePredmeta(sifraPredmeta))
            {
                //dodaj KorisnikKoncept
                foreach (var granula in Granule.vratiSveGranuleKoncepta(koncept.sifraKoncepta))
                {

                }
            }
        }

        public static void azurirajZnanjeKorisnika(KonkretnaProvjera provjera)
        {

        }
    }
}
