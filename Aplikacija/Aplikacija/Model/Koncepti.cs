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

        public static List<Koncept> vratiSveKonceptePredmetaBezUvjeta(int sifraPredmeta, int sifraOdnosa)
        {
            using (Baza baza = new Baza())
            {
                //return baza.Koncept.Where(kcp => kcp.sifraPredmeta == sifraPredmeta).ToList();
            }
        }

        public static Boolean dodajKorisnikaKonceptu(int sifraKorisnika, int sifraKoncepta, double? znanje = null)
        {
            using (Baza baza = new Baza())
            {
            }
        }
    }
}
