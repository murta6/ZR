using System;
using System.Text;
using System.Security.Cryptography;
using System.Data.Entity;
using Aplikacija;
using Aplikacija.BP;
using System.Linq;

namespace Aplikacija.Model
{
    public class KorisniciAdmini
    {

        #region Admin

        public static Boolean dodajAdmina(String username, String password, String ime, String prezime)
        {
            using (Baza db = new Baza())
            {
                if(zauzetAdminUsername(username, db))
                {
                    return false;
                }
                Admin a = new Admin {username = username, password = hashPassword(password), ime = ime, prezime = prezime };
                db.Admin.Add(a);
                db.SaveChanges();
                return true;
            }
        }

        public static Boolean provjeriAdmina(String username, String password)
        {
            using (Baza db = new Baza())
            {
                String hashedPassword = hashPassword(password);
                return db.Admin.Where(a => a.username == username && a.password == hashedPassword).Count() > 0;
            }
        }

        public static Boolean zauzetAdminUsername(String username, Baza db)
        {
            return db.Admin.Where(a => a.username == username).Count() > 0;
        }

        #endregion

        #region Korisnik

        public static Boolean dodajKorisnika(String username, String password, String ime, String prezime)
        {
            using (Baza db = new Baza())
            {
                if (zauzetKorisnikUsername(username, db))
                {
                    return false;
                }
                Korisnik k = new Korisnik { username = username, password = hashPassword(password), ime = ime, prezime = prezime };
                db.Korisnik.Add(k);
                db.SaveChanges();
                return true;
            }
        }

        public static Boolean provjeriKorisnika(String username, String password)
        {
            using (Baza db = new Baza())
            {
                String hashedPassword = hashPassword(password);
                return db.Korisnik.Where(k => k.username == username && k.password == hashedPassword).Count() > 0;
            }
        }

        public static Boolean zauzetKorisnikUsername(String username, Baza db)
        {
            return db.Korisnik.Where(k => k.username == username).Count() > 0;
        }

        #endregion

        private static String hashPassword(String password)
        {
            HashAlgorithm alg = new SHA256Managed();
            var bytes = Encoding.UTF8.GetBytes(password);
            bytes = alg.ComputeHash(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
