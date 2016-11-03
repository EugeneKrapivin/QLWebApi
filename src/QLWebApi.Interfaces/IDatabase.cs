using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLWebApi.Interfaces
{
    public interface IDatabase
    {
        Task<string> GetDatabaseVersionAsync();
    }
}
