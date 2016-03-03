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
            return View(db.Pessoas.Where(x => x.Fornecedor == false && x.Fisica.Funcionario == null).ToList());
        }

        // GET: Pessoas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa Fisi = db.Pessoas.FirstOrDefault(x => x.Id == id && x.Fornecedor == false);
            if (Fisi == null)
            {
                return HttpNotFound();
            }
            return View(Fisi);
        }

        // GET: Pessoas/Create
        public ActionResult Create()
        {
            Pessoa fisi = new Pessoa();
            fisi.CliComunicars.Add(new CliComunicar());
            return View(fisi);
        }

        // POST: Pessoas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Fisica,Juridica,Nome,Estado,Cidade,Endereco,Email,CliComunicars")] Pessoa Fisi)
        {
            if (ModelState.IsValid)
            {
                Fisi.Fornecedor = false;
                if (Fisi.Fisica.CPF == "00000000000" && Fisi.Juridica.CNPJ != "00000000000000")
                {
                    Fisi.Fisica = null;
                }
                else
                {
                    Fisi.Juridica = null;
                }
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
            Pessoa Fisi = db.Pessoas.FirstOrDefault(x => x.Id == id && x.Fornecedor == false);
            if (Fisi == null)
            {
                return HttpNotFound();
            }
            return View(Fisi);
        }

        // POST: Pessoas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fisica,Juridica,Nome,Estado,Cidade,Endereco,Email,CliComunicars")] Pessoa Fisi)
        {
            if (ModelState.IsValid)
            {
                Pessoa tp = db.Pessoas.FirstOrDefault(x => x.Id == Fisi.Id);
                tp.Nome = Fisi.Nome;
                tp.Estado = Fisi.Estado;
                tp.Cidade = Fisi.Cidade;
                tp.Endereco = Fisi.Endereco;
                tp.Email = Fisi.Email;

                if (Fisi.Fisica.CPF == "00000000000" && Fisi.Juridica.CNPJ != "00000000000000")
                {
                    if (db.Fisicas.FirstOrDefault(x => x.Id == Fisi.Id) != null)
                    {
                        db.Fisicas.DeleteOnSubmit(db.Fisicas.FirstOrDefault(x => x.Id == Fisi.Id));

                        db.Juridicas.InsertOnSubmit(new Juridica { Id = Fisi.Id, CNPJ = Fisi.Juridica.CNPJ });
                    }
                    else
                    {
                        tp.Juridica.CNPJ = Fisi.Juridica.CNPJ;
                    }
                }
                else
                {
                    if (db.Juridicas.FirstOrDefault(x => x.Id == Fisi.Id) != null)
                    {
                        db.Juridicas.DeleteOnSubmit(db.Juridicas.FirstOrDefault(x => x.Id == Fisi.Id));
                        db.Fisicas.InsertOnSubmit(new Fisica { Id = Fisi.Id, CPF = Fisi.Fisica.CPF });
                    }
                    else
                    {
                        tp.Fisica.CPF = Fisi.Fisica.CPF;
                    }
                }

                foreach (CliComunicar tel in db.CliComunicars.Where(x => x.IdPessoa == Fisi.Id))
                {
                    CliComunicar tpc = Fisi.CliComunicars.Where(X => X.Id == tel.Id).FirstOrDefault();
                    if (tpc == null)
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
            return View(Fisi);
        }

        // GET: Pessoas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa Fisi = db.Pessoas.FirstOrDefault(x => x.Id == id && x.Fornecedor == false);
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
            if(Fisi.Fisica != null)
                db.Fisicas.DeleteOnSubmit(Fisi.Fisica);
            else
                db.Juridicas.DeleteOnSubmit(Fisi.Juridica);
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
