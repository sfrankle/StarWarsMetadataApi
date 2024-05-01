namespace MetadataApi.Tests;

public static class TestData
{
    public static string GetAvailableTypes()
    {
        return "{\"people\":\"https://swapi.dev/api/people/\",\"planets\":\"https://swapi.dev/api/planets/\",\"films\":\"https://swapi.dev/api/films/\",\"species\":\"https://swapi.dev/api/species/\",\"vehicles\":\"https://swapi.dev/api/vehicles/\",\"starships\":\"https://swapi.dev/api/starships/\"}";
    }
    public static IEnumerable<string> GetAvailableTypeList()
    {
        return new[] { "people", "planets" };
    }
    public static string GetPeopleOne()
    {
        return @"
        {
            ""name"": ""Luke Skywalker"",
            ""height"": ""172"",
            ""mass"": ""77"",
            ""hair_color"": ""blond"",
            ""skin_color"": ""fair"",
            ""eye_color"": ""blue"",
            ""birth_year"": ""19BBY"",
            ""gender"": ""male"",
            ""homeworld"": ""https://swapi.dev/api/planets/1/"",
            ""films"": [
                ""https://swapi.dev/api/films/1/"",
                ""https://swapi.dev/api/films/2/"",
                ""https://swapi.dev/api/films/3/"",
                ""https://swapi.dev/api/films/6/""
            ],
            ""species"": [],
            ""vehicles"": [
                ""https://swapi.dev/api/vehicles/14/"",
                ""https://swapi.dev/api/vehicles/30/""
            ],
            ""starships"": [
                ""https://swapi.dev/api/starships/12/"",
                ""https://swapi.dev/api/starships/22/""
            ],
            ""created"": ""2014-12-09T13:50:51.644Z"",
            ""edited"": ""2014-12-20T21:17:56.891Z"",
            ""url"": ""https://swapi.dev/api/people/1/""
        }";
    }

    public static string GetPlanetOne()
    {
        return @"
        {
            ""name"": ""Tatooine"",
            ""rotation_period"": ""23"",
            ""orbital_period"": ""304"",
            ""diameter"": ""10465"",
            ""climate"": ""arid"",
            ""gravity"": ""1 standard"",
            ""terrain"": ""desert"",
            ""surface_water"": ""1"",
            ""population"": ""200000"",
            ""residents"": [
            ""https://swapi.dev/api/people/1/"",
            ""https://swapi.dev/api/people/2/"",
            ""https://swapi.dev/api/people/4/"",
            ""https://swapi.dev/api/people/6/"",
            ""https://swapi.dev/api/people/7/"",
            ""https://swapi.dev/api/people/8/"",
            ""https://swapi.dev/api/people/9/"",
            ""https://swapi.dev/api/people/11/"",
            ""https://swapi.dev/api/people/43/"",
            ""https://swapi.dev/api/people/62/""
            ],
            ""films"": [
            ""https://swapi.dev/api/films/1/"",
            ""https://swapi.dev/api/films/3/"",
            ""https://swapi.dev/api/films/4/"",
            ""https://swapi.dev/api/films/5/"",
            ""https://swapi.dev/api/films/6/""
            ],
            ""created"": ""2014-12-09T13:50:49.641Z"",
            ""edited"": ""2014-12-20T20:58:18.411Z"",
            ""url"": ""https://swapi.dev/api/planets/1/""
        }";
    }

    public static string GetPeopleOneHydratedWithPlanetOne()
    {

        return @"
        {
            ""name"": ""Luke Skywalker"",
            ""height"": ""172"",
            ""mass"": ""77"",
            ""hair_color"": ""blond"",
            ""skin_color"": ""fair"",
            ""eye_color"": ""blue"",
            ""birth_year"": ""19BBY"",
            ""gender"": ""male"",
            ""homeworld"": {
                ""name"": ""Tatooine"",
                ""rotation_period"": ""23"",
                ""orbital_period"": ""304"",
                ""diameter"": ""10465"",
                ""climate"": ""arid"",
                ""gravity"": ""1 standard"",
                ""terrain"": ""desert"",
                ""surface_water"": ""1"",
                ""population"": ""200000"",
                ""residents"": [
                ""https://swapi.dev/api/people/1/"",
                ""https://swapi.dev/api/people/2/"",
                ""https://swapi.dev/api/people/4/"",
                ""https://swapi.dev/api/people/6/"",
                ""https://swapi.dev/api/people/7/"",
                ""https://swapi.dev/api/people/8/"",
                ""https://swapi.dev/api/people/9/"",
                ""https://swapi.dev/api/people/11/"",
                ""https://swapi.dev/api/people/43/"",
                ""https://swapi.dev/api/people/62/""
                ],
                ""films"": [
                ""https://swapi.dev/api/films/1/"",
                ""https://swapi.dev/api/films/3/"",
                ""https://swapi.dev/api/films/4/"",
                ""https://swapi.dev/api/films/5/"",
                ""https://swapi.dev/api/films/6/""
                ],
                ""created"": ""2014-12-09T13:50:49.641Z"",
                ""edited"": ""2014-12-20T20:58:18.411Z"",
                ""url"": ""https://swapi.dev/api/planets/1/""
            },
            ""films"": [
                ""https://swapi.dev/api/films/1/"",
                ""https://swapi.dev/api/films/2/"",
                ""https://swapi.dev/api/films/3/"",
                ""https://swapi.dev/api/films/6/""
            ],
            ""species"": [],
            ""vehicles"": [
                ""https://swapi.dev/api/vehicles/14/"",
                ""https://swapi.dev/api/vehicles/30/""
            ],
            ""starships"": [
                ""https://swapi.dev/api/starships/12/"",
                ""https://swapi.dev/api/starships/22/""
            ],
            ""created"": ""2014-12-09T13:50:51.644Z"",
            ""edited"": ""2014-12-20T21:17:56.891Z"",
            ""url"": ""https://swapi.dev/api/people/1/""
        }";
    }

