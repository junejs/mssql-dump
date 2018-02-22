using CommandLine;
using mssql_service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mssql_tool_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithNotParsed(errs => Environment.Exit(2))
                .WithParsed(opts =>
                    {
                        var dbInfo = new DbInfo
                        {
                            Server = opts.Server,
                            UserId = opts.UserId,
                            Password = opts.Password,
                            DbName = opts.DbName
                        };

                        try
                        {
                            ScriptGenerateServiceWrapper.Generate(dbInfo, opts.IncludeData, opts.SeperateSchemaAndData, opts.OutputDir);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Environment.Exit(1);
                        }
                    });
        }
    }

    class Options
    {
        [Option(shortName: 's', longName: "server", Required = true, HelpText = "Database server")]
        public String Server { get; set; }

        [Option(shortName: 'u', longName: "userid", Required = true, HelpText = "Database user ID")]
        public String UserId { get; set; }

        [Option(shortName: 'p', longName: "password", Required = true, HelpText = "Database password")]
        public String Password { get; set; }

        [Option(shortName: 'i', longName: "dbname", Required = true, HelpText = "Database server")]
        public String DbName { get; set; }

        [Option(shortName: 'd', longName: "include-data", Default = true, Required = false, HelpText = "Include data")]
        public bool IncludeData { get; set; }

        [Option(shortName: 'l', longName: "seperate-schema-data", Default = false, Required = false, HelpText = "Seperate schema and data script")]
        public bool SeperateSchemaAndData { get; set; }

        [Option(shortName: 'o', longName: "output-dir", Default = "./script", HelpText = "Output directory for sql scripts")]
        public string OutputDir { get; set; }
    }
}
