using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace mssql_service
{
    public interface IScriptService
    {
        void GenerateSchema(DbInfo dbInfo, TextWriter textWriter);
        void GenerateData(DbInfo dbInfo, TextWriter textWriter);
    }
}
