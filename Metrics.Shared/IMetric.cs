using JetBrains.Annotations;
using System;

namespace NFive.Metrics.Shared
{
	[PublicAPI]
	public interface IMetric
	{
		[NotNull]
		string Type { get; set; }

		DateTime At { get; set; }
	}
}
