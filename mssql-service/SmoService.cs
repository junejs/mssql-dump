using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
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
            if (db == null)
            {
                throw new Exception($"数据库 {dbInfo.DbName} 不存在.");
            }

            Scripter scripter = new Scripter(srv);
            scripter.Options.ScriptData = true;
            scripter.Options.ScriptSchema = false;

            textWriter.WriteLine("-----------------------------------------");
            textWriter.WriteLine("-----------------data--------------------");
            textWriter.WriteLine("-----------------------------------------");
            foreach (Urn table in getTableUrnWithDependence(db))
            {
                Console.WriteLine();
                var sc = scripter.EnumScript(new Urn[] { table });
                foreach (string st in sc)
                {
                    Console.WriteLine(st);
                    textWriter.WriteLine(st);
                }
                if (sc.Count() > 0)
                {
                    textWriter.WriteLine("GO");
                }
            }
        }

        public void GenerateSchema(DbInfo dbInfo, TextWriter textWriter)
        {
            var serverConn = new ServerConnection(dbInfo.Server, dbInfo.UserId, dbInfo.Password);
            Server srv = new Server(serverConn);
            Database db = srv.Databases[dbInfo.DbName];
            if (db == null)
            {
                throw new Exception($"Database {dbInfo.DbName} not exists.");
            }

            Scripter scripter = new Scripter(srv);

            scripter.Options.NoCollation = true;
            scripter.Options.Indexes = true;
            scripter.Options.ClusteredIndexes = true;
            scripter.Options.NonClusteredIndexes = true;
            scripter.Options.DriAll = true;
            scripter.Options.DriAllConstraints = true;

            scripter.Options.Triggers = true;
            scripter.Options.FullTextIndexes = true;

            scripter.Options.AllowSystemObjects = false;
            scripter.Options.WithDependencies = false;

            scripter.Options.TargetDatabaseEngineEdition = DatabaseEngineEdition.Standard;
            scripter.Options.TargetDatabaseEngineType = DatabaseEngineType.Standalone;

            scripter.Options.ExtendedProperties = true;

            scripter.Options.ScriptDrops = false;
            scripter.Options.ScriptSchema = true;
            scripter.Options.ScriptData = false;

            textWriter.WriteLine("-----------------------------------------");
            textWriter.WriteLine("-----------------schema------------------");
            textWriter.WriteLine("-----------------------------------------");
            foreach (Urn table in getTableUrnWithDependence(db))
            {
                System.Collections.Specialized.StringCollection sc = scripter.Script(new Urn[] { table });
                foreach (string st in sc)
                {
                    Console.WriteLine(st);
                    textWriter.WriteLine(st);
                    textWriter.WriteLine("GO");
                }
            }

            var smoProcedures = db.StoredProcedures.Cast<StoredProcedure>().Where(t => !t.IsSystemObject).ToList();
            textWriter.WriteLine("-----------------------------------------");
            textWriter.WriteLine("-----------------procedure---------------");
            textWriter.WriteLine("-----------------------------------------");
            foreach (var procedureSmo in smoProcedures)
            {
                System.Collections.Specialized.StringCollection sc = scripter.Script(new Urn[] { procedureSmo.Urn });
                foreach (string st in sc)
                {
                    Console.WriteLine(st);
                    textWriter.WriteLine(st);
                    textWriter.WriteLine("GO");
                }
            }
        }

        private List<Urn> getTableUrnWithDependence(Database db)
        {
            var smoTables = db.Tables.Cast<Table>().Where(t => !t.IsSystemObject).ToList();
            var dependencyWalker = new DependencyWalker(db.Parent);
            var dependencyTree = dependencyWalker.DiscoverDependencies(smoTables.Cast<SqlSmoObject>().ToArray(), DependencyType.Parents);
            var dependencyCollection = dependencyWalker.WalkDependencies(dependencyTree);

            return dependencyCollection.Select(d => d.Urn).ToList();
        }
    }
}
