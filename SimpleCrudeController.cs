using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleCrudeOperation;
using log4net;

namespace SimpleCrudeOperation.Controllers
{
    public class SimpleCrudeController : Controller
    {
        public ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public CrudeOperationEntities db = new CrudeOperationEntities();

        // GET: SimpleCrude
        public ActionResult Index()
        {
            try
            {
                //Fetch data from crude table and display into Index view
                return View(db.Crudes.ToList());
            }

            catch
            {
                throw;
            }
        }

        // GET: SimpleCrude/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                logger.Info("SimpleCrude Detail Methods starts at " + DateTime.UtcNow);
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Crude crude = db.Crudes.Find(id);
                if (crude == null)
                {
                    return HttpNotFound();
                }
                return View(crude);
            }

            catch
            {
                throw;
            }
          
        }

        // GET: SimpleCrude/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SimpleCrude/Create
       // this method add new Record 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,EmailId,MobileNumber,Address")] Crude crude)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    logger.Info("SimpleCrude Create Methods Start at " + DateTime.UtcNow);
                    db.Crudes.Add(crude);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(crude);
            }

            catch
            {
                throw;
            }
        }

        // GET: SimpleCrude/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crude crude = db.Crudes.Find(id);
            if (crude == null)
            {
                return HttpNotFound();
            }
            return View(crude);
        }

        // POST: SimpleCrude/Edit/5
        //this method Edit Record and saved into database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,EmailId,MobileNumber,Address")] Crude crude)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    logger.Info("SimpleCrude Edit Methods Start at " + DateTime.UtcNow);
                    db.Entry(crude).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(crude);
            }

            catch
            {
                throw;
            }
        }

        // GET: SimpleCrude/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Crude crude = db.Crudes.Find(id);
                if (crude == null)
                {
                    return HttpNotFound();
                }
                return View(crude);
            }

            catch
            {
                throw;
            }
        }

        // POST: SimpleCrude/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                logger.Info("SimpleCrude Delete Methods Start at " + DateTime.UtcNow);
                Crude crude = db.Crudes.Find(id);
                db.Crudes.Remove(crude);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                throw;
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }

            catch
            {
                throw;
            }
        }
    }
}
