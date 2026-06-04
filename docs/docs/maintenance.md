# Maintenance

The service is part of the TERRA platform offered through an existing deployment, following the TERRA release and deployment procedures over a managed infrasrtucture along with the maintenance activities that are scheduled within the platform. The purpose of this section is not to detail the maintenance activities put in place by the TERRA team.

## Healthchecks

The service [OpenAPI Reference](openapi.md) describes healthcheck endpoints that can be used to track the status of the service.

The appropriate configuration file that controls the behavior of the healthcheck endpoints is described in the relevant [Configuration](configuration.md) section along with the response status codes for Healthy / Degrades / Unhealthy status and if the response will be verbose.

An example of a Verbose response that returns 200 OK for healthy state is:

```json
{
  "status": "Healthy",
  "duration": "00:00:00.0216780",
  "results": {
    "privateMemory": {
      "status": "Healthy",
      "description": null,
      "duration": "00:00:00.0015544",
      "tags": null,
      "exception": null,
      "data": {}
    },
    "processMemory": {
      "status": "Healthy",
      "description": "Allocated megabytes in memory: 408 mb",
      "duration": "00:00:00.0000382",
      "tags": null,
      "exception": null,
      "data": {}
    },
    "db": {
      "status": "Healthy",
      "description": null,
      "duration": "00:00:00.0211601",
      "tags": null,
      "exception": null,
      "data": {}
    }
  }
}
```

## Verions & Updates

The service follows a semantic versioning scheme and structures versions as MAJOR.MINOR.PATCH:

- MAJOR (X.0.0): Breaking changes that are incompatible with previous versions.
- MINOR (X.Y.0): New features added in a backward-compatible way.
- PATCH (X.Y.Z): Bug fixes and security patches that do not affect compatibility.

## Troubleshooting

Troubleshooting is primarily done through the logging mechanisms that are available and are described in the respective [logging](logging.md) section.
