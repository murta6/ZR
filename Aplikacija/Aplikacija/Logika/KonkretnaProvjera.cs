using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija.Logika
{
    public class KonkretnaProvjera
    {

    }

    public class KonkretanZadatak
    {
        public String pitanje { get; }
        public String[] parametri { get; }
        private double odgovor { get; }
        public byte[] slika { get; }
        public int sifraSlozenosti { get; }
        private Boolean tocno {get; set; }
        public int brojBodova { get; }
        public double negativni { get; }

        public KonkretanZadatak(String pitanje, String izraz, String parametri, byte[] slika, int sifraSlozenosti)
        {
            this.pitanje = pitanje;
            this.parametri = generirajParametre(parametri);
            this.slika = slika;
            this.sifraSlozenosti = sifraSlozenosti;
        }

        private static String[] generirajParametre(String parametri)
        {
            String[] par = parametri.Split(' ');
            foreach(String parametar in par)
            {

            }
            return par;
        }

        private static void ocijeniOdgovor(String izraz, String[] par)
        {

        }
    }
}
