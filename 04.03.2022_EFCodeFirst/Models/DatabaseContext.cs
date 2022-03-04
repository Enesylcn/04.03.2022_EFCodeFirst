using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _04._03._2022_EFCodeFirst.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Kisiler> Kisiler { get; set; }
        public DbSet<Adresler> Adresler { get; set; }

        public DatabaseContext() 
        {
            VeritabaniOlustur v = new VeritabaniOlustur();
            Database.SetInitializer(v);
        }

    }

    public class VeritabaniOlustur : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context) // Database Create edildikten sonra Bu method Çalışacak.
        {
            for (int i = 0; i < 10; i++)
            {
                Kisiler k = new Kisiler();
                k.Ad = FakeData.NameData.GetFirstName();
                k.Soyad = FakeData.NameData.GetSurname();
                k.Yas = FakeData.NumberData.GetNumber(10, 80);
                context.Kisiler.Add(k);
            }

            context.SaveChanges();

            List<Kisiler> kisilistesi = context.Kisiler.ToList();
            foreach (var kisi in kisilistesi)
            {
                for (int i = 0; i < 3; i++)
                {
                    Adresler a = new Adresler();
                    a.Kisi = kisi;
                    a.AdresTanim = FakeData.PlaceData.GetAddress();
                    context.Adresler.Add(a);

                }

            }

            context.SaveChanges();

        }

    }





}