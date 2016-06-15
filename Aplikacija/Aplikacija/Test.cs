using Aplikacija.Logika;
using Aplikacija.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija
{
    public static class Test
    {

        public static void testirajSvaPitanja(int brojIteracija)
        {
            int i = 0;
            Boolean sveTocno = true;
            while (i < brojIteracija)
            {
                foreach (var kon in Koncepti.vratiSveKonceptePredmeta(1))
                {
                    foreach (var zadatak in Zadaci.vratiSveZadatkeKoncepta(kon.sifraKoncepta))
                    {
                        KonkretanZadatak zad = new KonkretanZadatak(zadatak.pitanje, zadatak.izraz, zadatak.parametri, null, 1, 3);
                        //Console.WriteLine(zadatak.sifraZadatka + ": " + zad.odgovor);
                        if (zad.odgovor == Double.NaN || zad.odgovor == 0)
                        {
                            Console.WriteLine(zadatak.sifraZadatka);
                            foreach (var par in zad.parametri)
                            {
                                Console.WriteLine(par);
                            }
                            Console.WriteLine(zad.izraz);
                            sveTocno = false;
                        }
                    }
                }
                i++;
                if (i % 50 == 0) Console.WriteLine(i);
            }
            if (sveTocno)
            {
                Console.WriteLine("Svi zadaci su točno parametrizirani!");
            }
            else
            {
                Console.WriteLine("Postoji greška u parametrizaciji!");
            }
        }

    }
}
