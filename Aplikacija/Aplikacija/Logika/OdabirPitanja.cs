using Aplikacija.BP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija.Logika
{
    public static class OdabirPitanja
    {

        public static List<int> odabirPitanja(int sifraKorisnika)
        {

        }

        public static KonkretnaProvjera generirajProvjeru(double brojBodova, int sifraKorisnika)
        {
            return KonkretnaProvjera.generirajProvjeru(odabirPitanja(sifraKorisnika), brojBodova, sifraKorisnika);
        }

    }
}
