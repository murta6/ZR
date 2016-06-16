using Aplikacija.BP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija.Model
{
    public static class Zadaci
    {
        private static Dictionary<int, int> slozenost = null;

        public static List<Zadatak> vratiSveZadatkeKoncepta(int sifraKoncepta)
        {
            using(Baza baza = new Baza())
            {
                var granule = Granule.vratiSifreGranulaKoncepta(sifraKoncepta);
                return baza.Zadatak.Where(zadGran => granule.Contains(zadGran.sifraGranule)).ToList();
            }
        }

        public static List<Zadatak> vratiSveZadatkeGranule(int sifraGranule)
        {
            using (Baza baza = new Baza())
            {
                return baza.Zadatak.Where(zadGran => zadGran.sifraGranule == sifraGranule).ToList();
            }
        }

        public static Zadatak vratiZadatak(int sifraZadatka)
        {
            using (Baza baza = new Baza())
            {
                return baza.Zadatak.Where(zad => zad.sifraZadatka == sifraZadatka).First();
            }
        }

        public static Boolean urediZadatak(int sifraZadatka, String pitanje, String izraz, String parametri, byte[] slika, int sifraSlozenosti, int sifraGranule)
        {
            using (Baza baza = new Baza())
            {
                var zadatak = baza.Zadatak.Where(zad => zad.sifraZadatka == sifraZadatka).First();
                zadatak.pitanje = pitanje;
                zadatak.izraz = izraz;
                zadatak.parametri = parametri;
                zadatak.slika = slika;
                zadatak.sifraSlozenosti = sifraSlozenosti;
                zadatak.sifraGranule = sifraGranule;
                return true;
            }
        }

        public static int vratiBrojSlozenosti(int sifraSlozenosti)
        {
            if(slozenost == null)
            {
                using (Baza baza = new Baza())
                {
                    foreach(var sl in baza.Slozenost.ToList())
                    {
                        slozenost.Add(sl.sifraSlozenosti, sl.brojSlozenosti);
                    }
                }
            }
            return slozenost[sifraSlozenosti];
        }

        public static List<Zadatak> vratiZadatkeKoncepta(int sifraKoncepta, List<Granula> granuleKorisnika)
        {
            using(Baza baza = new Baza())
            {
                var rez = from zadGran in baza.Zadatak
                          join gran in granuleKorisnika
                          on zadGran.sifraGranule equals gran.sifraGranule
                          select zadGran;
                return rez.ToList();
            }
        }
        

        public static int vratiSifruGranuleZadatka(int sifraZadatka)
        {
            using(Baza baza = new Baza())
            {
                return baza.Zadatak.Where(zad => zad.sifraZadatka == sifraZadatka).Single().sifraGranule;
            }
        }
    }
}
