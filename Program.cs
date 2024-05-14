using Services;
using Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISingletonService, SingletonService>();
builder.Services.AddScoped<IScopedService, ScopedService>();
builder.Services.AddTransient<ITransientService, TransientService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/singleton", async (ISingletonService service) =>
{
    // An only instance of SingletonService is created and shared across all requests
    return new SingletonResponseDto { CreationDateTime = service.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss.fff") };
})
.WithName("GetSingleton")
.WithOpenApi();

app.MapGet("/scoped", async (IServiceProvider serviceProvider) =>
{
    var service = serviceProvider.GetRequiredService<IScopedService>();
    var creationTimeFirstRequest = service.GetDateTime();

    await Task.Delay(5000);

    service = serviceProvider.GetRequiredService<IScopedService>();
    var creationTimeSecondRequest = service.GetDateTime();

    return new ResponseDto
    { 
        FirstInstanceDateTime = creationTimeFirstRequest.ToString("yyyy-MM-dd HH:mm:ss.fff"), 
        SecondInstanceDateTime = creationTimeSecondRequest.ToString("yyyy-MM-dd HH:mm:ss.fff")
    };
})
.WithName("GetScoped")
.WithOpenApi();

app.MapGet("/transient", async (IServiceProvider serviceProvider) =>
{
    var service = serviceProvider.GetRequiredService<ITransientService>();
    var creationTimeFirstRequest = service.GetDateTime();

    await Task.Delay(5000);

    service = serviceProvider.GetRequiredService<ITransientService>();
    var creationTimeSecondRequest = service.GetDateTime();

    return new ResponseDto
    { 
        FirstInstanceDateTime = creationTimeFirstRequest.ToString("yyyy-MM-dd HH:mm:ss.fff"), 
        SecondInstanceDateTime = creationTimeSecondRequest.ToString("yyyy-MM-dd HH:mm:ss.fff")
    };
})
.WithName("GetTransient")
.WithOpenApi();

app.Run();
