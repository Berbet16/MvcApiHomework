using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface ILogger
    {
        void Write(Log log);
        IEnumerable<Log> ReadAll();

        IEnumerable<Log> Search(string query);
    }
}
