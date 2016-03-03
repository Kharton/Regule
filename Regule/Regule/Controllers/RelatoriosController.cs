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
            ViewBag.Produtos = db.Produtos.Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });
            ViewBag.Fornecedores = db.Pessoas.Where(x => x.Fornecedor == true).Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });
            ViewBag.Clientes = db.Pessoas.Where(x => x.Fornecedor == false && x.Fisica.Funcionario == null ).Select(h => new SelectListItem { Text = h.Nome, Value = h.Id.ToString() });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult Index([Bind(Include = "Inicio,Fim,Cliente,Fornecedor,Produto")] Relatorio Func)
        {
            Produto p = db.Produtos.FirstOrDefault(x=>x.Id == Func.Produto);
            Pessoa pe = db.Pessoas.FirstOrDefault(x => x.Id == Func.Cliente || x.Id == Func.Fornecedor );
            string pessoa = Func.Fornecedor > 0 ? "Fornecedor" : "Cliente";
            string rel = "";
            IQueryable<VendaProduto> v = db.VendaProdutos.Where(x=>x.IdPro<0);
            IQueryable<CompraProduto> c = db.CompraProdutos.Where(x=>x.IdPro<0);
            if (Func.Fornecedor >= 0)
            {
                c = db.CompraProdutos.Where(x => x.Compra.Data >= Func.Inicio && x.Compra.Data <= Func.Fim);
                if (Func.Fornecedor > 0)
                {
                    c = c.Where(x => x.Compra.IdPessoa == pe.Id);
                }
                if (Func.Produto > 0)
                {
                    c = c.Where(x => x.IdPro == Func.Produto);
                }
                rel = "Compras";
            }
            else
            {
                v = db.VendaProdutos.Where(x => x.Venda.Data >= Func.Inicio && x.Venda.Data <= Func.Fim);
                if (Func.Cliente > 0)
                {
                    v = v.Where(x => x.Venda.IdPessoa == pe.Id);
                }
                if (Func.Produto > 0)
                {
                    v = v.Where(x => x.IdPro == Func.Produto);
                }
                rel = "Vendas";
            }

            Font FontTexto = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
            Font FontTextoN = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);
            Phrase phrase = null;

            phrase = new Phrase();
            phrase.Add(new Chunk("\nPeríodo: " + Func.Inicio.ToString("dd/MM/yyyy") + " até " + Func.Fim.ToString("dd/MM/yyyy") + "\n", FontTextoN));
            phrase.Add(new Chunk("Produto: " + (p != null ? p.Nome : "<<todos>>") + "\n", FontTextoN));
            phrase.Add(new Chunk(pessoa + ": " + (pe != null ? pe.Nome : "<<todos>>") + "\n\n", FontTextoN));

            ITextEvents evento = new ITextEvents(phrase,rel);

            byte[] b = Func.Fornecedor >= 0 ? CreatePDF(evento,null, c) : CreatePDF(evento, v);
            
            return new FileStreamResult(new MemoryStream(b), "application/pdf");
        }

        private byte[] CreatePDF(ITextEvents evento, IQueryable<VendaProduto> v = null,IQueryable<CompraProduto> c=null)
        {


            decimal? total = 0;
            Font FontTexto = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
            Font FontTextoN = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);
            Phrase phrase = null;
            PdfPCell cell = null;
            PdfPTable table = null;

            Document doc = new Document(PageSize.A4.Rotate(), 40f, 40f, 130f, 60f);
            using (MemoryStream dados = new MemoryStream())
            {
                DateTime hj = DateTime.Now;
                PdfWriter Pdf = PdfWriter.GetInstance(doc, dados);

                Pdf.PageEvent = evento;

                doc.Open();
            #region Dados

            table = new PdfPTable(6);
            table.TotalWidth = doc.PageSize.Width - doc.LeftMargin * 2;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 0.12f, 0.3f, 0.3f, 0.16f, 0.1f, 0.3f });

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
            int ind = 0;
                try {
                    if (evento.header == "Vendas")
                    {
                        foreach (VendaProduto item in v)
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
                            phrase.Add(new Chunk(item.Quantidade.ToString() + " - " + item.Unidade.Sigla, FontTexto));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            table.AddCell(cell);
                            //Valor/Un
                            phrase = new Phrase();
                            phrase.Add(new Chunk(((decimal)item.Preco).ToString("N"), FontTexto));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            table.AddCell(cell);
                            //Valor Total
                            phrase = new Phrase();
                            phrase.Add(new Chunk(((decimal)(item.Preco * item.Quantidade)).ToString("N"), FontTexto));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            table.AddCell(cell);
                            total += item.Preco * item.Quantidade;
                            ind++;
                        }
                    }
                    else
                    {
                        foreach (CompraProduto item in c)
                        {
                            //Data
                            phrase = new Phrase();
                            phrase.Add(new Chunk(Convert.ToDateTime(item.Compra.Data).ToString("dd/MM/yyyy"), FontTexto));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                            table.AddCell(cell);
                            //Cliente
                            phrase = new Phrase();
                            phrase.Add(new Chunk(item.Compra.Pessoa.Nome, FontTexto));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                            table.AddCell(cell);
                            //Produto
                            phrase = new Phrase();
                            phrase.Add(new Chunk(item.Produto.Nome, FontTexto));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                            table.AddCell(cell);
                            //Quantidade
                            phrase = new Phrase();
                            phrase.Add(new Chunk(item.Quantidade.ToString() + " - " + item.Unidade.Sigla, FontTexto));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            table.AddCell(cell);
                            //Valor/Un
                            phrase = new Phrase();
                            phrase.Add(new Chunk(((decimal)item.Preco).ToString("N"), FontTexto));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            table.AddCell(cell);
                            //Valor Total
                            phrase = new Phrase();
                            phrase.Add(new Chunk(((decimal)(item.Preco * item.Quantidade)).ToString("N"), FontTexto));
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                            table.AddCell(cell);
                            total += item.Preco * item.Quantidade;
                            ind++;
                        }

                    }
                }catch(Exception)
                {

                }
                doc.Add(table);
                #endregion

                #region Rodape
                
                table = new PdfPTable(3);
                table.TotalWidth = doc.PageSize.Width - doc.LeftMargin * 2;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.5f, 0.2f,0.3f});

                phrase = new Phrase();
                phrase.Add(new Chunk("Vlr Total: ", FontTextoN));
                cell = PhraseCell(phrase, Element.ALIGN_RIGHT);
                cell.Border = PdfPCell.NO_BORDER;
                table.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(((decimal)total).ToString("N"), FontTextoN));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);

                cell.BorderColorBottom = BaseColor.BLACK;
                cell.BorderWidthBottom = 1;
                cell.Border = Rectangle.BOTTOM_BORDER;
                table.AddCell(cell);
                
                table.AddCell(new PdfPCell() { Border = PdfPCell.NO_BORDER});

                table.WriteSelectedRows(0, -1, 40, 45, Pdf.DirectContent);
                
                #endregion
                for (int i = 0; i <= ind / 22; i++)
                    doc.NewPage();


            doc.Close();
            return dados.ToArray();
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
public class ITextEvents : PdfPageEventHelper
{

