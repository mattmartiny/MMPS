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
    public class WebSitesController : Controller
    {
        private MartinyFolioEntities1 db = new MartinyFolioEntities1();

        // GET: WebSites
        public ActionResult Index(int? page)
        {
            
            
            int pageNumber = page ?? 1;
            var webSites = db.WebSites.Include(w => w.Type).Include(w => w.Type);
            var pageOfSites = db.WebSites.ToList().OrderBy(x => x.SiteOrder).ToPagedList(pageNumber, 3);

            return View(pageOfSites);
        }

        // GET: WebSites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebSite webSite = db.WebSites.Find(id);
            if (webSite == null)
            {
                return HttpNotFound();
            }
            return View(webSite);
        }

        // GET: WebSites/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.typeID = new SelectList(db.Types, "typeID", "typeName");
            ViewBag.typeID = new SelectList(db.Types, "typeID", "typeName");
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: WebSites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "imageID,SiteName,description,imageSource,siteUrl,typeName,typeID,alt,SiteOrder")] WebSite webSite, HttpPostedFileBase Image)
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
                        Image.SaveAs(Server.MapPath("~/Content/Images/Sites/" + imageName));
                    }
                    else
                    {
                        imageName = "NoImage.png";
                    }
                }
                webSite.imageSource = imageName;



                db.WebSites.Add(webSite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.typeID = new SelectList(db.Types, "typeID", "typeName", webSite.typeID);
            ViewBag.typeID = new SelectList(db.Types, "typeID", "typeName", webSite.typeID);
            return View(webSite);
        }
        [Authorize(Roles = "Admin")]
        // GET: WebSites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebSite webSite = db.WebSites.Find(id);
            if (webSite == null)
            {
                return HttpNotFound();
            }
            ViewBag.typeID = new SelectList(db.Types, "typeID", "typeName", webSite.typeID);
            ViewBag.typeID = new SelectList(db.Types, "typeID", "typeName", webSite.typeID);
            return View(webSite);
        }
        [Authorize(Roles = "Admin")]
        // POST: WebSites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "imageID,SiteName,description,imageSource,siteUrl,typeName,typeID,alt,SiteOrder")] WebSite webSite, HttpPostedFileBase Image)
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
                        Image.SaveAs(Server.MapPath("~/Content/Images/Sites/" + imageName));
                        webSite.imageSource = imageName;

                    }

                }

               
                db.Entry(webSite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.typeID = new SelectList(db.Types, "typeID", "typeName", webSite.typeID);
            ViewBag.typeID = new SelectList(db.Types, "typeID", "typeName", webSite.typeID);
            return View(webSite);
        }

        // GET: WebSites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebSite webSite = db.WebSites.Find(id);
            if (webSite == null)
            {
                return HttpNotFound();
            }
            return View(webSite);
        }

        // POST: WebSites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WebSite webSite = db.WebSites.Find(id);
            db.WebSites.Remove(webSite);
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
