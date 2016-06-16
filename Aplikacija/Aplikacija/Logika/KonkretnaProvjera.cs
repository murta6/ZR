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
        public int brojBodova { get; }
        public double ostvareniBrojBodova { get; set; }
        public int sifraKorisnika { get; }
        public int sifraVrsteProvjere { get; }
        public int sifraPredmeta { get; }
        private KonkretnaProvjera(int brojBodova, int sifraKorisnika, int sifraVrsteProvjere, int sifraPredmeta)
        {
            this.brojBodova = brojBodova;
            zadaci = new List<KonkretanZadatak>();
        }

        public static KonkretnaProvjera generirajProvjeru(List<int> rbrZadataka, int brojBodova,
            int sifraKorisnika, int kolicinaSlozenosti, int sifraVrsteProvjere, int sifraPredmeta)
        {
            KonkretnaProvjera provjera = new KonkretnaProvjera(brojBodova, sifraKorisnika, sifraVrsteProvjere, sifraPredmeta);
            foreach(var rbrZad in rbrZadataka)
            {
                var zad = Zadaci.vratiZadatak(rbrZad);
                int brojSlozenostiZadatka = Zadaci.vratiBrojSlozenosti(zad.sifraSlozenosti);
                var brojBodovaZadatka = brojBodova / kolicinaSlozenosti * brojSlozenostiZadatka;
                KonkretanZadatak zadatak = new KonkretanZadatak(rbrZad, zad.pitanje, zad.izraz, zad.parametri, zad.slika, zad.sifraSlozenosti, brojBodovaZadatka);
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
        public static readonly int maxParametar = 120;
        public static readonly int minParametar = 1;

        public String pitanje { get; }
        public String[] parametri { get; }
        public double odgovor { get; }
        public byte[] slika { get; }
        public int sifraSlozenosti { get; }
        public Boolean tocno { get; set; }
        public double brojBodova { get; }
        public double negativni { get; }
        public String izraz { get; }
        public double? korisnikovOdgovor { get; set; }
        public int sifraZadatka { get; }
        public int sifraGranule { get; }
        public KonkretanZadatak(int sifraZadatka, String pitanje, String izraz, String parametri, byte[] slika, int sifraSlozenosti, double brojBodova)
        {
            this.pitanje = pitanje;
            this.parametri = generirajParametre(parametri);
            this.slika = slika;
            this.sifraSlozenosti = sifraSlozenosti;
            this.izraz = izraz;
            this.brojBodova = brojBodova;
            this.negativni = brojBodova / 4;
            this.sifraGranule = Zadaci.vratiSifruGranuleZadatka(sifraZadatka);
            odgovor = izracunajOdgovor();
            korisnikovOdgovor = null;
        }

        private String[] generirajParametre(String parametri)
        {
            parametri = parametri.Trim();
            String[] par = parametri.Split(' ');
            Random rand = new Random();
            for (int i = 0; i < par.Length; i++)
            {
                String[] param = par[i].Split(':');
                Boolean uvjet = false;
                double br = 0;
                while (uvjet != true)
                {
                    br = (int)(rand.NextDouble() * maxParametar + minParametar);
                    uvjet = izracunajUvjet(param[1], br);
                }
                
                par[i] = param[0] + ":" + br.ToString();
            }
            return par;
        }

        private Boolean izracunajUvjet(string uvjet, double br)
        {
            if(uvjet == "none")
            {
                return true;
            }
            Boolean povratna = true;
            string[] uvjeti = uvjet.Split('&');
            foreach(var uvj in uvjeti)
            {
                double par = Double.Parse(uvj.Substring(1));
                if (uvj[0] == '>')
                {
                    povratna = povratna && br > par;
                }
                else if(uvj[0] == '<')
                {
                    povratna = povratna && br < par;
                }
            }
            return povratna;

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
            if (korisnikovOdgovor == null) tocno = false;
            tocno = ((odgovor - tolereancija / 100 * odgovor) < korisnikovOdgovor) && ((odgovor + tolereancija / 100 * odgovor) > korisnikovOdgovor);
        }

        public void dodajKorisnikovOdgovor(String odgovor)
        {
            try
            {
                korisnikovOdgovor = Double.Parse(odgovor);
            }
            catch (Exception)
            {
                korisnikovOdgovor = null;
            }
        }
    }
}
