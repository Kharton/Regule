using Regule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Regule.Controllers
{
    public class ComprasController : Controller
    {
        private ReguleDataContext db = new ReguleDataContext();

        // GET: Compras
        public ActionResult Index()
        {
            return View(db.Compras.ToList());
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra Comp = db.Compras.FirstOrDefault(x => x.Id == id);
            if (Comp == null)
            {
                return HttpNotFound();
            }
            return View(Comp);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            IEnumerable<Pessoa> pessoas = db.Pessoas.Where(x => x.Fornecedor == true).ToList();

            ViewBag.Pessoas = pessoas.Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });
            IEnumerable<Unidade> unidades = db.Unidades.ToList();
            ViewBag.Unidades = unidades.Select(h => new SelectListItem { Text = h.Sigla + " - " + h.Descricao, Value = h.Id.ToString() });
            IEnumerable<Produto> produtos = db.Produtos.ToList();
            ViewBag.Produtos = produtos.Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });
            ViewBag.Pessoas = pessoas.Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString()} );
            return View();
        }

        // POST: Compras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Data,Desconto,IdPessoa")] Compra Comp)
        {
            if (ModelState.IsValid)
            {
                db.Compras.InsertOnSubmit(Comp);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(Comp);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra Comp = db.Compras.FirstOrDefault(x => x.Id == id);
            if (Comp == null)
            {
                return HttpNotFound();
            }
            IEnumerable<Pessoa> pessoas = db.Pessoas.Where(x => x.Fornecedor==true).ToList();
            ViewBag.Pessoas = pessoas.Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });
            IEnumerable<Unidade> unidades = db.Unidades.ToList();
            ViewBag.Unidades = unidades.Select(h => new SelectListItem { Text = h.Sigla + " - " + h.Descricao, Value = h.Id.ToString() });
            IEnumerable<Produto> produtos = db.Produtos.ToList();
            ViewBag.Produtos = produtos.Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });
            ViewBag.Pessoas = pessoas.Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });
            return View(Comp);
        }

        // POST: Compras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Desconto,Pessoa,Data")] Compra Comp)
        {
            if (ModelState.IsValid)
            {
                Compra tp = db.Compras.FirstOrDefault(x => x.Id == Comp.Id);
                tp.Data = Comp.Data;
                tp.Desconto = Comp.Desconto;
                tp.IdPessoa = Comp.IdPessoa;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(Comp);
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra Comp = db.Compras.FirstOrDefault(x => x.Id == id);
            if (Comp == null)
            {
                return HttpNotFound();
            }
            return View(Comp);
        }
        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compra Comp = db.Compras.FirstOrDefault(x => x.Id == id);
            db.Compras.DeleteOnSubmit(Comp);
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
