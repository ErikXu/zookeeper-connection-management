using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using org.apache.zookeeper;
using Zookeeper.Mysql.Models;

namespace Zookeeper.Mysql.Controllers
{
    [Route("api/connectioins")]
    [ApiController]
    public class ConnectionsController : ControllerBase
    {
        private readonly org.apache.zookeeper.ZooKeeper _zooKeeper;

        public ConnectionsController(org.apache.zookeeper.ZooKeeper zooKeeper)
        {
            _zooKeeper = zooKeeper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = new List<Connection>();

            var connResult = await _zooKeeper.getChildrenAsync("/connections");
            foreach (var conn in connResult.Children)
            {
                var connData = await _zooKeeper.getDataAsync($"/connections/{conn}/value");
                var connStr = Encoding.UTF8.GetString(connData.Data);
                var item = new Connection
                {
                    Id = conn,
                    Value = connStr
                };
                list.Add(item);
            }

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Connection form)
        {
            await _zooKeeper.createAsync($"/connections/{form.Id}", null, ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
            await _zooKeeper.createAsync($"/connections/{form.Id}/value", Encoding.UTF8.GetBytes(form.Value), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            await _zooKeeper.deleteAsync($"/connections/{id}/value");
            await _zooKeeper.deleteAsync($"/connections/{id}");
            return Ok();
        }
    }
}
