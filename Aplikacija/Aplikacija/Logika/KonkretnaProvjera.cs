using Aplikacija.Model;
using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija.Logika
{

    public class KonkretnaProvjera
    {
        public List<KonkretanZadatak> zadaci;
        public double brojBodova { get; }
        public double ostvareniBrojBodova { get; set; }

        private KonkretnaProvjera(double brojBodova)
        {
            this.brojBodova = brojBodova;
            zadaci = new List<KonkretanZadatak>();
        }

        public static KonkretnaProvjera generirajProvjeru(List<int> rbrZadataka, double brojBodova)
        {
            KonkretnaProvjera provjera = new KonkretnaProvjera(brojBodova);
            foreach(var rbrZad in rbrZadataka)
            {
                var zad = Zadaci.vratiZadatak(rbrZad);
                KonkretanZadatak zadatak = new KonkretanZadatak(zad.pitanje, zad.izraz, zad.parametri, zad.slika, zad.sifraSlozenosti);
                provjera.zadaci.Add(zadatak);
            }
            return provjera;
        }

        public void zapisiOdgovore(Double[] odgovori)
        {
            if(zadaci.Count != odgovori.Length)
            {
                throw new ArgumentException("Nema jednako odgovora koliko pitanja");
            }
            int i = 0;
            foreach(var zad in zadaci)
            {
                zad.korisnikovOdgovor = odgovori[i];
                i++;
            }
        }

        public void ocijeniProvjeru()
        {
            foreach (var zad in zadaci)
            {
                zad.izracunajTocnost();
            }
        }
    }

    public class KonkretanZadatak
    {

        public static readonly double tolereancija = 5.0;

        public String pitanje { get; }
        public String[] parametri { get; }
        public double odgovor { get; }
        public byte[] slika { get; }
        public int sifraSlozenosti { get; }
        public Boolean tocno { get; set; }
        public int brojBodova { get; }
        public double negativni { get; }
        public String izraz { get; }
        public double korisnikovOdgovor { get; set; }

        public KonkretanZadatak(String pitanje, String izraz, String parametri, byte[] slika, int sifraSlozenosti)
        {
            this.pitanje = pitanje;
            this.parametri = generirajParametre(parametri);
            this.slika = slika;
            this.sifraSlozenosti = sifraSlozenosti;
            this.izraz = izraz;
            odgovor = izracunajOdgovor();
        }

        private String[] generirajParametre(String parametri)
        {
            String[] par = parametri.Split(' ');
            Random rand = new Random();
            for (int i = 0; i < par.Length; i++)
            {
                String[] param = par[i].Split(':');
                var br = rand.NextDouble() * 40;
                par[i] = param[0] + ":" + br.ToString();
            }
            return par;
        }

        private double izracunajOdgovor()
        {
            Expression exp = new Expression(izraz);
            foreach(String param in parametri){
                var parametarime = param.Split(':');
                exp.addArguments(new Argument(parametarime[0], Double.Parse(parametarime[1])));
            }
            return exp.calculate();
        }

        public void izracunajTocnost()
        {
            tocno = ((odgovor - tolereancija / 100 * odgovor) < korisnikovOdgovor) && ((odgovor + tolereancija / 100 * odgovor) > korisnikovOdgovor);
        }
    }
}
