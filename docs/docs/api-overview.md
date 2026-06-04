# Api Overview

The complete API reference for the provided endpoints is available directly as an [Open API](openapi.md) reference. Additionally, common [Status & Error Codes](error-codes.md) information is detailed to cover the integration information needed.

In this section, we will present some primary endpoints, and groups of endpoints to assist integrating parties. Additionally, some common practices that are used for a number of endpoints are listed separately.

## FieldSet / Projection

The Gateway API offers the option to apply selective projection over the information that will be returned by the invoked endpoints. If the projection list is left empty, no information is returned. 

* The projection list consists of the response model property names that are requested
* If additional authorization is required for some of the requested model properties, these will be censored and not included in the response
* Requested properties may be qualified to include nested models
* Depending on the request, project list field sets may be either part of the request body, or query string parameters

## Query Structure

A number of available endpoints provide the ability for the requestor to execute complex queries on to control the kind of data that will be included in the response. These query endpoints have a similar structure across the different entities over which they can be applied:

* Predicate section: The available predicates by which the caller can filter the items included in the response. This section is dependant on the predicates supported for the specific entity
* Paging section: Section that controls the number of items that are requested to be returned as well the option to skip some items. 
    * Paging is only valid if ordering is also applied
    * If no paging is requested, all items matching the selected predicates are returned
* Ordering section: Section that controls the ordering applied to the items included in the response
    * If supported by the endpoint utilized, ordering on multiple properties is supported
    * Ascending ordering is defined using a + predicate on the ordered property name
    * Descending ordering is defined using a - predicate on the ordered property name
* Metadata section: Additional directives that may govern how the query should operate
    * Supported option includes requesting a total count of the items matching the predicates
* Projection section: List of fiels to be included for the items matching the predicates
    * As explained in the Fieldset / Projection section

## Current Principal

A dedicated endpoint is available to retrieve information on the logged in user and receive profile information as well as static permissions granted to the user

```console
curl --location 'https://terra-staging01.vhosts.cite.gr/terra-app-api/api/principal/me' \
--header 'Authorization: Bearer eyJ...CtA'
```

The response includes

```json
{
    "isAuthenticated": true,
    "principal": {
        "subject": "c...5",
        "name": "terra user-1",
        "username": "terra-user-1",
        "givenName": "terra",
        "familyName": "user-1",
        "email": "terra-user-1@terra-horizon.eu"
    },
    "token": {
        "issuer": "https://terra-staging01.vhosts.cite.gr/oauth/realms/staging",
        "tokenType": "Bearer",
        "authorizedParty": "d...i",
        "audience": [
            "d...i",
            "a...p",
            "a...t"
        ],
        "expiresAt": "2025-12-29T15:07:56Z",
        "issuedAt": "2025-12-29T15:02:56Z",
        "scope": [
            "openid profile d...i email offline_access"
        ]
    },
    "roles": [
        "G...v",
        "G...r",
        "G...s",
        "G...n"
    ],
    "permissions": [
        "Can...ery",
        "Can...ion",
        "Can...ion",
        "Cre...ion",
        "Loo...ups"
    ],
    "deferredPermissions": [
        "Bro...set",
        "Bro...ion",
        "Bro...ion",
        "Del...ion",
        "Edi...ion",
        "Add...oup",
        "Rem...oup"
    ]
}
```

## Context Grants

For permissions granted at the context level, there are dedicated endpoints that allows retrieval and nanagement of context based access grants. These can resolve grants assigned explicitly to users or inherited through group memberships. Additionally, they will allow searcing based on specific users, grant types, dataset and collection context.

To retrieve the list of context grants assigned to the current user, the following endpoint can be used

```console
curl --location 'https://terra-staging01.vhosts.cite.gr/terra-app-api/api/principal/me/context-grants' \
--header 'Authorization: Bearer eyJ...Nk-g'
```

and the response will be in the following form

```json
[
    {
        "principalId": "c...5",
        "principalType": 1,
        "targetType": 0,
        "targetId": "0...9",
        "role": "terra-sth-browse"
    },
    ...
]
```