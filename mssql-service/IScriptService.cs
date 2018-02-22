using System.IO;

namespace mssql_service
{
    public interface IScriptService
    {
        void GenerateSchema(DbInfo dbInfo, TextWriter textWriter);
        void GenerateData(DbInfo dbInfo, TextWriter textWriter);
    }
}
