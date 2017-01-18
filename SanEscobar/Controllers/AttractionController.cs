using SanEscobar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
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

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attraction at = db.Attractions.Find(id);
            if (at == null)
            {
                return HttpNotFound();
            }
            db.Attractions.Remove(at);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include ="Id, Name, Desc, Added, Image")] Attraction at)
        {
            if(ModelState.IsValid)
            {
                string dir = "images/attractions/";
                HttpPostedFileBase photo = Request.Files["Image"];
                if(photo!=null && photo.ContentLength>0)
                {
                    var filename = Path.GetFileName(photo.FileName);
                    photo.SaveAs(Path.Combine(dir, filename));
                    at.Image = dir + at.Id + filename;
                }
                
                db.Attractions.Add(at);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(at);
        }

        [Authorize]
        [HttpPost]
        public string UploadIMage()
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
    Request.ApplicationPath.TrimEnd('/') + "/";
            string dir = "images\\attractions\\";
            string savepath = baseUrl + dir;
            string rt = "";
            var i = Request.Files.Count;
            HttpPostedFileBase photo = Request.Files["file"];

            if (photo != null && photo.ContentLength > 0)
            {
                //string filename = Path.GetFileName(photo.FileName);
                string filename = "images\\attractions\\";
                filename += GenImgName(new Random((int)DateTime.Now.Ticks), 10);
                
                filename += Path.GetFileName(photo.FileName);
                string fullPath = Path.Combine(Server.MapPath("~"), filename);
                rt = filename;
                photo.SaveAs(fullPath);
            }
            return rt;
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attraction atrakcje = db.Attractions.Find(id);
            if (atrakcje == null)
            {
                return HttpNotFound();
            }
            return View(atrakcje);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Desc,Added,Image")] Attraction atrakcje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(atrakcje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(atrakcje);
        }

        public char GenChar(System.Random random)
        {
            char rdm;
            int n = random.Next(0, 60);
            if(n<10)
            {
                rdm = (char)(n + '0');
            }else if (n < 36)
            {
                rdm = (char)(n - 10 + 'A');
            }
            else
            {
                rdm = (char)(n - 36 + 'a');
            }
            return rdm;
        }

        public string GenImgName(System.Random random, int lenght)
        {
            char[] r = new char[lenght];
            for(int i = 0; i<lenght; i++)
            {
                r[i] = GenChar(random);
            }
            return new string(r);
        }
    }
}