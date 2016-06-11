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
        private static OdabranaPitanja odabirPitanja(int sifraPredmeta, int sifraKorisnika)
        {
            var otkljucaniKoncepti = Koncepti.vratiOtkljucaneKoncepteKorisnika(sifraPredmeta, sifraKorisnika);
            var granuleKorisnika = Granule.vratiGranuleKorisnika(sifraKorisnika);
            
            List<int> list = new List<int>();
            int kolicinaSlozenosti = 0;

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
