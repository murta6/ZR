using Aplikacija.Model;
using Aplikacija.Logika;
using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplikacija.BP;

namespace Aplikacija
{
    public class Program
    {
        static void Main(string[] args)
        {
            Baza baza = new Baza();
            //KorisniciAdmini.dodajAdmina("murta", "pass", "Alen", "Murtic");
            //Expression e = new Expression("C(n, m)");
            //e.addArguments(new Argument("n", 8));
            //e.addArguments(new Argument("m", 5));
            ////e.addArguments(new Ar)
            //Console.WriteLine(e.calculate());
            Test.testirajSvaPitanja(10000);
            //foreach(var od in baza.OdnosGranula)
            //{
            //    Console.WriteLine("niza:" + Granule.vratiGranulu(od.sifraNizeGranule).nazivGranule + "visa:" + Granule.vratiGranulu(od.sifraViseGranule).nazivGranule + Odnosi.vratiOdnosZnanja(od.sifraOdnosa).nazivOdnosa);
            //}
            //foreach (var od in baza.OdnosKoncepata)
            //{
            //    Console.WriteLine("nizi:" + Koncepti.vratiKoncept(od.sifraNizegKon).nazivKoncepta + "visi:" + Koncepti.vratiKoncept(od.sifraVisegKon).nazivKoncepta + Odnosi.vratiOdnosZnanja(od.sifraOdnosa).nazivOdnosa);
            //}
            //KonkretanZadatak zad = new KonkretanZadatak("fddf", "round(n/k, 0)-round((m-1)/k, 0)", "m:>15&<70 n:>70 k:>5&<12", null, 1, 3);
            //foreach(var par in zad.parametri)
            //{
            //    Console.WriteLine(par);
            //}
            //Console.WriteLine(zad.izraz);
            //Console.WriteLine(zad.odgovor);
            Console.ReadLine();
        }
    }
}
