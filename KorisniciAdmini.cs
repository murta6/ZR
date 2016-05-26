using System;
using System.Text;
using System.Security.Cryptography;
using System.Data.Entity;
using Aplikacija;

namespace Aplikacija
{
    public class KorisniciAdmini
    {
        public static void dodajAdmina(String username, String password, String ime, String prezime)
        {
            using (Entities db = new Entities())
            {
                Admin a = new Admin {username = username, password = hashPassword(password), ime = ime, prezime = prezime };
                //db.Entry(a).State = EntityState.Added;
                Console.WriteLine(a.ime);
                db.Admin.Add(a);
                //db.SaveChangesAsync();
                db.SaveChanges();
            }
        }

        public static void provjeriAdmina(String username, String password)
        {
            //using (Database db = new Database())
            //{
            //    String hashedPassword = hashPassword(password);
            //    Admin ad = db.Admin.Where(a => a.username == username && a.password == hashedPassword).First();
            //    Console.WriteLine(ad.ime);
            //}
        }

        private static String hashPassword(String password)
        {
            HashAlgorithm alg = new SHA256Managed();
            var bytes = Encoding.UTF8.GetBytes(password);
            bytes = alg.ComputeHash(bytes);
            return bytes.ToString();
        }
    }
}
