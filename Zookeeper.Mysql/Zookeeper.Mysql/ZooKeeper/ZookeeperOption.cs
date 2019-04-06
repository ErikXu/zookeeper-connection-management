using System;
using Microsoft.Extensions.Configuration;

namespace Zookeeper.Mysql.ZooKeeper
{
    public class ZookeeperOption
    {
        public ZookeeperOption(IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var section = config.GetSection("zookeeper");
            section.Bind(this);
        }

        public string ConnectionString { get; set; }

        public int Timeout { get; set; }
    }
}