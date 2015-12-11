using Regule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Regule.Controllers
{
    public class ProdutosController : Controller
    {
        private ReguleDataContext db = new ReguleDataContext();

        // GET: Produtos
        public ActionResult Index()
        {
            return View(db.Produtos.ToList());
        }

        // GET: Produtos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto Prod = db.Produtos.FirstOrDefault(x => x.Id == id);
            if (Prod == null)
            {
                return HttpNotFound();
            }
            return View(Prod);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome")] Produto Prod)
        {
            if (ModelState.IsValid)
            {
                db.Produtos.InsertOnSubmit(Prod);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(Prod);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto Prod= db.Produtos.FirstOrDefault(x=>x.Id == id);
            if (Prod== null)
            {
                return HttpNotFound();
            }
            return View(Prod);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome")] Produto Prod)
        {
            if (ModelState.IsValid)
            {
                db.Produtos.FirstOrDefault(x=> x.Id == Prod.Id).Nome = Prod.Nome;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(Prod);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto Prod= db.Produtos.FirstOrDefault(x=>x.Id == id);
            if (Prod == null)
            {
                return HttpNotFound();
            }
            return View(Prod);
        }
        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto Prod = db.Produtos.FirstOrDefault(x=> x.Id ==id);
            db.Produtos.DeleteOnSubmit(Prod);
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
