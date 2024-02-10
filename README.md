# APIAggregation Documentation

Welcome to the APIAggregation Documentation that have been created from Kotis,A.
API was created in net80. You must have installed the framework to run it. Support only Development environment.
Bellow you can find information about the Services and the enums that needs you for the requests. Allthough when the application is running with debug you can open Swagger Documentation and Redoc for more information.

1. Swagger 
 ```
/swagger/index.html
```
2. Redoc
 ```
/api-docs/index.html
```

# Controllers
In the incoming sections you will find the avaliable Endpoints of each controller. If you want more detailed informations you can consult the Swagger and Redoc as mentioned above.

## JokeController

${\color{green}[POST]}$ /Joke/GetJoke

## WeatherController

${\color{blue}[GET]}$ /WeatherForecast/CityWeather

${\color{blue}[GET]}$ /WeatherForecast/LatitudeLongitudeWeather

# Services

## _WeatherServices_
Weather Controller supports two different services to return you the appropriate information.
The described number of each service:

1. Retrieves information from [Open Weather](https://openweathermap.org/api/one-call-3) 
2. Retrieves information from [Visual Crossing](https://www.visualcrossing.com/resources/documentation/weather-api/timeline-weather-api/) 

# Enum
***
<details>
    <summary>Languages</summary>

```

            /// <summary>
            /// English
            /// </summary>
            EN = 0,

            /// <summary>
            /// German
            /// </summary>
            DE = 1,

            /// <summary>
            /// Czech
            /// </summary>
            CS = 2,

            /// <summary>
            /// Spanish
            /// </summary>
            ES = 3,

            /// <summary>
            /// French
            /// </summary>
            FR = 4,

            /// <summary>
            /// Portuguese
            /// </summary>
            PT = 5
```

</details>

<details>
    <summary>WeatherUnit</summary>

```

            /// <summary>
            /// Celsius
            /// </summary>
            C,
            /// <summary>
            /// Fahrenheit 
            /// </summary>
            F
```

</details>


<details>
    <summary>Categories</summary>

```

            /// <summary>
            /// Any
            /// </summary>
            Any = 0,

            /// <summary>
            /// Programming
            /// </summary>
            Programming = 1,

            /// <summary>
            /// Misc
            /// </summary>
            Miscellaneous = 2,

            /// <summary>
            /// Dark
            /// </summary>
            Dark = 4,

            /// <summary>
            /// Pun
            /// </summary>
            Pun = 8,

            /// <summary>
            /// Spooky
            /// </summary>
            Spooky = 16,

            /// <summary>
            /// Christmas
            /// </summary>
            Christmas = 32
```

</details>
