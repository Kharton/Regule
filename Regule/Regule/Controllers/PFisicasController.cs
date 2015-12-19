using Regule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Regule.Controllers
{
    public class PFisicasController : Controller
    {
        private ReguleDataContext db = new ReguleDataContext();

        // GET: Fisicas
        public ActionResult Index()
        {
            return View(db.Fisicas.ToList());
        }

        // GET: Fisicas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fisica Fisi = db.Fisicas.FirstOrDefault(x => x.Id == id);
            if (Fisi == null)
            {
                return HttpNotFound();
            }
            return View(Fisi);
        }

        // GET: Fisicas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fisicas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pessoa,CPF")] Fisica Fisi)
        {
            if (ModelState.IsValid)
            {
                db.Fisicas.InsertOnSubmit(Fisi);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(Fisi);
        }

        // GET: Fisicas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fisica Fisi = db.Fisicas.FirstOrDefault(x => x.Id == id);
            if (Fisi == null)
            {
                return HttpNotFound();
            }
            return View(Fisi);
        }

        // POST: Fisicas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Pessoa,CPF")] Fisica Fisi)
        {
            if (ModelState.IsValid)
            {
                Fisica tp = db.Fisicas.FirstOrDefault(x => x.Id == Fisi.Id);
                tp.CPF= Fisi.CPF;
                tp.Pessoa.Fornecedor = Fisi.Pessoa.Fornecedor;
                tp.Pessoa.Nome = Fisi.Pessoa.Nome;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(Fisi);
        }

        // GET: Fisicas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fisica Fisi = db.Fisicas.FirstOrDefault(x => x.Id == id);
            if (Fisi == null)
            {
                return HttpNotFound();
            }
            return View(Fisi);
        }
        // POST: Fisicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fisica Fisi = db.Fisicas.FirstOrDefault(x => x.Id == id);
            db.Fisicas.DeleteOnSubmit(Fisi);
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
