using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using org.apache.zookeeper;

namespace Zookeeper.Mysql.ZooKeeper
{
    public interface IZookeeperHandler
    {
        Task InitAsync();

        Task RefreshAsync();
    }

    public class ZookeeperHandler: IZookeeperHandler
    {
        private readonly org.apache.zookeeper.ZooKeeper _zooKeeper;
        private readonly IMemoryCache _cache;

        public ZookeeperHandler(org.apache.zookeeper.ZooKeeper zooKeeper, IMemoryCache cache)
        {
            _zooKeeper = zooKeeper;
            _cache = cache;
        }

        public async Task InitAsync()
        {
            await RefreshAsync();
        }

        public async Task RefreshAsync()
        {
            var connDic = new Dictionary<string, string>();

            var isExisted = await _zooKeeper.existsAsync("/connections");
            if (isExisted == null)
            {
                await _zooKeeper.createAsync("/connections", null, ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
            }

            var connResult = await _zooKeeper.getChildrenAsync("/connections", new ConnectionWatcher(this));

            foreach (var conn in connResult.Children)
            {
                var connData = await _zooKeeper.getDataAsync($"/connections/{conn}/value");
                var connStr = Encoding.UTF8.GetString(connData.Data);
                connDic[conn] = connStr;
            }

            _cache.Set("connections", connDic);
        }
    }
}