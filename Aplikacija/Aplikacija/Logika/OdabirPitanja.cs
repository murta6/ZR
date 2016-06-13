using Aplikacija.BP;
using Aplikacija.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija.Logika
{
    public static class OdabirPitanja
    {
        private class OdabranaPitanja
        {
            public List<int> pitanja { get; set; }
            public int kolicinaSlozenosti { get; set; }
        }
        private static OdabranaPitanja odabirPitanja(int sifraPredmeta, int sifraKorisnika, int brojPitanja = 10)
        {
            var otkljucaniKoncepti = Koncepti.vratiOtkljucaneKoncepteKorisnika(sifraPredmeta, sifraKorisnika);
            var granuleKorisnika = Granule.vratiOtkljucaneGranuleKorisnika(sifraKorisnika, otkljucaniKoncepti, sifraPredmeta);
            granuleKorisnika.Permutate();
            HashSet<Zadatak> zadaci = new HashSet<Zadatak>();
            int brojZadataka = zadaci.Count;
            while (zadaci.Count < brojPitanja)
            {
                foreach (var granula in granuleKorisnika)
                {
                    zadaci = Granule.prikladniZadaciGranule(sifraKorisnika, granula.sifraGranule,
                        granula.ukupnaSlozenost, 1, zadaci);
                }
                if(brojZadataka == zadaci.Count)
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
            foreach(var zad in zadaci)
            {
                list.Add(zad.sifraZadatka);
                kolicinaSlozenosti += zad.Slozenost.brojSlozenosti;
            }
            return new OdabranaPitanja() { pitanja = list, kolicinaSlozenosti = kolicinaSlozenosti};
        }

        public static KonkretnaProvjera generirajProvjeru(double brojBodova, int sifraKorisnika, int sifraPredmeta)
        {
            var odabir = OdabirPitanja.odabirPitanja(sifraPredmeta, sifraKorisnika);
            return KonkretnaProvjera.generirajProvjeru(odabir.pitanja,
                brojBodova, sifraKorisnika, odabir.kolicinaSlozenosti);
        }

    }
}
