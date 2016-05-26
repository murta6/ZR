using Aplikacija.BP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija.Model
{
    public static class Odnosi
    {
        #region OdnosiZnanja

        public static List<OdnosZnanja> vratiSveOdnoseZnanja()
        {
            using(Baza baza = new Baza())
            {
                return baza.OdnosZnanja.ToList();
            }
        }

        public static OdnosZnanja vratiOdnosZnanja(int sifraOdnosaZnanja)
        {
            using (Baza baza = new Baza())
            {
                return baza.OdnosZnanja.Where(oz => oz.sifraOdnosa == sifraOdnosaZnanja).First();
            }
        }

        public static Boolean urediOdnosZnanja(int sifraOdnosa, String naziv)
        {
            using(Baza baza = new Baza())
            {
                OdnosZnanja odn = baza.OdnosZnanja.Where(oz => oz.sifraOdnosa == sifraOdnosa).First();
                odn.nazivOdnosa = naziv;
                baza.SaveChanges();
                return true;
            }
        }

        #endregion

        #region OdnosGranula

        public static Boolean dodajOdnosGranula(int sifraNize, int sifraVise, int sifraOdnosa)
        {
            using(Baza baza = new Baza())
            {
                if(postojiOdnosGranula(sifraNize, sifraVise, sifraOdnosa, baza))
                {
                    return false;
                }
                OdnosGranula odn = new OdnosGranula() { sifraNizeGranule = sifraNize, sifraViseGranule = sifraVise, sifraOdnosa = sifraOdnosa };
                baza.OdnosGranula.Add(odn);
                baza.SaveChanges();
                return true;
            }
        }

        public static Boolean izbrisiOdnosGranula(int sifraNize, int sifraVise, int sifraOdnosa)
        {
            using (Baza baza = new Baza())
            {
                if (!postojiOdnosGranula(sifraNize, sifraVise, sifraOdnosa, baza))
                {
                    return false;
                }
                OdnosGranula odnos = baza.OdnosGranula.Where(odn => odn.sifraNizeGranule == sifraNize && odn.sifraViseGranule == sifraVise && odn.sifraOdnosa == sifraOdnosa).First();
                return izbrisiOdnosGranula(odnos.sifraOdnosaGranula);
            }
        }

        public static Boolean izbrisiOdnosGranula(int sifraOdnosaGranula)
        {
            using (Baza baza = new Baza())
            {
                OdnosGranula odn = baza.OdnosGranula.Where(o => o.sifraOdnosaGranula == sifraOdnosaGranula).First();
                baza.OdnosGranula.Remove(odn);
                baza.SaveChanges();
                return true;
            }
        }

        public static Boolean postojiOdnosGranula(int sifraNize, int sifraVise, int sifraOdnosa, Baza baza)
        {
            return baza.OdnosGranula.Where(odn => odn.sifraNizeGranule == sifraNize && odn.sifraViseGranule == sifraVise && odn.sifraOdnosa == sifraOdnosa).Count() > 0;
        }

        public static List<OdnosGranula> vratiOdnoseGranule(int sifraGranule)
        {
            using (Baza baza = new Baza())
            {
                return baza.OdnosGranula.Where(o => o.sifraNizeGranule == sifraGranule || o.sifraViseGranule == sifraGranule).ToList();
            }
        }

        #endregion

        #region OdnosKoncepata

        public static Boolean dodajOdnosKoncepata(int sifraNize, int sifraVise, int sifraOdnosa)
        {
            using (Baza baza = new Baza())
            {
                if (postojiOdnosKoncepata(sifraNize, sifraVise, sifraOdnosa, baza))
                {
                    return false;
                }
                OdnosKoncepata odn = new OdnosKoncepata() { sifraNizegKon = sifraNize, sifraVisegKon = sifraVise, sifraOdnosa = sifraOdnosa };
                baza.OdnosKoncepata.Add(odn);
                baza.SaveChanges();
                return true;
            }
        }

        public static Boolean izbrisiOdnosKoncepata(int sifraNize, int sifraVise, int sifraOdnosa)
        {
            using (Baza baza = new Baza())
            {
                if (!postojiOdnosKoncepata(sifraNize, sifraVise, sifraOdnosa, baza))
                {
                    return false;
                }
                OdnosKoncepata odnos = baza.OdnosKoncepata.Where(odn => odn.sifraNizegKon == sifraNize && odn.sifraVisegKon == sifraVise && odn.sifraOdnosa == sifraOdnosa).First();
                return izbrisiOdnosKoncepata(odnos.sifraOdnosa);
            }
        }

        public static Boolean izbrisiOdnosKoncepata(int sifraOdnosa)
        {
            using (Baza baza = new Baza())
            {
                OdnosKoncepata odn = baza.OdnosKoncepata.Where(o => o.sifraOdnosa == sifraOdnosa).First();
                baza.OdnosKoncepata.Remove(odn);
                baza.SaveChanges();
                return true;
            }
        }

        public static Boolean postojiOdnosKoncepata(int sifraNizeg, int sifraViseg, int sifraOdnosa, Baza baza)
        {
            return baza.OdnosKoncepata.Where(odn => odn.sifraNizegKon == sifraNizeg && odn.sifraVisegKon == sifraViseg && odn.sifraOdnosa == sifraOdnosa).Count() > 0;
        }

        public static List<OdnosKoncepata> vratiOdnoseKoncepta(int sifraKoncepta)
        {
            using (Baza baza = new Baza())
            {
                return baza.OdnosKoncepata.Where(o => o.sifraNizegKon == sifraKoncepta || o.sifraVisegKon == sifraKoncepta).ToList();
            }
        }

        #endregion
    }
}
