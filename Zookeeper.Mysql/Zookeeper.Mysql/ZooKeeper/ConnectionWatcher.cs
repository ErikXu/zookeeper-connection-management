using System.Threading.Tasks;
using org.apache.zookeeper;

namespace Zookeeper.Mysql.ZooKeeper
{
    public class ConnectionWatcher : Watcher
    {
        private readonly IZookeeperHandler _zookeeperService;

        public ConnectionWatcher(IZookeeperHandler zookeeperService)
        {
            _zookeeperService = zookeeperService;
        }

        public override async Task process(WatchedEvent @event)
        {
            var type = @event.get_Type();

            if (type != Event.EventType.None)
            {
                await _zookeeperService.RefreshAsync();
            }
        }
    }
}