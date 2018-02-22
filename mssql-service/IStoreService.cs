using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mssql_service
{
    public interface IStoreService
    {
        void Save(string key, Object value);

        T Load<T>(string key);
    }
}
