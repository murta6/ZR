//using Aplikacija.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Aplikacija.Logika
//{
//    public static class Admin
//    {
//        public static void dodajKorisnikaNaPredmet(int sifraKorisnika, int sifraPredmeta)
//        {
//            if(!Predmeti.postojiKorisnikPredmet(sifraPredmeta, sifraKorisnika))
//            {
//                Predmeti.dodajKorisnikPredmet(sifraPredmeta, sifraKorisnika);
//                foreach(var kon in Koncepti.vratiSveKonceptePredmeta(sifraPredmeta))
//                {
//                    Koncepti.dodajKorisnikKoncept(kon.sifraKoncepta, sifraKorisnika);
//                    foreach(var gran in Granule.vratiSveGranuleKoncepta(kon.sifraKoncepta))
//                    {
//                        Granule.dodajKorisnikGranula(sifraKorisnika, gran.sifraGranule);
//                    }
//                }
//            }
//        }

//        public static ocijeniKorisnikaNaPredmetu(int sifraKorisnika, int sifraPredmeta)
//        {

//        }

//        public static void pogledajRezultateKorisnika
//    }
//}
