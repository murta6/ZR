using Aplikacija.Model;
using Aplikacija.Logika;
using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija
{
    public class Program
    {
        static void Main(string[] args)
        {
            //KorisniciAdmini.dodajAdmina("murta", "pass", "Alen", "Murtic");
            Expression e = new Expression("C(n, m)");
            e.addArguments(new Argument("n", 654.0));
            e.addArguments(new Argument("m", 5));
            //e.addArguments(new Ar)
            Console.WriteLine(e.calculate());
            Test.testirajSvaPitanja(200);
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
