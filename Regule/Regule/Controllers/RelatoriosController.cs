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
            Pessoa pe = db.Pessoas.FirstOrDefault(x => x.Id == Func.Pessoa);
            var v = db.VendaProdutos.Where(x=>x.Venda.Data >=Func.Inicio && x.Venda.Data <= Func.Fim);
            if (Func.Pessoa > 0)
                v = v.Where(x => x.Venda.IdPessoa == Func.Pessoa);
            if (Func.Produto > 0)
            {
                v = v.Where(x => x.IdPro == Func.Produto);
            }

            Document doc = new Document(PageSize.A4.Rotate(), 40f, 40f, 20f, 20f);
            using (MemoryStream dados = new MemoryStream()) {
                DateTime hj = DateTime.Now;
                PdfWriter Pdf = PdfWriter.GetInstance(doc, dados);

                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                Font FontTexto = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
                Font FontTextoN = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);

                doc.Open();


                #region Header
                table = new PdfPTable(2);
                table.TotalWidth = doc.PageSize.Width - doc.LeftMargin*2;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.5f, 0.5f });

                phrase = new Phrase();
                phrase.Add(new Chunk("Relatório de Vendas", FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT,PdfPCell.ALIGN_BOTTOM);

                cell.BorderColorBottom = BaseColor.GRAY;
                cell.BorderWidthBottom = 3;
                cell.Border = Rectangle.BOTTOM_BORDER;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk("Página x de y\nimpresso em: "+hj.ToString("dd/MM/yyyy HH:mm"), FontTextoN));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT,PdfPCell.ALIGN_BOTTOM);
                
                cell.BorderColorBottom = BaseColor.GRAY;
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.BorderWidthBottom = 3;
                table.AddCell(cell);
                doc.Add(table);
                #endregion
               

                #region Pesquisa
                table = new PdfPTable(1);
                table.TotalWidth = doc.PageSize.Width-doc.LeftMargin*2;
                table.LockedWidth = true;
                table.SetWidths(new float[] {1f});

                //Frase das pesquisas
                phrase = new Phrase();
                phrase.Add(new Chunk("\nPeríodo: " + Func.Inicio.ToString("dd/MM/yyyy") + " até " + Func.Fim.ToString("dd/MM/yyyy") + "\n", FontTextoN));
                phrase.Add(new Chunk("Produto: " + (p != null ? p.Nome : "<<todos>>") + "\n", FontTextoN));
                phrase.Add(new Chunk("Cliente: " + (pe != null ? pe.Nome : "<<todos>>") + "\n\n", FontTextoN));

                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_BOTTOM);
                cell.BorderColorBottom = BaseColor.BLACK;
                cell.BorderWidthBottom = 2;
                cell.Border = Rectangle.BOTTOM_BORDER;
                table.AddCell(cell);
                doc.Add(table);
                #endregion

                #region Dados

                table = new PdfPTable(6);
                table.TotalWidth = doc.PageSize.Width - doc.LeftMargin * 2;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.12f,0.3f, 0.3f,0.16f,0.1f,0.3f });

                //Cabeçalho dos dados
                phrase = new Phrase();
                phrase.Add(new Chunk("Data", FontTexto));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                table.AddCell(cell);
                phrase = new Phrase();
                phrase.Add(new Chunk("Cliente", FontTexto));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                table.AddCell(cell);
                phrase = new Phrase();
                phrase.Add(new Chunk("Produto", FontTexto));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                table.AddCell(cell);
                phrase = new Phrase();
                phrase.Add(new Chunk("Quantidade", FontTexto));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                table.AddCell(cell);
                phrase = new Phrase();
                phrase.Add(new Chunk("Valor/un", FontTexto));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                table.AddCell(cell);
                phrase = new Phrase();
                phrase.Add(new Chunk("Vlr. Total", FontTexto));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                table.AddCell(cell);

                foreach (var item in v)
                {
                    //Data
                    phrase = new Phrase();
                    phrase.Add(new Chunk(Convert.ToDateTime(item.Venda.Data).ToString("dd/MM/yyyy"), FontTexto));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    table.AddCell(cell);
                    //Cliente
                    phrase = new Phrase();
                    phrase.Add(new Chunk(item.Venda.Pessoa.Nome, FontTexto));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    table.AddCell(cell);
                    //Produto
                    phrase = new Phrase();
                    phrase.Add(new Chunk(item.Produto.Nome, FontTexto));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    table.AddCell(cell);
                    //Quantidade
                    phrase = new Phrase();
                    phrase.Add(new Chunk(item.Quantidade.ToString()+" - "+item.Unidade.Sigla, FontTexto));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    table.AddCell(cell);
                    //Valor/Un
                    phrase = new Phrase();
                    phrase.Add(new Chunk(item.Preco.ToString(), FontTexto));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    table.AddCell(cell);
                    //Valor Total
                    phrase = new Phrase();
                    phrase.Add(new Chunk((item.Preco*item.Quantidade).ToString(), FontTexto));
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                    table.AddCell(cell);
                }

                doc.Add(table);
                #endregion

                doc.Close();
                return new FileStreamResult( new MemoryStream(dados.ToArray()), "application/pdf");
            }
        }

        private static PdfPCell PhraseCell(Phrase phrase, int align, int alignv = PdfPCell.ALIGN_TOP)
        {
            PdfPCell cell = new PdfPCell(phrase);
            cell.VerticalAlignment = alignv;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 5f;
            cell.PaddingTop = 0f;
            return cell;
        }
    }
}