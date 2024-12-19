using Microsoft.Extensions.Hosting;

namespace webServer
{
    public class RouteEnumerator
    {
        public IConfiguration _configuration { get; set; }
        public string HostName { get => HostName; private set => HostName = _configuration.GetValue("WebSiteHostName", ""); }
        public static string Connect { get => Connect; private set => Connect = "/connect"; }
        public static string Home { get => Home; private set => Home = "/Home"; }
        public RouteEnumerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
