﻿using Regule.Models;
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
            return View();
        }

        // POST: Funcionarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Fornecedor")] Funcionario Func)
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
            return View(Func);
        }

        // POST: Funcionarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Fornecedor")] Funcionario Func)
        {
            if (ModelState.IsValid)
            {
                Funcionario tp = db.Funcionarios.FirstOrDefault(x => x.Id == Func.Id);
                tp.Dirige = Func.Dirige;
                tp.CarteiraTrb = Func.CarteiraTrb;
                tp.Observacao = Func.Observacao;
                tp.RG = Func.RG;
                tp.Salario = Func.Salario;
                tp.Tecnico = Func.Tecnico;
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
            db.Funcionarios.DeleteOnSubmit(Func);
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