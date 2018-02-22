using System;
using System.IO;

namespace mssql_service
{
    public class ScriptGenerateServiceWrapper
    {
        public static void Generate(DbInfo dbInfo, bool includeData, bool seperateSchemaAndData, string outputDir)
        {
            Directory.CreateDirectory(outputDir);
            string schemaFileName = null, dataFileName = null;
            TextWriter twForSchema = null, twForData = null;
            try
            {
                if (seperateSchemaAndData)
                {
                    schemaFileName = Path.Combine(outputDir, $"{dbInfo.DbName}-schema.sql");
                    twForSchema = File.CreateText(schemaFileName);
                    dataFileName = Path.Combine(outputDir, $"{dbInfo.DbName}-data.sql");
                    twForData = File.CreateText(dataFileName);
                }
                else
                {
                    schemaFileName = dataFileName = Path.Combine(outputDir, $"{dbInfo.DbName}.sql");
                    twForSchema = twForData = File.CreateText(schemaFileName);
                }

                IScriptService scriptService = new SmoService();

                scriptService.GenerateSchema(dbInfo, twForSchema);
                twForSchema.Flush();

                if (includeData)
                {
                    scriptService.GenerateData(dbInfo, twForData);
                    twForData.Flush();
                }
            }
            catch (Exception e)
            {
                // clean up 
                try
                {
                    twForSchema?.Close();
                    twForData?.Close();
                    File.Delete(schemaFileName);
                    File.Delete(dataFileName);
                }
                catch
                {
                }

                throw e;
            }
            finally
            {
                twForSchema?.Dispose();
                twForData?.Dispose();
            }
        }
    }
}
