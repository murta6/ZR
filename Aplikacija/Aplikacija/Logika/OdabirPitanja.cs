using Aplikacija.BP;
using Aplikacija.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija.Logika
{

    public class OdabranaPitanja
    {
        public List<int> pitanja { get; set; }
        public int kolicinaSlozenosti { get; set; }
    }
    public static class OdabirPitanja
    {

        public static KonkretnaProvjera generirajProvjeru(int brojBodova, int sifraKorisnika, int sifraPredmeta, int sifraVrsteProvjere, IOdabir odabirator)
        {
            var odabir = odabirator.odaberiPitanja(sifraPredmeta, sifraKorisnika);
            return KonkretnaProvjera.generirajProvjeru(odabir.pitanja,
                brojBodova, sifraKorisnika, odabir.kolicinaSlozenosti, sifraVrsteProvjere, sifraPredmeta);
        }

    }
    public interface IOdabir
    {
        OdabranaPitanja odaberiPitanja(int sifraPredmeta, int sifraKorisnika, int brojPitanja = 10);
    }

    public class ZadaniOdabir : IOdabir
    {
        double ocekivanaSlozenost;
        int minSlozenost;
        int maxSlozenost;
        public ZadaniOdabir(double ocekivanaSlozenost, int minSlozenost = 0, int maxSlozenost = 6)
        {
            this.ocekivanaSlozenost = ocekivanaSlozenost;
            this.minSlozenost = minSlozenost;
            this.maxSlozenost = maxSlozenost;
        }
        public OdabranaPitanja odaberiPitanja(int sifraPredmeta, int sifraKorisnika, int brojPitanja = 10)
        {
            var otkljucaniKoncepti = Koncepti.vratiOtkljucaneKoncepteKorisnika(sifraPredmeta, sifraKorisnika);
            var granuleKorisnika = Granule.vratiOtkljucaneGranuleKorisnika(sifraKorisnika, otkljucaniKoncepti, sifraPredmeta);
            granuleKorisnika.Permutate();
            HashSet<Zadatak> zadaci = new HashSet<Zadatak>();
            HashSet<int> sifreZad = new HashSet<int>();
            int brojZadataka = zadaci.Count;
            while (zadaci.Count < brojPitanja)
            {
                foreach (var granula in granuleKorisnika)
                {
                    zadaci = Granule.zadaniZadaciGranule(sifraKorisnika, granula.sifraGranule,
                        granula.ukupnaSlozenost, 1, zadaci, sifreZad, ocekivanaSlozenost, minSlozenost, maxSlozenost);
                }
                if (brojZadataka == zadaci.Count)
                {
                    break;
                }
                else
                {
                    brojZadataka = zadaci.Count;
                }
            }
            int kolicinaSlozenosti = 0;
            List<int> list = new List<int>();
            foreach (var zad in zadaci)
            {
                list.Add(zad.sifraZadatka);
                kolicinaSlozenosti += zad.Slozenost.brojSlozenosti;
            }
            return new OdabranaPitanja() { pitanja = list, kolicinaSlozenosti = kolicinaSlozenosti };
        }
    }

    public class GeneriraniOdabir : IOdabir
    {
        public OdabranaPitanja odaberiPitanja(int sifraPredmeta, int sifraKorisnika, int brojPitanja = 10)
        {
            var otkljucaniKoncepti = Koncepti.vratiOtkljucaneKoncepteKorisnika(sifraPredmeta, sifraKorisnika);
            var granuleKorisnika = Granule.vratiOtkljucaneGranuleKorisnika(sifraKorisnika, otkljucaniKoncepti, sifraPredmeta);
            granuleKorisnika.Permutate();
            HashSet<Zadatak> zadaci = new HashSet<Zadatak>();
            HashSet<int> sifreZad = new HashSet<int>();
            int brojZadataka = zadaci.Count;
            while (zadaci.Count < brojPitanja)
            {
                foreach (var granula in granuleKorisnika)
                {
                    zadaci = Granule.prikladniZadaciGranule(sifraKorisnika, granula.sifraGranule,
                        granula.ukupnaSlozenost, 1, zadaci, sifreZad);
                }
                if (brojZadataka == zadaci.Count)
                {
                    break;
                }
                else
                {
                    brojZadataka = zadaci.Count;
                }
            }
            int kolicinaSlozenosti = 0;
            List<int> list = new List<int>();
            foreach (var zad in zadaci)
            {
                list.Add(zad.sifraZadatka);
                kolicinaSlozenosti += zad.sifraSlozenosti;
            }
            return new OdabranaPitanja() { pitanja = list, kolicinaSlozenosti = kolicinaSlozenosti };
        }
    }
}
