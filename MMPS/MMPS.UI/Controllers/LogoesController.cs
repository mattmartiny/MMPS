using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MMPS.DATA;
using PagedList;

namespace MMPS.UI.Controllers
{
    public class LogoesController : Controller
    {
        private MartinyFolioEntities1 db = new MartinyFolioEntities1();

        // GET: Logoes
        public ActionResult Index(int? page, int pageSize = 3)
        {

            int pageNumber = page ?? 1;
            var pageOfLogos = db.Logos.ToList().OrderBy(x => x.LogoOrder).ToPagedList(pageNumber, pageSize);
            var logos = db.Logos.Include(l => l.Type);
            return View(pageOfLogos);
        }

        // GET: Logoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logo logo = db.Logos.Find(id);
            if (logo == null)
            {
                return HttpNotFound();
            }
            return View(logo);
        }
        [Authorize(Roles = "Admin")]
        // GET: Logoes/Create
        public ActionResult Create()
        {
            ViewBag.typeID = new SelectList(db.Types, "typeID", "typeName");
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: Logoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "imageID,imageName,description,imageSource,typeName,typeID,alt, LogoOrder")] Logo logo, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                string imageName = "NoImage.png";

                if (Image != null)
                {
                    imageName = Image.FileName;


                    string ext = imageName.Substring(imageName.LastIndexOf('.'));

                    string[] goodExt = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (goodExt.Contains(ext.ToLower()))
                    {
                        Image.SaveAs(Server.MapPath("~/Content/Images/Logos/" + imageName));
                    }
                    else
                    {
                        imageName = "NoImage.png";
                    }
                }
                logo.imageSource = imageName;

                db.Logos.Add(logo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.typeID = new SelectList(db.Types, "typeID", "typeName", logo.typeID);
            return View(logo);
        }
        [Authorize(Roles = "Admin")]
        // GET: Logoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logo logo = db.Logos.Find(id);
            if (logo == null)
            {
                return HttpNotFound();
            }
            ViewBag.typeID = new SelectList(db.Types, "typeID", "typeName", logo.typeID);
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: Logoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "imageID,imageName,description,imageSource,typeName,typeID,alt,LogoOrder")] Logo logo, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    string imageName = Image.FileName;


                    string ext = imageName.Substring(imageName.LastIndexOf('.'));

                    string[] goodExt = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (goodExt.Contains(ext.ToLower()))
                    {
                        Image.SaveAs(Server.MapPath("~/Content/Images/Logos/" + imageName));
                       logo.imageSource = imageName;

                    }

                }
                db.Entry(logo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.typeID = new SelectList(db.Types, "typeID", "typeName", logo.typeID);
            return View(logo);
        }

        // GET: Logoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logo logo = db.Logos.Find(id);
            if (logo == null)
            {
                return HttpNotFound();
            }
            return View(logo);
        }

        // POST: Logoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Logo logo = db.Logos.Find(id);
            db.Logos.Remove(logo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
