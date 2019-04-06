using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zookeeper.Mysql.DataAccess;

namespace Zookeeper.Mysql.Controllers
{
    [Route("api/tables")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly IContextProvider _contextProvider;

        public TablesController(IContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }

        [HttpGet("{dbId}")]
        public async Task<IActionResult> List([FromRoute]string dbId)
        {
            var dbContext = _contextProvider.GetContext(dbId);
            var list = await dbContext.Table.ToListAsync();
            return Ok(list);
        }
    }
}