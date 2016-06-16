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
        public static readonly double faktorKoristenja = 0.4;
        public static readonly double faktorAnalognosti = 0.2;
        public static readonly double faktorVaznosti = 5;

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
            Predmeti.dodajKorisnikPredmet(sifraPredmeta, sifraKorisnika);
            foreach (var koncept in Koncepti.vratiSveKonceptePredmeta(sifraPredmeta))
            {
                Koncepti.dodajKorisnikKoncept(sifraKorisnika, koncept.sifraKoncepta);
                foreach (var granula in Granule.vratiSveGranuleKoncepta(koncept.sifraKoncepta))
                {
                    Granule.dodajKorisnikGranula(sifraKorisnika, granula.sifraGranule);
                }
            }
        }

        public static void azurirajZnanjeKorisnika(KonkretnaProvjera provjera)
        {
            //Provjere.dodajProvjeru(provjera);
            azurirajZnanjeGranula(provjera);
            azurirajZnanjeKoncepata(provjera.sifraKorisnika);
        }

        public static int trenutnaZakljucnaOcjenaKorisnika(int sifraKorisnika, int sifraPredmeta)
        {
            var postotak = Predmeti.vratiPostotakNaPredmetu(sifraPredmeta, sifraKorisnika);
            if (postotak < 50) return 1;
            else if (postotak < 60) return 2;
            else if (postotak < 70) return 3;
            else if (postotak < 85) return 4;
            else return 5;
        }

        public static void zakljuciOcjenuKorisnika(int sifraKorisnika, int sifraPredmeta)
        {
            Predmeti.urediKorisnikPredmet(sifraPredmeta, sifraKorisnika,
                trenutnaZakljucnaOcjenaKorisnika(sifraKorisnika, sifraPredmeta));
        }

        private static void azurirajZnanjeKoncepata(int sifraKorisnika)
        {
            using(Baza baza = new Baza())
            {
                var kon = Koncepti.vratiKoncepteKorisnika(sifraKorisnika);
                foreach(var koncept in kon)
                {
                    double znanje = azurirajZnanjeKoncepta(baza, koncept.sifraKoncepta, sifraKorisnika);
                    Koncepti.azurirajZnanjeKoncepta(sifraKorisnika, koncept.sifraKoncepta, znanje);
                }
                baza.SaveChanges();
            }
        }

        private static double azurirajZnanjeKoncepta(Baza baza, int sifraKoncepta, int sifraKorisnika)
        {
            Razlomak razlomak = new Razlomak();
            foreach (var gran in Granule.vratiSveGranuleKoncepta(sifraKoncepta))
            {
                double znanjeGran = znanjeGranule(baza, gran);
                razlomak.brojnik += faktorVaznosti * znanjeGran;
                razlomak.nazivnik += faktorVaznosti;
            }
            azurirajKoncepteSOdnosom(baza, sifraKoncepta, razlomak, faktorAnalognosti, Odnosi.Analogno);
            azurirajKoncepteSOdnosom(baza, sifraKoncepta, razlomak, 1, Odnosi.Podskup);
            azurirajKoncepteSOdnosom(baza, sifraKoncepta, razlomak, faktorKoristenja, Odnosi.Koristenje);
            return razlomak.izracunaj();
        }

        private class Razlomak
        {
            public double brojnik;
            public double nazivnik;
            public Razlomak()
            {
                brojnik = 0;
                nazivnik = 0;
            }
            public double izracunaj()
            {
                if (nazivnik == 0)
                {
                    return 0;
                }
                return brojnik / nazivnik;
            }
        }

        private static void azurirajKoncepteSOdnosom(Baza baza, int sifraKoncepta, Razlomak razlomak, double faktor, int sifraOdnosa)
        {
            foreach (var kon in Odnosi.vratiKoncepteSOdnosom(sifraKoncepta, sifraOdnosa))
            {
                foreach (var gran in Granule.vratiSveGranuleKoncepta(kon.sifraVisegKon))
                {
                    double znanjeGran = znanjeGranule(baza, gran);
                    razlomak.brojnik += faktorVaznosti * znanjeGran * faktor;
                    razlomak.nazivnik += faktorVaznosti * faktor;
                }
            }
        }

        private static double znanjeGranule(Baza baza, Granula gran)
        {
            var korisnikGranula = baza.KorisnikGranula.Where(kgr => kgr.sifraGranule == gran.sifraGranule).SingleOrDefault();
            double usvGranule;
            if (korisnikGranula.znanje > gran.ukupnaSlozenost)
            {
                usvGranule = 1;
            }
            else if (korisnikGranula.znanje < 0)
            {
                usvGranule = 0;
            }
            else
            {
                usvGranule = korisnikGranula.znanje;
            }
            return usvGranule;
        }

        private static void azurirajZnanjeGranula(KonkretnaProvjera provjera)
        {
            using(Baza baza = new Baza())
            {
                foreach(var zad in provjera.zadaci)
                {
                    int brojSlozenosti = zad.sifraSlozenosti;
                    if (!zad.tocno)
                    {
                        brojSlozenosti *= -1;
                    }
                    azurirajZnanjeGranule(baza, provjera.sifraKorisnika, zad.sifraGranule, brojSlozenosti);
                }
                baza.SaveChanges();
            }
        }

        private static void azurirajZnanjeGranule(Baza baza, int sifraKorisnika, int sifraGranule, int promjena)
        {
            var gran = baza.KorisnikGranula.Where(kgr =>
                kgr.sifraKorisnika == sifraKorisnika && kgr.sifraGranule == sifraGranule).SingleOrDefault();
            gran.znanje += promjena;
            azurirajPovezaneGranule(baza, sifraKorisnika, sifraGranule, promjena, Odnosi.Koristenje, faktorKoristenja);
            azurirajPovezaneGranule(baza, sifraKorisnika, sifraGranule, promjena, Odnosi.Analogno, faktorAnalognosti);
            azurirajPovezaneGranule(baza, sifraKorisnika, sifraGranule, promjena, Odnosi.Podskup, 1);
        }

        private static void azurirajPovezaneGranule(Baza baza, int sifraKorisnika, int sifraGranule, int promjena, int sifraOdnosa, double faktor)
        {
            foreach (var koristenaGranula in baza.OdnosGranula.Where(gr => gr.sifraNizeGranule == sifraGranule && gr.sifraOdnosa == sifraOdnosa))
            {
                var gran = baza.KorisnikGranula.Where(kgr =>
    kgr.sifraKorisnika == sifraKorisnika && kgr.sifraGranule == sifraGranule).SingleOrDefault();
                gran.znanje += promjena * faktor;
            }
        }
    }
}
