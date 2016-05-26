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
    }
}
