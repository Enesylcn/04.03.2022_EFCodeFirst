using _04._03._2022_EFCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace _04._03._2022_EFCodeFirst.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
       

        public ActionResult Index2()
        {
            DatabaseContext db = new DatabaseContext();
            List<Kisiler> Kisiler = db.Kisiler.ToList();

            KisilerveAdresler model = new KisilerveAdresler();
            model.Kisiler = db.Kisiler.ToList();
            model.Adresler = db.Adresler.ToList();
            return View(model);
        }

    }
}