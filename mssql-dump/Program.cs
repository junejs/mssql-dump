using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace mssql_tool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CommandLine.Parser.Default.ParseArguments<Options>(args)
    .WithParsed<Options>(opts => MessageBox.Show("ok"))
    .WithNotParsed<Options>((errs) => {
        foreach (var err in errs)
        {
            MessageBox.Show(err.ToString());
        }
    });


            Application.Run(new MainForm());
        }
    }

    class Options
    {
        [Option('s', Required = true, HelpText = "Database server")]
        public String Server { get; set; }

        [Option('u', Required = true, HelpText = "Database user ID")]
        public String UserId { get; set; }

        [Option('p', Required = true, HelpText = "Database password")]
        public String Password { get; set; }

        [Option('i', Required = true, HelpText = "Database server")]
        public String DbName { get; set; }

        [Option('d', Default = true, Required = false, HelpText = "Include data or not")]
        public bool IncludeData { get; set; }

        [Option("ssd", Default = false, Required = false, HelpText = "Seperate schema and data script or not")]
        public bool SeperateSchemaAndData { get; set; }
    }
}
