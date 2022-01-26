using Microsoft.Extensions.Primitives;

namespace app
{
    public class Startup
    {
        public interface IConfiguration
        {
            string this[string key] { get; set; }

            IConfigurationSection GetSection(string key);

            IEnumerable<IConfigurationSection> GetChildren();

            IChangeToken GetReloadToken();
        }
    }
}