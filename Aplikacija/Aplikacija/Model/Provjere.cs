using Aplikacija.BP;
using Aplikacija.Logika;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija.Model
{
    public static class Provjere
    {
        public static int sifraIspita = 1;
        public static void dodajProvjeru(KonkretnaProvjera provjera)
        {
            using(Baza baza = new Baza())
            {
                Provjera p = new Provjera();
                p.sifraKorisnika = provjera.sifraKorisnika;
                p.sifraPredmeta = provjera.sifraPredmeta;
                p.sifraVrsteProvjere = provjera.sifraVrsteProvjere;
                p.SkaliraniUkupniBrojBodova = provjera.brojBodova;
                p.ostvareniBrojBodova = provjera.ostvareniBrojBodova;
                baza.Provjera.Add(p);
                baza.SaveChanges();
                foreach(var zadatak in provjera.zadaci)
                {
                    ProvjeraZadatak zad = new ProvjeraZadatak();
                    zad.bodovi = zadatak.brojBodova;
                    zad.negativno = zadatak.negativni;
                    if (zadatak.tocno) zad.dobiveno = zad.bodovi;
                    else zad.dobiveno = zad.negativno;
                    zad.sifraProvjere = p.sifraProvjere;
                    zad.sifraZadatka = zadatak.sifraZadatka;
                    zad.odgovor = zadatak.odgovor;
                    baza.ProvjeraZadatak.Add(zad);
                }
                baza.SaveChanges();
            }
        }

        
    }
}
