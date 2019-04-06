using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Zookeeper.Mysql.ZooKeeper
{
    public static class ZookeeperApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseZookeeper(this IApplicationBuilder app)
        {
            var service = app.ApplicationServices.GetRequiredService<IZookeeperHandler>();
            service.InitAsync().Wait();
            return app;
        }
    }
}