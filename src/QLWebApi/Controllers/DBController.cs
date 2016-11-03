using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLWebApi.Interfaces;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace QLWebApi.Controllers
{
    [Route("api/[controller]")]
    public class DBController : Controller
    {
        public IDB Db { get; set; }

        public DBController(IDB db)
        {
            Db = db;
        }

        // GET: api/qlgdbVer
        [Route("qlgdbVer")]
        public IActionResult GetQLGDBVer()
        {
            var ver = Db.GetQLGDBVer();

            return new ObjectResult(ver);
        }
    }
}
