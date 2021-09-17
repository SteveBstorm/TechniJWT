using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOLibrary
{
    public class Command
    {
        internal readonly string _query;
        internal readonly Dictionary<string, object> _parameters;
        internal readonly bool _isStoredProcedure;

        public Command(string query, bool isStoredProcedure = false)
        {
            _query = query;
            _isStoredProcedure = isStoredProcedure;
            _parameters = new Dictionary<string, object>();
        }

        public void AddParameter(string key, object value)
        {
            _parameters.Add(key, value is null ? DBNull.Value : value);
        }
    }
}
