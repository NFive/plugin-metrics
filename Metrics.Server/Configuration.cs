using NFive.SDK.Core.Controllers;

namespace NFive.Metrics.Server
{
	public class Configuration : ControllerConfiguration
	{
		public string Host { get; set; } = "localhost";
		public int Port { get; set; } = 9200;
	}
}
