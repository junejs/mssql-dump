using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mssql_service
{
    public class ScriptGenerateServiceWrapper
    {
        public static void Generate(DbInfo dbInfo, bool includeData, bool seperateSchemaAndData, string outputDir)
        {
            Directory.CreateDirectory(outputDir);
            TextWriter twForSchema = null, twForData = null;
            try
            {
                if (seperateSchemaAndData)
                {
                    twForSchema = File.CreateText(Path.Combine(outputDir, $"{dbInfo.DbName}-schema.sql"));
                    twForData = File.CreateText(Path.Combine(outputDir, $"{dbInfo.DbName}-data.sql"));
                }
                else
                {
                    twForSchema = twForData = File.CreateText(Path.Combine(outputDir, $"{dbInfo.DbName}.sql"));
                }

                IScriptService scriptService = new SmoService();
                scriptService.GenerateSchema(dbInfo, twForSchema);

                if (includeData)
                {
                    scriptService.GenerateData(dbInfo, twForData);
                }
            }
            finally
            {
                twForSchema?.Dispose();
                twForData?.Dispose();
            }
        }
    }
}
