using Elasticsearch.Net;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NFive.SDK.Core.Diagnostics;
using NFive.SDK.Server.Controllers;
using NFive.SDK.Server.Events;
using NFive.SDK.Server.Rpc;
using System;
using System.Threading.Tasks;

namespace NFive.Metrics.Server
{
	[PublicAPI]
	public class MetricsController : ConfigurableController<Configuration>
	{
		private readonly ElasticLowLevelClient client;

		public MetricsController(ILogger logger, IEventManager events, IRpcHandler rpc, Configuration configuration) : base(logger, events, rpc, configuration)
		{
			this.client = new ElasticLowLevelClient(new ConnectionConfiguration(new Uri($"http://{this.Configuration.Host}:{this.Configuration.Port}")));

			this.Events.On<object>("metric", async data => await this.Store(data));

			this.Rpc.Event("metric").On(new Action<IRpcEvent, object>(async (e, data) => await this.Store(data)));
		}

		private async Task Store(object data)
		{
			try
			{
				var json = JsonConvert.SerializeObject(data);

				var result = await this.client.IndexAsync<StringResponse>("metrics", "metric", Guid.NewGuid().ToString(), PostData.String(json));
				result.TryGetServerError(out var error);

				if (error != default(ServerError)) throw new Exception(error.ToString());

				this.Logger.Debug($"Stored metric: {json}");
			}
			catch (Exception ex)
			{
				this.Logger.Error(ex);
			}
		}
	}
}
