# TaskManagerWebAPI
A simple demo project showing the structure and usage of [ASP.NET Core with WebAPI](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0) and [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

## Prerequisites
* [Visual Studio](https://visualstudio.microsoft.com/)
* [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* Any supported SQL server
* [Entity Framework Core tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

## Features
* Based on [.NET 6](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6) and [AspNetCore 6](https://docs.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-6.0?view=aspnetcore-6.0) with [C# 10](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/whatsnew) ([Code-First approach](https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/new-database))
* Extendable code with [MVC architecture](https://docs.microsoft.com/en-us/aspnet/mvc/) and [Repository pattern](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)
* [Swagger](https://swagger.io/) for development
* [AutoMapper](https://automapper.org/) used to handle models mapping

## Building & Running
* Clone the repository

```
git clone https://github.com/akikodev/TaskManagerWebAPI.git
```

* Open the solution file in Visual Studio
* Configure database  ```DefaultConnection``` inside ```appsettings.json```
* Add [migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli) and update database using Terminal window

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

* And finally run the app

```
dotnet run
```

* Now it can be accessed through ```localhost:5201/swagger/index.html```

## API endpoints
**POST Create a new project**
```http://localhost:5201/api/v1/Project```

**GET Get all existing projects**
```http://localhost:5201/api/v1/Project```

**GET Get a project by id**
```http://localhost:5201/api/v1/Project/{projectId}```

**PUT Update a project by id**
```http://localhost:5201/api/v1/Project/{projectId}```

**DELETE Delete a project by id**
```http://localhost:5201/api/v1/Project/{projectId}```

**GET Gets project tasks**
```http://localhost:5201/api/v1/Project/{projectId}/Tasks```

**POST Create a new task**
```http://localhost:5201/api/v1/Task```

**GET Get all existing tasks**
```http://localhost:5201/api/v1/Task```

**GET Get a task by id**
```http://localhost:5201/api/v1/Task/{taskId}```

**PUT Update a task by id**
```http://localhost:5201/api/v1/Task/{taskId}```

**DELETE Delete a task by id**
```http://localhost:5201/api/v1/Task/{taskId}```

**PUT Add task to a specified project**
```http://localhost:5201/api/v1/Task/{taskId}/AddToProject/{projectId}```

**POST Remove task from referenced project**
```http://localhost:5201/api/v1/Task/{taskId}/RemoveFromProject```

## License
Copyright 2022 Aki Aoki

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
