using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Zookeeper.Mysql.DataAccess
{
    public interface IContextProvider
    {
        TestContext GetContext(string id);
    }

    public class ContextProvider : IContextProvider
    {
        private readonly IMemoryCache _cache;

        public ContextProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public TestContext GetContext(string id)
        {
            var dic = _cache.Get<Dictionary<string, string>>("connections");
            var connectionStr = dic[id];

            var optionsBuilder = new DbContextOptionsBuilder<TestContext>();
            optionsBuilder.UseMySQL(connectionStr);
            return new TestContext(optionsBuilder.Options);
        }
    }
}