using SelectPdf;
using NLog;
using MarkdownSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;


namespace Comet
{
    internal static class PdfConverter
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Convert3(string data)
        {
            Logger.Info("Generation de la page");
            // Génération du fichier PDF
            var converter = new HtmlToPdf();
            var doc = converter.ConvertHtmlString(data);

            // Enregistrement du fichier PDF
            string fileName = "test.pdf";
            doc.Save(fileName);
            doc.Close();
        }

        public static void Convert(string data)
        {
            // Conversion Markdown en HTML
            var converter = new Markdown();
            var htmlContent = converter.Transform(data);

            // Génération du fichier PDF
            var document = new PdfSharp.Pdf.PdfDocument();
            var pagePdf = document.AddPage();
            var gfx = XGraphics.FromPdfPage(pagePdf);
            var font = new XFont("Arial", 12);
            var xRect = new XRect(10, 10, pagePdf.Width - 20, pagePdf.Height - 20);
            gfx.DrawString(htmlContent, font, XBrushes.Black, xRect, XStringFormats.TopLeft);

            // Enregistrement du fichier PDF
            var fileName = "test.pdf";
            document.Save(fileName);
        }


        //public static void Convert2(string data)
        //{
        //    Logger.Info("Generation de la page");

        //    /// TODO a mettre sur un HTLM converter
        //    /// /// 
        //    // Interprétation du contenu HTML
        //    var htmlDocument = new HtmlDocument();
        //    htmlDocument.LoadHtml(data);

        //    // Extraction du texte du contenu HTML
        //    var plainText = htmlDocument.DocumentNode.InnerText;

        //    /// /// ///

        //    var document = new PdfDocument();
        //    var pagePdf = document.AddPage();
        //    var gfx = XGraphics.FromPdfPage(pagePdf);
        //    var font = new XFont("Arial", 12);
        //    var xRect = new XRect(10, 10, pagePdf.Width - 20, pagePdf.Height - 20);
        //    gfx.DrawString(plainText, font, XBrushes.Black, xRect, XStringFormats.TopLeft);

        //    // Enregistrement du fichier PDF
        //    var fileName = "test.pdf";
        //    document.Save(fileName);
        //}
    }
}
