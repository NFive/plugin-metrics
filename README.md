# NFive Metrics
Metric logging with Elasticsearch.

This plugin connects to an Elasticsearch server and allows other plugins to easily store metrics. Requires an Elasticsearch 6.x server.

## Installation
It is recommended to install this plugin into your plugin with nfpm.

```bash
nfpm install NFive/plugin-metrics
```

Once installed, add a reference to `NFive.Metrics.Shared.net.dll` to your plugin's project(s).

## Usage
This plugin can be used from both client and server plugins with NFive Events:

### Events
```csharp
this.Events.Raise("metric", new
{
    Type = "myevent",
    At = DateTime.UtcNow,
    Some = "data"
});
```

The event's the name is `metric` with one argument: an object representing the metric to store. The object can be of any type which can be JSON serialized but it is strongly recommended to inherit from `NFive.Metrics.Shared.Metric` or implement `NFive.Metrics.Shared.IMetric`.

## Configuration
The only plugin configuration options are how to connect to the Elasticsearch server:

```yml
host: localhost
port: 9200
```

### Kibana
If you wish to use Kibana to view the data, just select the `metrics` index.

An example Docker Elasticsearch and Kibana setup is included in [docker-compose.yml](docker-compose.yml).
