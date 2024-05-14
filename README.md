## Lifecycle management in Dependency Injection

This repository is reserved to provide an explanation of the three types of lifecycles modes used in Dependency Injection, in this case w/ .NET Core. 
The modes are **Singleton**, **Scoped** and **Transient**.

### Singleton
- Only one service instance was created during the app lifecycle. 
- All requests to obtain this object will return the same instance. 
- It's useful for objects shared by application, such as database connections or global configs. 

### Scoped 
- We get a new instance with every request, or "Scope".
- Within the same scope, all requests will return the same instace of the object.
- It's useful for sharing resources during request processing without global sharing, as in web app. 

### Transient 
- Is created every time it's requested.
- Each request to obtain this object will return a new instance. 
- Useful for objects that don't need to be shared between different parts of the app.

## :sparkles: Hands On!

In this small project, you'll find three endpoints: 

![image](https://github.com/kmlyteixeira/singleton-scoped-transient-dotnet/assets/101020416/69746d8e-9f97-49c1-b90b-9d56872567e3)

In each of them, a **service** with a specific **lifecycle mode** was registered and this simple service **returns the creation date of the object instance**.

#### [GET] /singleton
An only instance of SingletonService is created and **shared across all requests**

`builder.Services.AddSingleton<ISingletonService, SingletonService>()`

Response body
```
{
  "creationDateTime": "2024-05-14 17:43:00.743"
}
```

#### [GET] /scoped
Two requests will have different instances of ScopedService and they return the same creation time because they are created in the **same scope**

`builder.Services.AddScoped<IScopedService, ScopedService>()`

Response body
```
{
  "firstInstanceDateTime": "2024-05-14 17:57:37.145",
  "secondInstanceDateTime": "2024-05-14 17:57:37.145",
  "areEqual": true
}
```

#### [GET] /transient 
Two requests will have different instances of TransientService and they return **different creation times**

`builder.Services.AddTransient<ITransientService, TransientService>()`

Response body
```
{
  "firstInstanceDateTime": "2024-05-14 17:58:02.964",
  "secondInstanceDateTime": "2024-05-14 17:58:07.964",
  "areEqual": false
}
```

### :hammer: Built with .NET 8

### :runner: Installing and Running

1.  Clone this repo: https://github.com/kmlyteixeira/singleton-scoped-transient-dotnet
2.  Run `dotnet build` to build this project
3.  Run `dotnet run`
4.  Open `http://localhost:5225/swagger` and **be happy** :)

## :books: __Learn more__

:one: [Dependency injection in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-8.0)

:two: [Singleton Design Pattern](https://refactoring.guru/design-patterns/singleton)

3️⃣ [Differences Between Scoped, Transient, And Singleton Service](https://www.c-sharpcorner.com/article/differences-between-scoped-transient-and-singleton-service/)
