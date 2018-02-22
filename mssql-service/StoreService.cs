using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace mssql_service
{
    public class StoreService : IStoreService
    {
        private string _storeDir;
        public StoreService()
        {
            string currentDir = Directory.GetParent(Assembly.GetExecutingAssembly().FullName).FullName;
            _storeDir = Path.Combine(currentDir, "_store");
        }

        public void Save(string key, Object value)
        {
            Directory.CreateDirectory(_storeDir);
            string fileName = Path.Combine(_storeDir, key);
            using (TextWriter tw = File.CreateText(fileName))
            {
                JsonSerializer.Create().Serialize(tw, value);
            }
        }

        public T Load<T>(string key)
        {
            string fileName = Path.Combine(_storeDir, key);
            if (!File.Exists(fileName))
            {
                return default(T);
            }

            using (TextReader tr = File.OpenText(fileName))
            {
                Object value = JsonSerializer.Create().Deserialize(tr, typeof(T));
                return (T)value;
            }
        }
    }
}
