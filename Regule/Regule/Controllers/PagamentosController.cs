using Regule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Regule.Controllers
{
    public class PagamentosController : Controller
    {
        private ReguleDataContext db = new ReguleDataContext();

        // GET: Pagamentos
        public ActionResult Index()
        {
            ViewBag.Funcionarios = db.Pessoas.Where(x => x.Fisica != null && x.Fisica.Funcionario != null).Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });
            Funcionario fun = new Funcionario();
            fun.Pagamentos.Add(new Pagamento());
            return View(fun);
        }

        // Post: Pagamentos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Pagamentos,Id")] Funcionario Fisi)
        {
            
            ViewBag.Funcionarios = db.Pessoas.Where(x => x.Fisica != null && x.Fisica.Funcionario != null).Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });
            Funcionario fun = db.Funcionarios.Where(x => x.Id == Fisi.Id).FirstOrDefault();
            if(Fisi.Id != 0 && Fisi.Pagamentos[0]!= null && Fisi.Pagamentos[0].Referencia!=null)
            {
                int index = 0;
                while (fun.Pagamentos.Count > index)
                {
                    if (!(fun.Pagamentos[index].Referencia!=null && fun.Pagamentos[index].Referencia.Value.Year == Fisi.Pagamentos[0].Referencia.Value.Year && fun.Pagamentos[index].Referencia.Value.Month == Fisi.Pagamentos[0].Referencia.Value.Month))
                    {
                        fun.Pagamentos.RemoveAt(index);
                    }
                    else
                        index++;
                }
            }
            if (fun == null)
                fun = new Funcionario();
            if(fun.Pagamentos.Count<1)
                fun.Pagamentos.Add(new Pagamento());
            return View(fun);
        }

        // GET: Pagamentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagamento Fisi = db.Pagamentos.FirstOrDefault(x => x.Id == id);
            if (Fisi == null)
            {
                return HttpNotFound();
            }
            return View(Fisi);
        }

        // GET: Pagamentos/Create
        public ActionResult Create()
        {
            ViewBag.Funcionarios = db.Pessoas.Where(x => x.Fisica != null && x.Fisica.Funcionario != null).Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });
            return View();
        }

        // POST: Pagamentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFun,Valor,Data,Referencia")] Pagamento Fisi)
        {
            if (ModelState.IsValid)
            {
                //Fisi.Fornecedor = false;
                db.Pagamentos.InsertOnSubmit(Fisi);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(Fisi);
        }

        // GET: Pagamentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagamento Fisi = db.Pagamentos.FirstOrDefault(x => x.Id == id);
            if (Fisi == null)
            {
                return HttpNotFound();
            }
            return View(Fisi);
        }

        // POST: Pagamentos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Valor,Data,Referencia")] Pagamento Fisi)
        {
            if (ModelState.IsValid)
            {
                Pagamento tp = db.Pagamentos.FirstOrDefault(x => x.Id == Fisi.Id);
                tp.Data= Fisi.Data;
                tp.Referencia = Fisi.Referencia;
                tp.Valor = Fisi.Valor;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(Fisi);
        }

        // GET: Pagamentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagamento Fisi = db.Pagamentos.FirstOrDefault(x => x.Id == id);
            if (Fisi == null)
            {
                return HttpNotFound();
            }
            return View(Fisi);
        }
        // POST: Pagamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pagamento Fisi = db.Pagamentos.FirstOrDefault(x => x.Id == id);
            db.Pagamentos.DeleteOnSubmit(Fisi);
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
