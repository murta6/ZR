using Aplikacija.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacija.Logika
{
    public static class AppTrail
    {
        public static void AdminTrail(int sifraAdmina)
        {
            Console.WriteLine("###################################");
            Console.WriteLine("Uspješno ste prijavljeni kao admin!");
            while (true)
            {

                Console.WriteLine("Odaberite šifru predmeta koji želite uređivati: ");
                foreach(var predmet in Predmeti.vratiSvePredmete())
                {
                    ispisi("Šifra: " + predmet.sifraPredmeta + ". Naziv: " + predmet.nazivPredmeta);
                }
                string line = Console.ReadLine();
                int sifraPredmeta;
                try
                {
                    sifraPredmeta = Int32.Parse(line);
                }
                catch (Exception)
                {
                    Program.ispisiCrtu();
                    continue;
                }
                foreach(var pred in Predmeti.vratiKorisnikeNaPredmetu(sifraPredmeta))
                {
                    var korisnik = KorisniciAdmini.vratiKorisnikaZaSifru(pred.sifraKorisnika);
                    if(pred.ocjena == null)
                    {
                        var ocjena = Admin.vratiTrenutnuOcjenuKorisnikaNaPredmetu
                                        (korisnik.sifraKorisnika, pred.sifraPredmeta);
                        ispisi(korisnik.ime + " " + korisnik.prezime + " prep. ocjena = " + ocjena);
                    }
                    else
                    {
                        ispisi(korisnik.sifraKorisnika+" "+korisnik.ime + " " + korisnik.prezime + " prep. ocjena = " + pred.ocjena);
                    }
                }
                ispisi("Unesi sifru korisnika: ");
                int sifraKorisnika;
                line = Console.ReadLine();
                try
                {
                    sifraKorisnika = Int32.Parse(line);
                }
                catch (Exception)
                {
                    Program.ispisiCrtu();
                    continue;
                }
                ispisi("Generiraj provjeru za korisnika? Da?");
                line = Console.ReadLine();
                KonkretnaProvjera provjera;
                if (line.ToUpper() == "DA")
                {
                    ispisi("Želite li da se provjera automatski generira?");
                    line = Console.ReadLine();
                    if(line.ToUpper() == "DA")
                    {
                        provjera = OdabirPitanja.generirajProvjeru(20, sifraKorisnika, sifraPredmeta, Provjere.sifraIspita, new GeneriraniOdabir());
                    }
                    else
                    {
                        ispisi("Unesite broj bodova! ");
                        line = Console.ReadLine();
                        int brojBodova = Int32.Parse(line);
                        ispisi("Unesite broj zadataka! ");
                        line = Console.ReadLine();
                        int brojZadataka = Int32.Parse(line);
                        ispisi("Unesite očekivanu složenost! ");
                        line = Console.ReadLine();
                        double ocekivanaSlozenost = Double.Parse(line);
                        ispisi("Želite li unijeti min/max složenost?");
                        line = Console.ReadLine();  
                        if (line.ToUpper() == "DA")
                        {
                            ispisi("Unesite min složenost! ");
                            line = Console.ReadLine();
                            int minSlo = Int32.Parse(line);
                            ispisi("Unesite max složenost! ");
                            line = Console.ReadLine();
                            int maxSlo = Int32.Parse(line);
                            provjera = OdabirPitanja.generirajProvjeru(brojBodova, sifraKorisnika, sifraPredmeta, Provjere.sifraIspita, new ZadaniOdabir(brojBodova, minSlo, maxSlo));
                        }
                        else
                        {
                            provjera = OdabirPitanja.generirajProvjeru(brojBodova, sifraKorisnika, sifraPredmeta, Provjere.sifraIspita, new ZadaniOdabir(brojBodova));
                        }
                    }
                    ispisiKorisnikovuProvjeru(provjera);
                    ispisi("Provjera ispisana u " + Directory.GetCurrentDirectory() + "\\provjera.txt");
                }
                Program.ispisiCrtu();
            }
        }

        private static void ispisiKorisnikovuProvjeru(KonkretnaProvjera provjera)
        {
            foreach(var zad in provjera.zadaci)
            {
                ispisiUDatoteku(zad.pitanje + "\n");
                foreach(var par in zad.parametri)
                {
                    ispisiUDatoteku(par);
                }
                ispisiUDatoteku("\n");
            }
        }

        private static void ispisiUDatoteku(string s)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(Directory.GetCurrentDirectory()+"\\provjera.txt", true))
            {
                file.WriteLine(s);
            }
        }

        private static void ispisi(string s)
        {
            Console.WriteLine(s);
        }

        private static void ispisiZnanjeKorisnikaPremaKonceptima(int sifraKorisnika, int sifraPredmeta)
        {
            foreach(var kon in Koncepti.vratiSveKonceptePredmeta(sifraPredmeta)){
                var kkp = Koncepti.vratiKorisnikovKoncept(sifraKorisnika, kon.sifraKoncepta);
                Console.WriteLine(kon.nazivKoncepta + " postotak {0}", kkp.znanje * 100);
            }
        }

        public static void KorisnikTrail(int sifraKorisnika)
        {
            Console.WriteLine("######################################");
            Console.WriteLine("Uspješno ste prijavljeni kao korisnik!");
            while (true)
            {
                ispisi("Sudjelujete na predmetima: ");
                foreach(var pred in Predmeti.vratiPredmeteKorisnika(sifraKorisnika))
                {
                    ispisi(pred.sifraPredmeta +": "+Predmeti.vratiPredmet(pred.sifraPredmeta).nazivPredmeta);
                }
                ispisi("Odaberite sifru predmeta! ");
                string line = Console.ReadLine();
                int sifraPredmeta = Int32.Parse(line);
                ispisiZnanjeKorisnikaPremaKonceptima(sifraKorisnika, sifraPredmeta);
                ispisi("Želite li pisati provjeru? ");
                line = Console.ReadLine();
                if (line.ToUpper() == "DA")
                {
                    ispisi("Želite li da provjera bude ispit, a ne vježba? ");
                    line = Console.ReadLine();
                    KonkretnaProvjera provjera;
                    if (line.ToUpper() == "DA")
                    {
                        provjera = OdabirPitanja.generirajProvjeru(20, sifraKorisnika, sifraPredmeta, Provjere.sifraIspita, new GeneriraniOdabir());
                    }
                    else
                    {
                        provjera = OdabirPitanja.generirajProvjeru(20, sifraKorisnika, sifraPredmeta, Provjere.sifraVjezbe, new GeneriraniOdabir());
                    }
                    foreach(var zad in provjera.zadaci)
                    {
                        ispisi(zad.pitanje);
                        foreach(var par in zad.parametri)
                        {
                            Console.Write(par);
                        }
                        ispisi("\n" + "Vaš odgovor je: ");
                        line = Console.ReadLine();
                        zad.dodajKorisnikovOdgovor(line);
                    }
                    provjera.ocijeniProvjeru();
                    ispisi("Ostvarili ste : " + provjera.ostvareniBrojBodova + " od " + provjera.brojBodova + ".");
                    ZnanjeKorisnika.azurirajZnanjeKorisnika(provjera);
                }
                Program.ispisiCrtu();
            }
        }
    }
}
