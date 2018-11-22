using JetBrains.Annotations;
using System;

namespace NFive.Metrics.Shared
{
	[PublicAPI]
	public abstract class Metric : IMetric
	{
		public string Type { get; set; }

		public DateTime At { get; set; } = DateTime.UtcNow;
	}
}
