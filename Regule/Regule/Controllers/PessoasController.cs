using Regule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Regule.Controllers
{
    public class PessoasController : Controller
    {

        private ReguleDataContext db = new ReguleDataContext();

        // GET: Pessoas
        public ActionResult Index()
        {
            return View(db.Pessoas.ToList());
        }

        // GET: Pessoas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa Pess = db.Pessoas.FirstOrDefault(x => x.Id == id);
            if (Pess == null)
            {
                return HttpNotFound();
            }
            return View(Pess);
        }

        // GET: Pessoas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Fornecedor")] Pessoa Pess)
        {
            if (ModelState.IsValid)
            {
                db.Pessoas.InsertOnSubmit(Pess);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(Pess);
        }

        // GET: Pessoas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa Pess = db.Pessoas.FirstOrDefault(x => x.Id == id);
            if (Pess == null)
            {
                return HttpNotFound();
            }
            return View(Pess);
        }

        // POST: Pessoas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Fornecedor")] Pessoa Pess)
        {
            if (ModelState.IsValid)
            {
                Pessoa tp = db.Pessoas.FirstOrDefault(x => x.Id == Pess.Id);
                tp.Nome= Pess.Nome;
                tp.Fornecedor = Pess.Fornecedor;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(Pess);
        }

        // GET: Pessoas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa Pess = db.Pessoas.FirstOrDefault(x => x.Id == id);
            if (Pess == null)
            {
                return HttpNotFound();
            }
            return View(Pess);
        }
        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pessoa Pess = db.Pessoas.FirstOrDefault(x => x.Id == id);
            db.Pessoas.DeleteOnSubmit(Pess);
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