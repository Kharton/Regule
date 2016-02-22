using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Regule.Models;
using System.IO;

namespace Regule.Controllers
{
    public class RelatoriosController : Controller
    {
        private ReguleDataContext db = new ReguleDataContext();

        // GET: Relatorios
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult Index([Bind(Include = "Inicio,Fim,Pessoa,Produto")] Relatorio Func)
        {
            Produto p = db.Produtos.FirstOrDefault(x=>x.Id == Func.Produto);
            var v = db.VendaProdutos.Where(x=>x.Venda.Data >=Func.Inicio && x.Venda.Data <= Func.Fim);
            if (Func.Pessoa > 0)
                v = v.Where(x => x.Venda.IdPessoa == Func.Pessoa);
            if (Func.Produto > 0)
            {
                v = v.Where(x => x.IdPro == Func.Produto);
            }

            byte[] f = new byte[5];
            return new FileStreamResult(new MemoryStream(f), "application/pdf");
            //File(f,"application/pdf","Relatório.pdf");
        }
    }
}