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

## Contributing
Contributions to "OrderByQuery" are welcome and encouraged! If you find a bug or have a feature request, please create an issue in the repository. If you'd like to contribute code to the project, please open a pull request with your changes.

## License
"OrderBy" is released under the MIT License. See LICENSE for details.
