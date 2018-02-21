using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace mssql_service
{
    public class SmoService : IScriptService
    {
        public void GenerateData(DbInfo dbInfo, TextWriter textWriter)
        {
            var serverConn = new ServerConnection(dbInfo.Server, dbInfo.UserId, dbInfo.Password);
            Server srv = new Server(serverConn);
            Database db = srv.Databases[dbInfo.DbName];

            Scripter scrp = new Scripter(srv);
            scrp.Options.ScriptData = true;
            scrp.Options.ScriptSchema = false;

            var smoTables = db.Tables.Cast<Table>().Where(t => !t.IsSystemObject).ToList();
            var dependencyWalker = new DependencyWalker(db.Parent);
            var dependencyTree = dependencyWalker.DiscoverDependencies(smoTables.Cast<SqlSmoObject>().ToArray(), DependencyType.Parents);
            var dependencyCollection = dependencyWalker.WalkDependencies(dependencyTree);

            textWriter.WriteLine("-----------------------------------------");
            textWriter.WriteLine("-----------------data--------------------");
            textWriter.WriteLine("-----------------------------------------");
            Scripter scrForData = new Scripter(srv);
            scrForData.Options.ScriptData = true;
            scrForData.Options.ScriptSchema = false;
            foreach (DependencyCollectionNode dependencyCollectionNode in dependencyCollection)
            {
                var sc = scrForData.EnumScript(new Urn[] { dependencyCollectionNode.Urn });
                foreach (string st in sc)
                {
                    Debug.WriteLine(st);
                    textWriter.WriteLine(st);
                }
            }
        }

        public void GenerateSchema(DbInfo dbInfo, TextWriter textWriter)
        {
            var serverConn = new ServerConnection(dbInfo.Server, dbInfo.UserId, dbInfo.Password);
            Server srv = new Server(serverConn);
            Database db = srv.Databases[dbInfo.DbName];

            Scripter scrp = new Scripter(srv);
            scrp.Options.NoCollation = true;
            scrp.Options.Indexes = true;
            scrp.Options.ClusteredIndexes = true;
            scrp.Options.NonClusteredIndexes = true;
            scrp.Options.DriAll = true;
            scrp.Options.DriAllConstraints = true;

            scrp.Options.Triggers = true;
            scrp.Options.FullTextIndexes = true;

            scrp.Options.AllowSystemObjects = false;
            scrp.Options.WithDependencies = false;

            scrp.Options.TargetDatabaseEngineEdition = DatabaseEngineEdition.Standard;
            scrp.Options.TargetDatabaseEngineType = DatabaseEngineType.Standalone;

            scrp.Options.ExtendedProperties = true;

            scrp.Options.ScriptDrops = false;
            scrp.Options.ScriptSchema = true;
            scrp.Options.ScriptData = false;

            var smoTables = db.Tables.Cast<Table>().Where(t => !t.IsSystemObject).ToList();
            var dependencyWalker = new DependencyWalker(db.Parent);
            var dependencyTree = dependencyWalker.DiscoverDependencies(smoTables.Cast<SqlSmoObject>().ToArray(), DependencyType.Parents);
            var dependencyCollection = dependencyWalker.WalkDependencies(dependencyTree);

            textWriter.WriteLine("-----------------------------------------");
            textWriter.WriteLine("-----------------schema------------------");
            textWriter.WriteLine("-----------------------------------------");
            foreach (DependencyCollectionNode dependencyCollectionNode in dependencyCollection)
            {
                Debug.WriteLine(dependencyCollectionNode.Urn);
                System.Collections.Specialized.StringCollection sc = scrp.Script(new Urn[] { dependencyCollectionNode.Urn });
                foreach (string st in sc)
                {
                    Debug.WriteLine(st);
                    textWriter.WriteLine(st);
                }
            }

            var smoProcedures = db.StoredProcedures.Cast<StoredProcedure>().Where(t => !t.IsSystemObject).ToList();
            textWriter.WriteLine("-----------------------------------------");
            textWriter.WriteLine("-----------------procedure---------------");
            textWriter.WriteLine("-----------------------------------------");
            foreach (var procedureSmo in smoProcedures)
            {
                System.Collections.Specialized.StringCollection sc = scrp.Script(new Urn[] { procedureSmo.Urn });
                foreach (string st in sc)
                {
                    Debug.WriteLine(st);
                    textWriter.WriteLine(st);
                }
            }
        }
    }
}
