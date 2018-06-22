# TODO application

A simple todo application for understanding the basic components of ASP.NET
core, MVC and EntityFramework Core.

## For running the application

Requires .net core 2.1.

For running the project from the `todo` project:

    $ dotnet restore
    $ dotnet ef database update
    $ dotnet run

For running the tests (from the `todo.tests` directory):

    $ dotnet test
