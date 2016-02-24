using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Regule.Models;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

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

            Document doc = new Document(PageSize.A4.Rotate(), 40f, 40f, 20f, 20f);
            Font nf = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
            using (MemoryStream dados = new MemoryStream()) {
                DateTime hj = DateTime.Now;
                PdfWriter Pdf = PdfWriter.GetInstance(doc, dados);

                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                BaseColor cor = null;

                doc.Open();


                //Header Table
                table = new PdfPTable(2);
                table.TotalWidth = doc.PageSize.Width - doc.LeftMargin*2;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.5f, 0.5f });

                phrase = new Phrase();
                phrase.Add(new Chunk("Relatório de Vendas", FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.RED)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT,PdfPCell.ALIGN_BOTTOM);
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Página x de y\nimpresso em:"+hj.ToString("dd/MM/yyyy HH:mm"), FontFactory.GetFont("Arial", 12 , Font.NORMAL, BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT,PdfPCell.ALIGN_BOTTOM);
                table.AddCell(cell);

                doc.Add(table);
                
                cor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"));
                DrawLine(Pdf, 25f, doc.Top - 40f, doc.PageSize.Width - 25f, doc.Top - 40f, cor);
                DrawLine(Pdf, 25f, doc.Top - 41f, doc.PageSize.Width - 25f, doc.Top - 41f, cor);



                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.9f, 0.1f });
                Pessoa pe = db.Pessoas.Where(x => x.Id == Func.Pessoa).FirstOrDefault();
                //Frase das pesquisas
                phrase = new Phrase();
                phrase.Add(new Chunk("Período: " + Func.Inicio.ToString("dd/MM/yyyy") + " até " + Func.Fim.ToString("dd/MM/yyyy")+"\n", FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)));
                phrase.Add(new Chunk("Produto: " + (p!=null?p.Nome:"<<todos>>"+"\n") + "\n", FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)));
                phrase.Add(new Chunk("Cliente: " + (pe!=null?pe.Nome:"<<todos>>") + "\n", FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)));

                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT,PdfPCell.ALIGN_BOTTOM);
                cell.BorderColor = BaseColor.BLACK;
                cell.BorderWidth = 3;
                cell.Border = 1;
                table.AddCell(cell);
                doc.Add(table);
                doc.Close();
                return new FileStreamResult( new MemoryStream(dados.ToArray()), "application/pdf");
            }
            //File(f,"application/pdf","Relatório.pdf");
        }

        private static PdfPCell PhraseCell(Phrase phrase, int align, int alignv = PdfPCell.ALIGN_TOP)
        {
            PdfPCell cell = new PdfPCell(phrase);
            cell.BorderColor = BaseColor.WHITE;
            cell.VerticalAlignment = alignv;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 2f;
            cell.PaddingTop = 0f;
            return cell;
        }

        private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, BaseColor color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(color);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }
    }
}