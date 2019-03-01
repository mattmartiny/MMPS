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
    public class GraphicsController : Controller
    {
      MartinyFolioEntities1 db = new MartinyFolioEntities1();

        // GET: Graphics
        public ActionResult Index(int? page)
        {

            int pageNumber = page ?? 1;
            var pageOfGraphics = db.Graphics.ToList().OrderBy(x => x.GraphicOrder).ToPagedList(pageNumber, 3);
          //if (Request.IsAjaxRequest())
          //  {
          //      return PartialView("_GraphicGrid", pageOfGraphics);
          //  }

           


           // var graphics = db.Graphics.Include(g => g.Type);
            return View(pageOfGraphics);
        }

        // GET: Graphics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Graphic graphic = db.Graphics.Find(id);
            if (graphic == null)
            {
                return HttpNotFound();
            }
            return View(graphic);
        }

        // GET: Graphics/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            
            ViewBag.typeID = new SelectList(db.Types, "typeID", "typeName");
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: Graphics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "imageID,imageName,description,imageSource,typeID,alt,GraphicOrder")] Graphic graphic, HttpPostedFileBase Image)
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
                        Image.SaveAs(Server.MapPath("~/Content/Images/Graphics/" + imageName));
                    }
                    else
                    {
                        imageName = "NoImage.png";
                    }
                }
                graphic.imageSource = imageName;
              
                db.Graphics.Add(graphic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           
            ViewBag.typeID = new SelectList(db.Types, "typeID", "typeName", graphic.typeID);
            return View(graphic);
        }
        [Authorize(Roles = "Admin")]
        // GET: Graphics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Graphic graphic = db.Graphics.Find(id);
            if (graphic == null)
            {
                return HttpNotFound();
            }
            ViewBag.imageID = new SelectList(db.Advertisements, "imageID", "imageName", graphic.imageID);
            ViewBag.typeID = new SelectList(db.Types, "typeID", "typeName", graphic.typeID);
            return View(graphic);
        }
        [Authorize(Roles = "Admin")]
        // POST: Graphics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "imageID,imageName,description,imageSource,typeName,typeID,alt,GraphicOrder")] Graphic graphic, HttpPostedFileBase Image)
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
                        Image.SaveAs(Server.MapPath("~/Content/Images/Graphics/" + imageName));
                        graphic.imageSource = imageName;

                    }

                }
           
           




                db.Entry(graphic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.imageID = new SelectList(db.Advertisements, "imageID", "imageName", graphic.imageID);
            ViewBag.typeID = new SelectList(db.Types, "typeID", "typeName", graphic.typeID);
            return View(graphic);
        }

        // GET: Graphics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Graphic graphic = db.Graphics.Find(id);
            if (graphic == null)
            {
                return HttpNotFound();
            }
            return View(graphic);
        }

        // POST: Graphics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Graphic graphic = db.Graphics.Find(id);
            db.Graphics.Remove(graphic);
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