    public static string GetPeopleOneHydratedWithVehicles()
    {
        return @"
        {
            ""name"": ""Luke Skywalker"",
            ""height"": ""172"",
            ""mass"": ""77"",
            ""hair_color"": ""blond"",
            ""skin_color"": ""fair"",
            ""eye_color"": ""blue"",
            ""birth_year"": ""19BBY"",
            ""gender"": ""male"",
            ""homeworld"": ""https://swapi.dev/api/planets/1/"",
            ""films"": [
                ""https://swapi.dev/api/films/1/"",
                ""https://swapi.dev/api/films/2/"",
                ""https://swapi.dev/api/films/3/"",
                ""https://swapi.dev/api/films/6/""
            ],
            ""species"": [],
            ""vehicles"": [
                {
                    ""name"": ""Snowspeeder"",
                    ""model"": ""t-47 airspeeder"",
                    ""manufacturer"": ""Incom corporation"",
                    ""cost_in_credits"": ""unknown"",
                    ""length"": ""4.5"",
                    ""max_atmosphering_speed"": ""650"",
                    ""crew"": ""2"",
                    ""passengers"": ""0"",
                    ""cargo_capacity"": ""10"",
                    ""consumables"": ""none"",
                    ""vehicle_class"": ""airspeeder"",
                    ""pilots"": [
                        ""https://swapi.dev/api/people/1/"",
                        ""https://swapi.dev/api/people/18/""
                    ],
                    ""films"": [
                        ""https://swapi.dev/api/films/2/""
                    ],
                    ""created"": ""2014-12-15T12:22:12Z"",
                    ""edited"": ""2014-12-20T21:30:21.672000Z"",
                    ""url"": ""https://swapi.dev/api/vehicles/14/""
                },
                {
                    ""name"": ""Imperial Speeder Bike"",
                    ""model"": ""74-Z speeder bike"",
                    ""manufacturer"": ""Aratech Repulsor Company"",
                    ""cost_in_credits"": ""8000"",
                    ""length"": ""3"",
                    ""max_atmosphering_speed"": ""360"",
                    ""crew"": ""1"",
                    ""passengers"": ""1"",
                    ""cargo_capacity"": ""4"",
                    ""consumables"": ""1 day"",
                    ""vehicle_class"": ""speeder"",
                    ""pilots"": [
                        ""https://swapi.dev/api/people/1/"",
                        ""https://swapi.dev/api/people/5/""
                    ],
                    ""films"": [
                        ""https://swapi.dev/api/films/3/""
                    ],
                    ""created"": ""2014-12-18T11:20:04.625000Z"",
                    ""edited"": ""2014-12-20T21:30:21.693000Z"",
                    ""url"": ""https://swapi.dev/api/vehicles/30/""
                }
            ],
            ""starships"": [
                ""https://swapi.dev/api/starships/12/"",
                ""https://swapi.dev/api/starships/22/""
            ],
            ""created"": ""2014-12-09T13:50:51.644Z"",
            ""edited"": ""2014-12-20T21:17:56.891Z"",
            ""url"": ""https://swapi.dev/api/people/1/""
        }";
    }

    public static string GetVehicles14()
    {
        return @"
        {
            ""name"": ""Snowspeeder"",
            ""model"": ""t-47 airspeeder"",
            ""manufacturer"": ""Incom corporation"",
            ""cost_in_credits"": ""unknown"",
            ""length"": ""4.5"",
            ""max_atmosphering_speed"": ""650"",
            ""crew"": ""2"",
            ""passengers"": ""0"",
            ""cargo_capacity"": ""10"",
            ""consumables"": ""none"",
            ""vehicle_class"": ""airspeeder"",
            ""pilots"": [
                ""https://swapi.dev/api/people/1/"",
                ""https://swapi.dev/api/people/18/""
            ],
            ""films"": [
                ""https://swapi.dev/api/films/2/""
            ],
            ""created"": ""2014-12-15T12:22:12Z"",
            ""edited"": ""2014-12-20T21:30:21.672000Z"",
            ""url"": ""https://swapi.dev/api/vehicles/14/""
        }";
    }

    public static string GetVehicles30()
    {
        return @"
        {
            ""name"": ""Imperial Speeder Bike"",
            ""model"": ""74-Z speeder bike"",
            ""manufacturer"": ""Aratech Repulsor Company"",
            ""cost_in_credits"": ""8000"",
            ""length"": ""3"",
            ""max_atmosphering_speed"": ""360"",
            ""crew"": ""1"",
            ""passengers"": ""1"",
            ""cargo_capacity"": ""4"",
            ""consumables"": ""1 day"",
            ""vehicle_class"": ""speeder"",
            ""pilots"": [
                ""https://swapi.dev/api/people/1/"",
                ""https://swapi.dev/api/people/5/""
            ],
            ""films"": [
                ""https://swapi.dev/api/films/3/""
            ],
            ""created"": ""2014-12-18T11:20:04.625000Z"",
            ""edited"": ""2014-12-20T21:30:21.693000Z"",
            ""url"": ""https://swapi.dev/api/vehicles/30/""
        }";
    }
}
