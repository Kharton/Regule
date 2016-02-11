using Regule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Regule.Controllers
{
    public class ClientesController : Controller
    {
        private ReguleDataContext db = new ReguleDataContext();

        // GET: Pessoas
        public ActionResult Index()
        {
            return View(db.Pessoas.Where(x=>x.Fornecedor==false).ToList());
        }

        // GET: Pessoas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa Fisi = db.Pessoas.FirstOrDefault(x => x.Id == id);
            if (Fisi == null)
            {
                return HttpNotFound();
            }
            return View(Fisi);
        }

        // GET: Pessoas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Fisica,Juridica")] Pessoa Fisi)
        {
            if (ModelState.IsValid)
            {
                Fisi.Fornecedor = false;
                db.Pessoas.InsertOnSubmit(Fisi);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(Fisi);
        }

        // GET: Pessoas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa Fisi = db.Pessoas.FirstOrDefault(x => x.Id == id);
            if (Fisi == null)
            {
                return HttpNotFound();
            }
            return View(Fisi);
        }

        // POST: Pessoas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fisica,Juridica")] Pessoa Fisi)
        {
            if (ModelState.IsValid)
            {
                Pessoa tp = db.Pessoas.FirstOrDefault(x => x.Id == Fisi.Id);
                //tp.CPF= Fisi.CPF;
                //tp.Pessoa.Fornecedor = Fisi.Pessoa.Fornecedor;
                //tp.Pessoa.Nome = Fisi.Pessoa.Nome;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(Fisi);
        }

        // GET: Pessoas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa Fisi = db.Pessoas.FirstOrDefault(x => x.Id == id);
            if (Fisi == null)
            {
                return HttpNotFound();
            }
            return View(Fisi);
        }
        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pessoa Fisi = db.Pessoas.FirstOrDefault(x => x.Id == id);
            db.Pessoas.DeleteOnSubmit(Fisi);
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
