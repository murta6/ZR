using Aplikacija.BP;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija.Model
{
    public static class Predmeti
    {

        #region Predmeti

        public static List<Predmet> vratiSvePredmete()
        {
            using(Baza baza = new Baza())
            {
                return baza.Predmet.ToList();
            }
        }

        public static Predmet vratiPredmet(int sifraPredmeta)
        {
            using(Baza baza = new Baza())
            {
                return baza.Predmet.Where(p => p.sifraPredmeta == sifraPredmeta).First();
            }
        }

        public static Boolean dodajPredmet(String nazivPredmeta)
        {
            using(Baza baza = new Baza())
            {
                Predmet pr = new Predmet() { nazivPredmeta = nazivPredmeta };
                baza.Predmet.Add(pr);
                baza.SaveChanges();
                return true;
            }
        }

        public static Boolean obrisiPredmet(int sifraPredmeta)
        {
            using (Baza baza = new Baza())
            {
                Predmet predmet = baza.Predmet.Where(pr => pr.sifraPredmeta == sifraPredmeta).First();
                try
                {
                    baza.Predmet.Remove(predmet);
                    baza.SaveChanges();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        #endregion

        #region KorisnikPredmet

        public static List<KorisnikPredmet> vratiPredmeteKorisnika(int sifraKorisnika)
        {
            using(Baza baza = new Baza())
            {
                return baza.KorisnikPredmet.Where(kopr => kopr.sifraKorisnika == sifraKorisnika).ToList();
            }
        }

        public static List<KorisnikPredmet> vratiKorisnikeNaPredmetu(int sifraPredmeta)
        {
            using (Baza baza = new Baza())
            {
                return baza.KorisnikPredmet.Where(kopr => kopr.sifraPredmeta == sifraPredmeta).ToList();
            }
        }

        public static Boolean dodajKorisnikPredmet(int sifraPredmeta, int sifraKorisnika, int? ocjena)
        {
            using (Baza baza = new Baza())
            {
                if(postojiKorisnikPredmet(sifraPredmeta, sifraKorisnika, baza))
                {
                    return false;
                }
                KorisnikPredmet kopr = new KorisnikPredmet() { sifraKorisnika = sifraKorisnika, sifraPredmeta = sifraPredmeta, ocjena = ocjena };
                baza.KorisnikPredmet.Add(kopr);
                baza.SaveChanges();
                return true;
            }
        }

        public static Boolean dodajKorisnikPredmet(int sifraPredmeta, int sifraKorisnika)
        {
            return dodajKorisnikPredmet(sifraPredmeta, sifraKorisnika, null);
        }

        public static Boolean postojiKorisnikPredmet(int sifraPredmeta, int sifraKorisnika)
        {
            using(Baza baza = new Baza())
            {
                return baza.KorisnikPredmet.Where(kpr => kpr.sifraKorisnika == sifraKorisnika && kpr.sifraPredmeta == sifraPredmeta).Count() > 0;
            }
        }

        public static Boolean urediKorisnikPredmet(int sifraPredmeta, int sifraKorisnika, int ocjena)
        {
            using (Baza baza = new Baza())
            {
                KorisnikPredmet korisnikPredmet = baza.KorisnikPredmet.Where(kopr => kopr.sifraKorisnika == sifraKorisnika && kopr.sifraPredmeta == sifraPredmeta).First();
                korisnikPredmet.ocjena = ocjena;
                baza.SaveChanges();
                return true;
            }
        }

        public static Boolean urediKorisnikPredmet(int sifraKorisnikPredmet, int ocjena)
        {
            using (Baza baza = new Baza())
            {
                KorisnikPredmet korisnikPredmet = baza.KorisnikPredmet.Where(kopr => kopr.sifraKorisnikPredmet == sifraKorisnikPredmet).First();
                korisnikPredmet.ocjena = ocjena;
                baza.SaveChanges();
                return true;
            }
        }

        public static double vratiPostotakNaPredmetu(int sifraPredmeta, int sifraKorisnika)
        {
            using(Baza baza = new Baza())
            {
                var provjere = baza.Provjera.Where(prov => prov.sifraKorisnika == sifraKorisnika && prov.sifraPredmeta == sifraPredmeta && prov.sifraVrsteProvjere == Provjere.sifraIspita).ToList();
                double ukupno = 0, ostvareno = 0;
                foreach (var prov in provjere)
                {
                    ukupno += (double)prov.ostvareniBrojBodova;
                    ostvareno += prov.SkaliraniUkupniBrojBodova;
                }
                if (ukupno == 0) return 0;
                return ostvareno / ukupno * 100;
            }
        }

        public static Boolean postojiKorisnikPredmet(int sifraPredmeta, int sifraKorisnika, Baza baza)
        {
            return baza.KorisnikPredmet.Where(kopr => kopr.sifraKorisnika == sifraKorisnika && kopr.sifraPredmeta == sifraPredmeta).Count() > 0;
        }

        #endregion

    }
}
