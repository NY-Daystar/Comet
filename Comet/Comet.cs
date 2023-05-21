using Comet.Utils;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using System;
using System.IO;

namespace Comet
{
    internal static class Comet
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static string LOGFILE => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Comet",
        "logs",
        "comet.log"
        );

        private static readonly string _application = "Comet";

        private static readonly string _version = "1.0.0";

        // TODO FAIRE UN README reprendre celui d'addams
        // TODO Push le projet
        // TODO ajouter des actions  gitflow  pour build le projet
        // TODO mail pour moi demain au boulot de recuperer le projet
        static void Main(string[] args)
        {
            // Get arguments from exe file
            CometOptions options = CometOptions.DefineOptions(args);

            Core.WriteLine("Welcome to ", ConsoleColor.Yellow, _application, ConsoleColor.White,
                " - Version : ", ConsoleColor.Yellow, _version, ConsoleColor.White);
            Console.WriteLine("---------------------");

            LogLevel level = options.Debug ? LogLevel.Debug : LogLevel.Info; // add in exe argument --debug
            SetupLogger(LOGFILE, level);
            Logger.Info("Launching Addams Application");

            // TODO ssi le projectID n'estst pas renseigner demandder le
            int projectId = 36458835;
            Api api = new Api();
            string result = api.GetWikiPages(projectId);
            Console.WriteLine(result);

            // TODO faire un service qui a une api pour avir des moddeles

            string slug = "home";
            string result2 = api.GetPageContent(projectId, slug);
            // TODO recuperer que le content
            Console.WriteLine(result2);

            PdfConverter.Convert(result2);
            
            ////
            /////
            ///
            // TODO pour le faire sur toutes les pages

            //foreach (var page in pages)
            //{
            //    var pageContent = GetPageContent(page.Slug); // Méthode pour récupérer le contenu de la page

            //    // Génération du fichier PDF
            //    var document = new PdfDocument();
            //    var pagePdf = document.AddPage();
            //    var gfx = XGraphics.FromPdfPage(pagePdf);
            //    var font = new XFont("Arial", 12);
            //    var xRect = new XRect(10, 10, pagePdf.Width - 20, pagePdf.Height - 20);
            //    gfx.DrawString(pageContent, font, XBrushes.Black, xRect, XStringFormats.TopLeft);

            //    // Enregistrement du fichier PDF
            //    var fileName = page.Slug + ".pdf";
            //    document.Save(fileName);
            //}

            ////
            ////
            ////

            Console.ReadLine();
        }

        /// <summary>
        /// Setup the logger with its path and it's minimum level
        /// </summary>
        /// <param name="filePath">path of the file</param>
        /// <param name="level">Minimum level to define</param>
        private static void SetupLogger(string filePath, LogLevel level)
        {
            LoggingConfiguration config = new LoggingConfiguration();
            Layout layout = "level:${uppercase:${level}} - date:${date} - caller: ${callsite-filename}:${callsite-linenumber} - ${message} ${exception:format=tostring}";

            // Targets where to log to: File and Console
            FileTarget logfile = new FileTarget("logfile")
            {
                FileName = filePath,
                ArchiveEvery = FileArchivePeriod.Minute,
                ArchiveNumbering = ArchiveNumberingMode.Rolling,
                MaxArchiveFiles = 5,
                Layout = layout

            };

            ConsoleTarget logconsole = new ConsoleTarget("logconsole")
            {
                Layout = layout
            };

            // Rules for mapping loggers to targets            
            config.AddRule(level, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, logfile);

            // Apply config           
            LogManager.Configuration = config;
        }
    }
}
