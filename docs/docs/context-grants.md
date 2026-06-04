# Context Grant Endpoints

Authorization is managed horizontally thrugh the [TERRA AAI](https://terra-horizon.github.io/terra-aai). Some permission granting capbilities are also exposed through the Gateway API endpoints that proxy the requests to the Terra AAI service.

These endpoints are under the `/api/principal/context-grants/...` route. and can be separated in the following coarse categories:

- Querying context grants assigned to logged in user
- Querying context grants assigned to some user or user group
- Assign / Unassign context grant to some user or user group

The context grants that can be assigned and are returned by the respective lookup options are the ones managed through the AAI service and are documented in the [Terra AAI Security Model Context Roles](https://terra-horizon.github.io/terra-aai/latest/security/#context-roles).

## Querying context grants

The `/api/principal/context-grants/query` endpoint allows querying based on predicates such as

- Roles: Limit the response to context grants assigned for the specific roles / context grants
- Subject Id: Which user to search for. Leaving it empty implies current user

More information can be found in the [OpenAPI Reference](openapi.md).

```bash
curl --location '<base url>/api/principal/context-grants/query' \
--header 'Authorization: Bearer eyJ...ZA' \
--header 'Content-Type: application/json' \
--data '{
    "roles": ["terra-browse"]
}'
```

This will provide an answer like the following:

```json
[
    {
        "principalId": "16...d1",
        "principalType": 1,
        "targetType": 0,
        "targetId": "07...19",
        "role": "terra-browse"
    },
    ...
]
```

## Querying context grants assigned to logged in user

There is one endpoint that allows retrieval of context grants for the logged in user:

### Retrieving all context grants for the logged in user

Using the `/api/principal/me/context-grants` endpoint we retrieve all context grants assigned to the logged in user

More information can be found in the [OpenAPI Reference](openapi.md).

```bash
curl --location '<base url>/api/principal/me/context-grants' \
--header 'Authorization: Bearer eyYA'
```

This will provide an answer like the following:

```json
[
    {
        "principalId": "16...d1",
        "principalType": 1,
        "targetType": 0,
        "targetId": "07...19",
        "role": "terra-browse"
    },
    ...
]
```

## Querying context grants assigned to some user or user group

There are 2 endpoints that allow retrieval of context grants for an arbitrary user of user group:

### Retrieving all context grants assigned to some user

Using the `/api/principal/user/<subject id>/context-grants` endpoint we retrieve all context grants assigned to some user

More information can be found in the [OpenAPI Reference](openapi.md).

```bash
curl --location '<base url>/api/principal/user/ec...2e/context-grants' \
--header 'Authorization: Bearer ey...IA'
```

This will provide an answer like the following:

```json
[
    {
        "principalId": "ec...2e",
        "principalType": 1,
        "targetType": 0,
        "targetId": "07...19",
        "role": "terra-browse"
    },
    ...
]
```

### Retrieving all context grants assigned to some user group

Using the `/api/principal/group/<group id>/context-grants` endpoint we retrieve all context grants assigned to some user group

More information can be found in the [OpenAPI Reference](openapi.md).

```bash
curl --location '<base url>/api/principal/group/f9...8f/context-grants' \
--header 'Authorization: Bearer ey...IA'
```

This will provide an answer like the following:

```json
[
    {
        "principalId": "f9...8f",
        "principalType": 1,
        "targetType": 0,
        "targetId": "07...19",
        "role": "terra-browse"
    },
    ...
]
```
