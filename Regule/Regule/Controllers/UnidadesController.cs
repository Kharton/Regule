using Regule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Regule.Controllers
{
    public class UnidadesController : Controller
    {
        private ReguleDataContext db = new ReguleDataContext();

        // GET: Unidades
        public ActionResult Index()
        {
            return View(db.Unidades.ToList());
        }

        // GET: Unidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unidade Unid = db.Unidades.FirstOrDefault(x => x.Id == id);
            if (Unid == null)
            {
                return HttpNotFound();
            }
            return View(Unid);
        }

        // GET: Unidades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Unidades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sigla,Descricao")] Unidade Unid)
        {
            if (ModelState.IsValid)
            {
                db.Unidades.InsertOnSubmit(Unid);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(Unid);
        }

        // GET: Unidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unidade Unid = db.Unidades.FirstOrDefault(x => x.Id == id);
            if (Unid == null)
            {
                return HttpNotFound();
            }
            return View(Unid);
        }

        // POST: Unidades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Sigla,Descricao")] Unidade Unid)
        {
            if (ModelState.IsValid)
            {
                Unidade tp = db.Unidades.FirstOrDefault(x => x.Id == Unid.Id);
                tp.Descricao = Unid.Descricao;
                tp.Sigla = Unid.Sigla;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(Unid);
        }

        // GET: Unidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unidade Unid = db.Unidades.FirstOrDefault(x => x.Id == id);
            if (Unid == null)
            {
                return HttpNotFound();
            }
            return View(Unid);
        }
        // POST: Unidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Unidade Unid = db.Unidades.FirstOrDefault(x => x.Id == id);
            db.Unidades.DeleteOnSubmit(Unid);
            db.SubmitChanges();
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
