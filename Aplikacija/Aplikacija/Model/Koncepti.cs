using Aplikacija.BP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija.Model
{
    public static class Koncepti
    {
        public static Boolean dodajKoncept(String naziv, int sifraPredmeta)
        {
            using (Baza baza = new Baza())
            {
                Koncept kcp = new Koncept() { nazivKoncepta = naziv, sifraPredmeta = sifraPredmeta };
                baza.Koncept.Add(kcp);
                baza.SaveChanges();
                return true;
            }
        }

        public static Koncept vratiKoncept(int sifraKoncepta)
        {
            using (Baza baza = new Baza())
            {
                return baza.Koncept.Where(kcp => kcp.sifraKoncepta == sifraKoncepta).First();
            }
        }

        public static List<Koncept> vratiSveKonceptePredmeta(int sifraPredmeta)
        {
            using (Baza baza = new Baza())
            {
                return baza.Koncept.Where(kcp => kcp.sifraPredmeta == sifraPredmeta).ToList();
            }
        }

        public static List<Koncept> vratiOtkljucaneKoncepteKorisnika(int sifraPredmeta, int sifraKorisnika)
        {
            using (Baza baza = new Baza())
            {
                List<Koncept> list = new List<Koncept>();
                var konceptiKorisnika = baza.KorisnikKoncept.Where(kor => kor.sifraKorisnika == sifraKorisnika);
                foreach(var kon in konceptiKorisnika)
                {
                    if(zadovoljenUvjet(kon.sifraKorisnika, kon.sifraKoncepta, baza))
                    {
                        list.Add(kon.Koncept);
                    }
                }
                return list;
            }
        }

        private static Boolean zadovoljenUvjet(int sifraKorisnika, int sifraKoncepta, Baza baza)
        {
            var preduvjeti = baza.OdnosKoncepata.
                Where(kon => kon.sifraVisegKon == sifraKoncepta && kon.sifraOdnosa == 1).ToList();
            foreach(var uvjet in preduvjeti)
            {
                if(!preko50(sifraKorisnika, sifraKoncepta, baza))
                {
                    return false;
                }
            }
            return true;
        }

        private static Boolean preko50(int sifraKorisnika, int sifraKoncepta, Baza baza)
        {
            return baza.KorisnikKoncept.Where(korkon => korkon.sifraKoncepta == sifraKoncepta && korkon.sifraKorisnika == sifraKorisnika).First().znanje > 0.5;
        }

        public static void dodajKorisnikKoncept(int sifraKorisnika, int sifraKoncepta)
        {
            using(Baza baza = new Baza())
            {
                KorisnikKoncept kkp = new KorisnikKoncept();
                kkp.sifraKoncepta = sifraKoncepta;
                kkp.sifraKorisnika = sifraKorisnika;
                kkp.znanje = 0;
                baza.KorisnikKoncept.Add(kkp);
                baza.SaveChanges();
            }
        }

        public static void azurirajZnanjeKoncepta(int sifraKorisnika, int sifraKoncepta, double znanje)
        {
            using(Baza baza = new Baza())
            {
                var kk = baza.KorisnikKoncept.Where(kkp => kkp.sifraKoncepta == sifraKoncepta && kkp.sifraKorisnika == sifraKorisnika).SingleOrDefault();
                kk.znanje = znanje;
                baza.SaveChanges();
            }
        }

        public static KorisnikKoncept vratiKorisnikovKoncept(int sifraKorisnika, int sifraKoncepta)
        {
            using(Baza baza = new Baza())
            {
                return baza.KorisnikKoncept.Where(kkp => kkp.sifraKoncepta == sifraKoncepta &&
                    kkp.sifraKorisnika == sifraKorisnika).SingleOrDefault();
            }
        }

        public static List<KorisnikKoncept> vratiOtkljucaneKoncepteKorisnika(int sifraKorisnika)
        {
            using (Baza baza = new Baza())
            {
                return baza.KorisnikKoncept.Where(kkp => kkp.sifraKorisnika == sifraKorisnika).ToList();
            }
        }
    }
}
