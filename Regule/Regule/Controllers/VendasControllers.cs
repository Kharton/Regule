using Regule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Regule.Controllers
{
    public class VendasController : Controller
    {
        private ReguleDataContext db = new ReguleDataContext();

        // GET: Vendas
        public ActionResult Index()
        {
            return View(db.Vendas.ToList());
        }

        // GET: Vendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda Vend = db.Vendas.FirstOrDefault(x => x.Id == id);
            if (Vend == null)
            {
                return HttpNotFound();
            }
            return View(Vend);
        }

        // GET: Vendas/Create
        public ActionResult Create()
        {
            IEnumerable<Pessoa> pessoas = db.Pessoas.ToList();
            ViewBag.Pessoas = pessoas.Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });

            Venda Vend = db.Vendas.FirstOrDefault();
            ViewBag.Pessoas = pessoas.Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });
            IEnumerable<Unidade> unidades = db.Unidades.ToList();
            ViewBag.Unidades = unidades.Select(h => new SelectListItem { Text = h.Sigla + " - " + h.Descricao, Value = h.Id.ToString() });
            IEnumerable<Produto> produtos = db.Produtos.ToList();
            ViewBag.Produtos = produtos.Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });
            return View();
        }

        // POST: Vendas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Data,Desconto,IdPessoa,VendaProdutos")] Venda Vend)
        {
            if (ModelState.IsValid)
            {
                db.Vendas.InsertOnSubmit(Vend);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(Vend);
        }

        // GET: Vendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda Vend = db.Vendas.FirstOrDefault(x => x.Id == id);
            if (Vend == null)
            {
                return HttpNotFound();
            }
            IEnumerable<Pessoa> pessoas = db.Pessoas.ToList();
            ViewBag.Pessoas = pessoas.Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });
            IEnumerable<Unidade> unidades = db.Unidades.ToList();
            ViewBag.Unidades = unidades.Select(h => new SelectListItem { Text = h.Sigla + " - " + h.Descricao, Value = h.Id.ToString() });
            IEnumerable<Produto> produtos = db.Produtos.ToList();
            ViewBag.Produtos = produtos.Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });

            return View(Vend);
        }

        // POST: Vendas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Data,Desconto,IdPessoa,VendaProdutos")] Venda Vend)
        {
            if (ModelState.IsValid)
            {
                Venda tp = db.Vendas.FirstOrDefault(x => x.Id == Vend.Id);
                tp.Data = Vend.Data;
                tp.Desconto = Vend.Desconto;
                tp.IdPessoa = Vend.IdPessoa;
                tp.VendaProdutos = Vend.VendaProdutos;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(Vend);
        }

        // GET: Vendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda Vend = db.Vendas.FirstOrDefault(x => x.Id == id);
            if (Vend == null)
            {
                return HttpNotFound();
            }
            return View(Vend);
        }
        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venda Vend = db.Vendas.FirstOrDefault(x => x.Id == id);
            db.Vendas.DeleteOnSubmit(Vend);
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
