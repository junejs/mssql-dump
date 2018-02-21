using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mssql_service
{
    public class DbInfo : IEquatable<DbInfo>
    {
        public String Server { get; set; }
        public String UserId { get; set; }
        public String Password { get; set; }
        public String DbName { get; set; }

        public bool Equals(DbInfo other)
        {
            return Server == other.Server
                && UserId == other.UserId
                && Password == other.Password
                && DbName == other.DbName;
        }
    }
}
