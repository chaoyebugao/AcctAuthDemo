using System;
using System.Collections.Generic;
using System.Text;

namespace AcctAuthDemo.Data.DbConfig
{
    public class AcctAuthDemoDbConfig : BaseDbConfig
    {
        public AcctAuthDemoDbConfig(string name, string connectionString)
        {
            this.Name = name;
            this.ConnectionString = connectionString;
        }
    }
}
