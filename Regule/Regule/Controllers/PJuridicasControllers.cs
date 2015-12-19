using Regule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Regule.Controllers
{
    public class PJuridicasController : Controller
    {
        private ReguleDataContext db = new ReguleDataContext();

        // GET: Juridicas
        public ActionResult Index()
        {
            return View(db.Juridicas.ToList());
        }

        // GET: Juridicas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Juridica Juri = db.Juridicas.FirstOrDefault(x => x.Id == id);
            if (Juri == null)
            {
                return HttpNotFound();
            }
            return View(Juri);
        }

        // GET: Juridicas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Juridicas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pessoa,CNPJ,RazaoSocial")] Juridica Juri)
        {
            if (ModelState.IsValid)
            {
                db.Juridicas.InsertOnSubmit(Juri);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(Juri);
        }

        // GET: Juridicas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Juridica Juri = db.Juridicas.FirstOrDefault(x => x.Id == id);
            if (Juri == null)
            {
                return HttpNotFound();
            }
            return View(Juri);
        }

        // POST: Juridicas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Pessoa,CNPJ,RazaoSocial")] Juridica Juri)
        {
            if (ModelState.IsValid)
            {
                Juridica tp = db.Juridicas.FirstOrDefault(x => x.Id == Juri.Id);
                tp.CNPJ = Juri.CNPJ;
                tp.RazaoSocial = Juri.RazaoSocial;
                tp.Pessoa.Fornecedor = Juri.Pessoa.Fornecedor;
                tp.Pessoa.Nome = Juri.Pessoa.Nome;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(Juri);
        }

        // GET: Juridicas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Juridica Juri = db.Juridicas.FirstOrDefault(x => x.Id == id);
            if (Juri == null)
            {
                return HttpNotFound();
            }
            return View(Juri);
        }
        // POST: Juridicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Juridica Juri = db.Juridicas.FirstOrDefault(x => x.Id == id);
            db.Juridicas.DeleteOnSubmit(Juri);
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
