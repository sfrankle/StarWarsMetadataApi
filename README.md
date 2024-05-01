# Star Wars Metadata Api

A service for a metadata ingester to furnish information from the Star Wars movie universe, enriching our metadata system. The service proxies the SWAPI database, which offers a public REST API (https://swapi.dev/) accessible for querying this data.

## Endpoints
For more details, see Swagger Docs

## GET Types
- Proxies call to base `https://swapi.dev/api/`
- Returns: `IEnumerable<string>` representing the valid types available to query (i.e. people, films, etc)

### GET Object
- Proxies a single call to the SWAPI
- `GET /StarWars/{type}/{id}`

## GET Hydtrated Object
- Gets a single object, and then replaces the requested properties' url with the hydrated object
- `GET /StarWars/{type}/{id}?properties={propertyName}`
  - i.e. `GET /StarWars/people/1?properties=homeworld` gets the person with id=1, and then replaces the property "homeworld" default value (a link to that planet), with the planet object

## Rate Limiting
Fixed Window


## Error Handling
Currently the service catches all errors, logs them, and returns Internal Server Error.

See Possible Extensions / Error Handling for more.

## Possible Extensions
  
### Endpoints
- add an endpoint that allows for getting multiple of the same object
  - GET `/StarWars/{type}?[{id},{id},{id}]`

- update the nested endpoint to allow for an "all" properties to be included
  - this would convert all links into hydrated objects, except the url property

## Error Handling
- 