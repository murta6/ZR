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


        /*public static Boolean dodajNovogKorisnika(String username, String password, String ime, String prezime)
        {
            Boolean dodan = KorisniciAdmini.dodajKorisnika(username, password, ime, prezime);
            if (!dodan)
            {
                throw new ArgumentException("Već postoji korisnik s navedenim usernameom");
            }
            int sifraKorisnika = KorisniciAdmini.sifraZaUsername(username);
            dodajKorisnikePredmetima(sifraKorisnika);
            return true;
        }

        public static void dodajKorisnikePredmetima(int sifraKorisnika)
        {
            foreach(Predmet predmet in Predmeti.vratiSvePredmete())
            {
                Predmeti.dodajKorisnikPredmet(predmet.sifraPredmeta, sifraKorisnika);
                dodajKoncepteKorisniku(predmet.sifraPredmeta, sifraKorisnika);
            }
        }

        public static void dodajKoncepteKorisniku(int sifraPredmeta, int sifraKorisnika)
        {
            foreach(Koncept kon in Koncepti.vratiSveKonceptePredmetaBezUvjeta(sifraPredmeta, sifraPreduvjeta))
            {
                Koncepti.dodajKorisnikaKonceptu(sifraKorisnika, kon.sifraKoncepta);
            }
        }

        public static Boolean dozvolaOtvaranjaKoncepta(int sifraKorisnika, int sifraPredmeta)
        {

        }

        public static void otvoriKoncepte(int sifraKorisnika, int sifraPredmeta)
        {
            foreach(Koncept kon in Koncepti.vratiSveKonceptePredmeta(sifraPredmeta))
            {
                return false;
            }
        }*/
    }
}
