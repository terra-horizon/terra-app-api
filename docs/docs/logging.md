# Logging

All TERRA services are producing logs in a structured way, usilizing common log formats. The logs are aggregated to the [Logging Service](https://terra-horizon.github.io/terra-logging) where they can be queried and analyzed.

## Log format

The service utilized serilog for structured logging. The respective [Configuration](configuration.md) section describes where this is configured.

The log format utilized by is compact json providing structured presentation of the information presented. This is easily parsed and made available for further processing.

## Correlation Identifier

A key property in enabling troubleshooting in the micro-service TERRA architecture is the Correlation Identifier.

In order to serve a user request, a number of services invocations may be chained. It will be useful to be able to track the chain of the request across all involved services. To achive this, we utilize a shared Correlation Id that is generated early in the call stack and propagated across all subsequent invocations.

At the begining of the request stack, we check if there is a correlation id provided for the request in the request headers typically under a header named x-tracking-correlation. If not, we generate one for the request and any downstream calls. We also add it in the logging configuration so that all subsequent log messages include this correlation id.

At the time of invoking another service, we include the correlation id header, along with the correlation id value so that the next service in line will use the same identifier.

The respective [Configuration](configuration.md) section describes where this behavior is configured.

## Troubleshooting Logs

Troubleshooting logs are produced by the service throughout the execution of caller requests. The messages are separated by the log level:

- Trace
- Debug
- Information
- Warning
- Error
- Critical

Log entries may contain the following information (where available):

- Timestamp in UTC (ISO8601)
- Correlation Identifier
- Subject Id
- Client Id
- Message text
- Log Level
- ... additional properties

## Accounting Logs

The service generates accounting entries that utilize the same logging mechanism but are differentiated by troubleshooting logs through the "SourceContext" property which is set to "accounting".

These accounting log entries are harvested and processed by the [Accounting Service](https://terra-horizon.github.io/terra-accounting).
