using Aplikacija.BP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija.Model
{
    public static class Granule
    {

        public static Dictionary<int, double> prosjecnaSlozenost = null;

        public static Boolean dodajGranulu(String naziv, int sifraKoncepta)
        {
            using(Baza baza = new Baza())
            {
                Granula gr = new Granula() { nazivGranule = naziv, sifraKoncepta = sifraKoncepta };
                baza.Granula.Add(gr);
                baza.SaveChanges();
                return true;
            }
        }

        public static Granula vratiGranulu(int sifraGranule)
        {
            using (Baza baza = new Baza())
            {
                return baza.Granula.Where(gr => gr.sifraGranule == sifraGranule).First();
            }
        }

        public static List<Granula> vratiSveGranuleKoncepta(int sifraKoncepta)
        {
            using (Baza baza = new Baza())
            {
                return baza.Granula.Where(gr => gr.sifraKoncepta == sifraKoncepta).ToList();
            }
        }

        public static List<int> vratiSifreGranulaKoncepta(int sifraKoncepta)
        {
            using (Baza baza = new Baza())
            {
                return baza.Granula.Where(gr => gr.sifraKoncepta == sifraKoncepta).Select(gr=> gr.sifraGranule).ToList();
            }
        }

        public static List<KorisnikGranula> vratiGranuleKorisnika(int sifraKorisnika)
        {
            using (Baza baza = new Baza())
            {
                return baza.KorisnikGranula.Where(korgr => korgr.sifraKorisnika == sifraKorisnika).ToList();
            }
        }

        public static List<Granula> vratiOtkljucaneGranuleKorisnika(int sifraKorisnika, List<Koncept> otkljucaniKoncepti, int sifraPredmeta)
        {
            using(Baza baza = new Baza())
            {
                List<Granula> list = new List<Granula>();
                foreach(var koncept in otkljucaniKoncepti)
                {
                    foreach(var granula in Granule.vratiSveGranuleKoncepta(koncept.sifraKoncepta))
                    {
                        if (zadovoljenUvjet(sifraKorisnika, granula.sifraGranule, baza, granula.ukupnaSlozenost))
                        {
                            list.Add(granula);
                        }
                    }
                }
                return list;
            }
        }

        public static HashSet<Zadatak> prikladniZadaciGranule(int sifraKorisnika, int sifraGranule,
            double ukupnaSlozenost, int brojZadataka, HashSet<Zadatak> zadaci)
        {
            using(Baza baza = new Baza())
            {
                double znanje = baza.KorisnikGranula.Where(kgr => kgr.sifraKorisnika == sifraKorisnika && kgr.sifraGranule == sifraGranule).First().znanje;
                double ocekivanaSlozenost = 1;
                if (znanje != 0)
                {
                    ocekivanaSlozenost = ukupnaSlozenost / prosjecnaSlozenost[sifraGranule] * znanje;
                }
                int brojDodanih = 0;
                var sviZadaci = baza.Zadatak.Where(zad => zad.sifraGranule == sifraGranule);
                //permutiraj
                sviZadaci = sviZadaci.OrderBy(a => Guid.NewGuid());
                sviZadaci = sviZadaci.OrderBy(zad => Math.Abs(zad.Slozenost.brojSlozenosti - ocekivanaSlozenost));
                foreach (var zad in sviZadaci){
                    if(brojDodanih == brojZadataka)
                    {
                        break;
                    }
                    if (!zadaci.Contains(zad))
                    {
                        zadaci.Add(zad);
                        brojDodanih++;
                    }
                }
                return zadaci;
            }
        }

        private class GranAvg
        {
            public int sifraGranule;
            public double prosjecnaSlozenost;
        }

        public static double prosjecnaSLozenostGranule(int sifraGranule)
        {
            using(Baza baza = new Baza())
            {
                if(prosjecnaSlozenost == null)
                {
                    var dict = from gran in baza.Zadatak
                               group gran by new { gran.sifraGranule } into grp
                               select new GranAvg
                               {
                                   sifraGranule = grp.Key.sifraGranule,
                                   prosjecnaSlozenost = grp.Average(gra => gra.sifraSlozenosti)
                               };
                    foreach(var d in dict)
                    {
                        prosjecnaSlozenost.Add(d.sifraGranule, d.prosjecnaSlozenost);
                    }
                }
                return prosjecnaSlozenost[sifraGranule];
            }
        }

        private static Boolean zadovoljenUvjet(int sifraKorisnika, int sifraGranule, Baza baza, double ukupnaSlozenost)
        {
            var preduvjeti = baza.OdnosGranula.
                Where(gran => gran.sifraViseGranule == sifraGranule && gran.sifraOdnosa == 1).ToList();
            foreach (var uvjet in preduvjeti)
            {
                if (!preko50(sifraKorisnika, sifraGranule, baza, ukupnaSlozenost))
                {
                    return false;
                }
            }
            return true;
        }

        private static Boolean preko50(int sifraKorisnika, int sifraGranule, Baza baza, double ukupnaSlozenost)
        {
            return baza.KorisnikGranula.Where(korgran => korgran.sifraGranule == sifraGranule && korgran.sifraKorisnika == sifraKorisnika).First().znanje > ukupnaSlozenost/2;
        }

        public static void dodajKorisnikGranula(int sifraKorisnika, int sifraGranule)
        {
            using(Baza baza = new Baza())
            {
                KorisnikGranula kgr = new KorisnikGranula();
                kgr.sifraGranule = sifraGranule;
                kgr.sifraKorisnika = sifraKorisnika;
                kgr.znanje = 0;
                baza.KorisnikGranula.Add(kgr);
                baza.SaveChanges();
            }
        }

        public static int vratiKonceptGranule(int sifraGranule)
        {
            using(Baza baza = new Baza())
            {
                return baza.Granula.Where(gr => gr.sifraGranule == sifraGranule).SingleOrDefault().sifraKoncepta;
            }
        }
    }
}
