# Pitman.OrderBy

**Pitman.OrderBy** is a .NET library that provides an implementation of an OrderByQuery class that enables you to sort an **IQueryable** collection of entities dynamically based on a field passed as a query parameter. With "**OrderBy**", you can automatically get the field of order by from your endpoint and apply it to an IQueryable.

Usage
You can use "OrderBy" in your .NET project by installing the "OrderByQuery" NuGet package.

Installation
You can install "OrderByQuery" by running the following command in the Package Manager Console:
```
Install-Package Pitman.OrderBy
```

## Example
Here's an example of how to use "OrderByQuery" to sort an IQueryable collection of entities based on a field passed as a query parameter.

### Using OrderByQuery
```
var orderBy = new OrderByQuery("age:asc");

var list = new List<Person>
{
    new Person() { Id = 3, Name = "C", Age = 3 },
    new Person() { Id = 4, Name = "D", Age = 4 },
    new Person() { Id = 1, Name = "A", Age = 1 },
    new Person() { Id = 2, Name = "B", Age = 2 },
    new Person() { Id = 5, Name = "E", Age = 5 }
};

var orderedList = orderBy.ApplyOrder(list.AsQueryable());
```
### Using Asp.net
On a controller use **[FromQuery] OrderBy** and apply the order on an IQueryable list.
In the url add the parameter **order** with the value of **{propertyName}:{direction}**. 
Direction can be either **asc** or **desc**.

Example of an url sorting the by temperatureC as property name:
```
https://localhost:7201/weatherforecast?order=temperatureC:asc
or
https://localhost:7201/weatherforecast?order=temperatureC:desc
```

#### Full Example
```
[HttpGet]
public IEnumerable<WeatherForecast> Get([FromQuery] OrderBy orderBy) // receive order from url parameters
{
    return Results(orderBy);
}

private static IEnumerable<WeatherForecast> Results(IOrderBy orderBy)
{
    var queryable =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .AsQueryable();

    queryable = orderBy.ApplyOrder(queryable); // apply the order on the IQueryable

    return queryable;

}
```

## Contributing
Contributions to "OrderByQuery" are welcome and encouraged! If you find a bug or have a feature request, please create an issue in the repository. If you'd like to contribute code to the project, please open a pull request with your changes.

## License
"OrderBy" is released under the MIT License. See LICENSE for details.
