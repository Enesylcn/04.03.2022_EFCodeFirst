﻿using _04._03._2022_EFCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _04._03._2022_EFCodeFirst.Controllers
{
    public class AdresController : Controller
    {
        // GET: Adres
        public ActionResult Yeni()
        {
            DatabaseContext db = new DatabaseContext();

            //LİNQ ile Yapılışı
            List<SelectListItem> kisiliste =
                (from kisi in db.Kisiler.ToList()
                 select new SelectListItem()
                 {
                     Text = kisi.Ad + " " + kisi.Soyad,
                     Value = kisi.ID.ToString()
                 }).ToList();


            //List<Kisiler> kisiler= db.Kisiler.ToList();
            //List<SelectListItem> KisiListe = new List<SelectListItem>();

            //foreach (var kisi in kisiler)
            //{
            //    SelectListItem item = new SelectListItem();
            //    item.Text = kisi.Ad + " " + kisi.Soyad;
            //    item.Value = kisi.ID.ToString();
            //    KisiListe.Add(item);

            //}

            TempData["kisiler"] = kisiliste;

            ViewBag.kisiler = kisiliste;
            return View();
        }
        [HttpPost]
        public ActionResult Yeni(Adresler adres)
        {
            DatabaseContext db = new DatabaseContext();
            Kisiler kisi = db.Kisiler.Where(x => x.ID == adres.Kisi.ID).FirstOrDefault();

            adres.Kisi = kisi;
            db.Adresler.Add(adres);
            int sonuc = db.SaveChanges();
            if (sonuc > 0)
            {
                ViewBag.mesaj = "Kişi Kaydedildi.";
                ViewBag.renk = "success";
            }
            else
            {
                ViewBag.mesaj = "Kişi Kaydedilemedi.";
                ViewBag.renk = "danger";
            }

            ViewBag.kisiler = TempData["kisiler"];
            return View();
        }
        public ActionResult Duzenle(int? adresid)
        {
            Adresler adres = new Adresler();
            DatabaseContext db = new DatabaseContext();
            if (adresid!=null)
            {
                adres = db.Adresler.Where(x => x.ID == adresid).FirstOrDefault();
            }

            List<SelectListItem> kisiliste =
               (from kisi in db.Kisiler.ToList()
                select new SelectListItem()
                {
                    Text = kisi.Ad + " " + kisi.Soyad,
                    Value = kisi.ID.ToString()
                }).ToList();
            TempData["Kisiler"] = kisiliste;
            ViewBag.kisiler = kisiliste;
            return View(adres);
        }

        [HttpPost]

        public ActionResult Duzenle(Adresler adres, int? adresid)
        {

            DatabaseContext db = new DatabaseContext();
            Adresler a = new Adresler();
            if (adresid != null)
            {
                a = db.Adresler.Where(x => x.ID == adresid).FirstOrDefault();

                Kisiler k = db.Kisiler.Where(x => x.ID == adres.Kisi.ID).FirstOrDefault();

                a.Kisi = k;
                a.AdresTanim = adres.AdresTanim;


                int sonuc = db.SaveChanges();
                if (sonuc > 0)
                {
                    ViewBag.mesaj = "Bilgiler Değiştirildi.";
                    ViewBag.renk = "success";
                }
                else
                {
                    ViewBag.mesaj = "Bilgiler Kaydedilemedi.";
                    ViewBag.renk = "danger";
                }
            }

            ViewBag.kisiler = TempData["kisiler"];
            return View(a);
        }
        public ActionResult Sil(int? adresid) //kisiid null gelebilir = ?
        {

            Adresler adres = new Adresler();
            DatabaseContext db = new DatabaseContext();
            if (adresid != null)
            {
                adres = db.Adresler.Where(x => x.ID == adresid).FirstOrDefault();

            }
            return View(adres);
        }

        [HttpPost, ActionName("Sil")]
        public ActionResult AdresSil(int? adresid)
        {

            Adresler adres = new Adresler();
            DatabaseContext db = new DatabaseContext();
            if (adresid != null)
            {
                adres = db.Adresler.Where(x => x.ID == adresid).FirstOrDefault();
                db.Adresler.Remove(adres);
                db.SaveChanges();
            }

            return RedirectToAction("Index2", "Home");
        }

    }
}