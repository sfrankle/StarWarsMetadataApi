# Star Wars Metadata Api

A service for a metadata ingester to furnish information from the Star Wars movie universe, enriching our metadata system. The service proxies the SWAPI database, which offers a public REST API (https://swapi.dev/) accessible for querying this data.

## Endpoints

### GET Types
- `GET /StarWars`
- Returns the collection of available type / models from the SWAPI  (i.e. people, films, etc)

### GET Object
- `GET /StarWars/{type}/{id}`
- Returns a single json object retrieved from the SWAPI


### GET Hydrated Object
- `GET /StarWars/{type}/{id}?properties={propertyName}`
- Gets a single json object, and then replaces the requested properties' url with the hydrated object

  - i.e. `GET /StarWars/people/1?properties=homeworld` gets the person with id=1, and then replaces the default value of the property "homeworld" (a link to that planet), with the planet object.

## Rate Limiting
Fixed Window Rate Limiting configured to:
- Request Limit: 10.000
- Window: 1 day (86400 seconds)
- Partitioned by IP Address of caller


## Error Handling
Currently the service catches all errors, logs them, and returns Internal Server Error.

See Possible Extensions / Error Handling for more future implementations.

## Possible Extensions
  
### Endpoints
- add an endpoint that allows for getting multiple of the same object
  - `GET /StarWars/{type}?ids=1&ids=2`

- update the hydrated endpoint to allow for an "all" properties to be included
  - `GET /StarWars/{type}/{id}?properties=all`
  - this would convert all links into hydrated objects, except the url property

- add an endpoint that allows a collection of different types with ids
  - Body: 
    ```
      {
        "people": 1,
        "films": 2,
        "vehicles": 30
      }
    ```
  - returns a json object with the requested objects next to each other

### Error Handling
#### Update to know difference between `NotFound` and `BadRequest`

When request to SWAPI returns `HttpRequestException` with details of `NotFound`, check if the incoming requests type.

- If the type is valid (in the list of GET Types), return `NotFoundResult`
- If the type is invalid, return `BadRequestResult` with "Invalid Argument" in description

Considerations:
- We might not want to give the caller too much information. In this case, in testing enviroments we could enable detailed errors.

#### Update to use IExceptionHandler

Add a `GlobalExceptionHandler`. This could check the environment, check if the type is valid, etc.