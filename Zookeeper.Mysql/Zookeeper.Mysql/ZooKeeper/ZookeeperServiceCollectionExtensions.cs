using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using org.apache.zookeeper;

namespace Zookeeper.Mysql.ZooKeeper
{
    public static class ZookeeperServiceCollectionExtensions
    {
        public static IServiceCollection AddZookeeper(this IServiceCollection services, IConfiguration config)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            services.AddOptions();

            var option = new ZookeeperOption(config);

            var zookeeper = new org.apache.zookeeper.ZooKeeper(option.ConnectionString, option.Timeout * 1000, new DefaultWatcher());

            services.Add(ServiceDescriptor.Singleton(zookeeper));
            return services;
        }
    }

    public class DefaultWatcher : Watcher
    {
        public override Task process(WatchedEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}