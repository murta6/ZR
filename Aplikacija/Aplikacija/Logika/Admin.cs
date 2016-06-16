using Aplikacija.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija.Logika
{
    public static class Admin
    {
        public static void dodajKorisnikaNaPredmet(int sifraKorisnika, int sifraPredmeta)
        {
            ZnanjeKorisnika.dodajKorisnikaPredmetu(sifraKorisnika, sifraPredmeta);
        }

        public static void ocijeniKorisnikaNaPredmetu(int sifraKorisnika, int sifraPredmeta)
        {
            ZnanjeKorisnika.zakljuciOcjenuKorisnika(sifraKorisnika, sifraPredmeta);
        }

        public static int vratiTrenutnuOcjenuKorisnikaNaPredmetu(int sifraKorisnika, int sifraPredmeta)
        {
            return ZnanjeKorisnika.trenutnaZakljucnaOcjenaKorisnika(sifraKorisnika, sifraPredmeta);
        }

        //public static void pogledajRezultateKorisnika
    }
}
