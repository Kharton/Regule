using Regule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Regule.Controllers
{
    public class FuncionariosController : Controller
    {

        private ReguleDataContext db = new ReguleDataContext();

        // GET: Funcionarios
        public ActionResult Index()
        {
            return View(db.Funcionarios.ToList());
        }

        // GET: Funcionarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario Func = db.Funcionarios.FirstOrDefault(x => x.Id == id);
            if (Func == null)
            {
                return HttpNotFound();
            }
            return View(Func);
        }

        // GET: Funcionarios/Create
        public ActionResult Create()
        {
            Funcionario Fun = new Funcionario();
            Fun.Fisica = new Fisica();
            Fun.Fisica.Pessoa = new Pessoa();
            Fun.Fisica.Pessoa.CliComunicars.Add(new CliComunicar());
            IEnumerable<Pessoa> pessoas = db.Pessoas.Where(x => x.Fisica != null && x.Fisica.Funcionario == null).ToList();
            ViewBag.Pessoas = pessoas.Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });
            return View(Fun);
        }

        // POST: Funcionarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Fisica,Id,Dirige,CarteiraTrb,Observacao,RG,Salario,Tecnico")] Funcionario Func)
        {
            if (ModelState.IsValid)
            {
                db.Funcionarios.InsertOnSubmit(Func);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(Func);
        }

        // GET: Funcionarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario Func = db.Funcionarios.FirstOrDefault(x => x.Id == id);
            if (Func == null)
            {
                return HttpNotFound();
            }
            if (Func.Fisica.Pessoa.CliComunicars.Count< 1)
            {
                Func.Fisica.Pessoa.CliComunicars.Add(new CliComunicar());
            }
            return View(Func);
        }

        // POST: Funcionarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fisica,Dirige,CarteiraTrb,Observacao,RG,Salario,Tecnico")] Funcionario Func)
        {
            if (ModelState.IsValid)
            {
                Funcionario tp = db.Funcionarios.FirstOrDefault(x => x.Id == Func.Id);
                List<CliComunicar> cf = db.CliComunicars.Where(x => x.IdPessoa == Func.Id).ToList();
                tp.Dirige = Func.Dirige;
                tp.CarteiraTrb = Func.CarteiraTrb;
                tp.Observacao = Func.Observacao;
                tp.RG = Func.RG;
                tp.Salario = Func.Salario;
                tp.Tecnico = Func.Tecnico;
                tp.Pagamentos = Func.Pagamentos;
                tp.Fisica.Pessoa.CliComunicars = Func.Fisica.Pessoa.CliComunicars;
                tp.Fisica.Pessoa.Nome = Func.Fisica.Pessoa.Nome;
                foreach (CliComunicar tel in db.CliComunicars.Where(x=>x.IdPessoa == Func.Id))
                {
                    CliComunicar tpc = Func.Fisica.Pessoa.CliComunicars.Where(X => X.Id == tel.Id).FirstOrDefault();
                    if(tpc==null)
                    {
                        CliComunicar t = db.CliComunicars.Where(x => x.Id == tel.Id).FirstOrDefault();
                        db.CliComunicars.DeleteOnSubmit(t);
                    }
                    else
                    {
                        tel.Tel = tpc.Tel;
                        tel.Principal = tpc.Principal;
                    }
                }
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(Func);
        }

        // GET: Funcionarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario Func = db.Funcionarios.FirstOrDefault(x => x.Id == id);
            if (Func == null)
            {
                return HttpNotFound();
            }
            return View(Func);
        }
        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Funcionario Func = db.Funcionarios.FirstOrDefault(x => x.Id == id);
            db.Pessoas.DeleteOnSubmit(Func.Fisica.Pessoa);
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