    // This is the contentbyte object of the writer
    PdfContentByte cb;

    // we will put the final number of pages in a template
    PdfTemplate headerTemplate;

    // this is the BaseFont we are going to use for the header / footer
    BaseFont bf = null;

    // This keeps track of the creation time

    #region Properties
    public Phrase pesq { get; set; }
    public string header{ get; set; }
    #endregion


    public override void OnOpenDocument(PdfWriter writer, Document document)
    {
        try
        {
            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb = writer.DirectContent;
            headerTemplate = cb.CreateTemplate(100, 100);
        }
        catch (DocumentException)
        {
            //handle exception here
        }
        catch (System.IO.IOException)
        {
            //handle exception here
        }
    }

    public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
    {
        Font FontTexto = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);
        Font FontTextoN = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK);
        base.OnEndPage(writer, document);
        
        Phrase p1Header = new Phrase("Relatório de "+header, FontTextoN);
        Phrase Data = new Phrase(new Chunk("impresso em: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), FontTexto));
        
        //Create PdfTable object
        PdfPTable pdfTab = new PdfPTable(2);

        //We will have to create separate cells to include image logo and 2 separate strings
        //Row 1
        PdfPCell pdfCell1 = new PdfPCell(p1Header);
        PdfPCell pdfCell2 = new PdfPCell(Data);
        String text = "Página " + writer.PageNumber + " de ";
        
        //Add paging to header
        {
            cb.BeginText();
            cb.SetFontAndSize(bf, 12);
            cb.SetTextMatrix(document.PageSize.GetRight(160), document.PageSize.GetTop(30));
            cb.ShowText(text);
            cb.EndText();
            float len = bf.GetWidthPoint(text, 12);
            //Adds "12" in Page 1 of 12
            cb.AddTemplate(headerTemplate, document.PageSize.GetRight(160) + len, document.PageSize.GetTop(30));
        }

        pdfCell1.VerticalAlignment = Element.ALIGN_BOTTOM;
        pdfCell2.VerticalAlignment = Element.ALIGN_BOTTOM;



        pdfCell1.BorderColorBottom = BaseColor.GRAY;
        pdfCell1.BorderWidthBottom = 3;
        pdfCell1.UseBorderPadding = true;
        pdfCell1.Border = Rectangle.BOTTOM_BORDER;

        pdfCell2.HorizontalAlignment = Element.ALIGN_RIGHT;
        pdfCell2.UseBorderPadding = true;
        pdfCell2.BorderColorBottom = BaseColor.GRAY;
        pdfCell2.BorderWidthBottom = 3;
        pdfCell2.Border = Rectangle.BOTTOM_BORDER;

        //add all three rows into PdfTable
        pdfTab.AddCell(pdfCell1);
        pdfTab.AddCell(pdfCell2);

        pdfTab.TotalWidth = document.PageSize.Width - 80f;
        pdfTab.WidthPercentage = 70;


        PdfPTable pdfTab2 = new PdfPTable(1);

        pdfCell1 = new PdfPCell(pesq);
        pdfCell1.BorderColorBottom = BaseColor.BLACK;
        pdfCell1.BorderWidthBottom = 2;
        pdfCell1.Border = Rectangle.BOTTOM_BORDER;
        pdfTab2.AddCell(pdfCell1);

        pdfTab2.TotalWidth = document.PageSize.Width - 80f;
        pdfTab2.WidthPercentage = 70;


        
        pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
        pdfTab2.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30 - pdfTab.TotalHeight, writer.DirectContent);

    }

    public override void OnCloseDocument(PdfWriter writer, Document document)
    {
        base.OnCloseDocument(writer, document);

        headerTemplate.BeginText();
        headerTemplate.SetFontAndSize(bf, 12);
        headerTemplate.SetTextMatrix(0, 0);
        headerTemplate.ShowText((writer.PageNumber - 1).ToString());
        headerTemplate.EndText();
    }

    public ITextEvents(Phrase p, string h)
    {
        pesq = p;
        header = h;
    }
}
