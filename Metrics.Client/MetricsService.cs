using JetBrains.Annotations;
using NFive.SDK.Client.Events;
using NFive.SDK.Client.Interface;
using NFive.SDK.Client.Rpc;
using NFive.SDK.Client.Services;
using NFive.SDK.Core.Diagnostics;
using NFive.SDK.Core.Models.Player;
using System;

namespace NFive.Metrics.Client
{
	[PublicAPI]
	public class MetricsService : Service
	{
		public MetricsService(ILogger logger, ITickManager ticks, IEventManager events, IRpcHandler rpc, OverlayManager overlay, User user) : base(logger, ticks, events, rpc, overlay, user)
		{
			this.Rpc.Event("metric").On(new Action<IRpcEvent, object>((e, data) => this.Rpc.Event("metric").Trigger(data)));
		}
	}
}
