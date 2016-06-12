using Aplikacija.Model;
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
            Expression e = new Expression("binomCoeff(n, m)");
            e.addArguments(new Argument("n", 3));
            e.addArguments(new Argument("m", 10));
            //e.addArguments(new Ar)
            Console.WriteLine(e.calculate());
            Console.ReadLine();
        }
    }
}
