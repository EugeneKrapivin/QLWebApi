using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLWebApi.DB;
using QLWebApi.Interfaces;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace QLWebApi.Controllers
{
    [Route("api/[controller]")]
    public class DBController : Controller
    {
        private readonly IDatabase _db;

        public DBController(IDatabase database)
        {
            _db = database;
        }

        // GET: api/qlgdbVer
        [HttpGet, Route("qlgdbVer")]
        public async Task<IActionResult> GetQLGDBVer()
        {
            try
            {
                var ver = await _db.GetDatabaseVersionAsync();
                
                return Ok(new {message = ver});
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
