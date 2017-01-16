using SanEscobar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SanEscobar.Controllers
{
    public class AttractionController : Controller
    {
        private AttrDBCtxt db = new AttrDBCtxt();
        // GET: Attraction
        public ActionResult Index()
        {
            return View(db.Attractions.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="Id, Name, Desc, Added")] Attraction at)
        {
            if(ModelState.IsValid)
            {
                db.Attractions.Add(at);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(at);
        }
    }
}