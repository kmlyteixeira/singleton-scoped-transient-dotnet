using Services;
using Dtos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISingletonService, SingletonService>();
builder.Services.AddScoped<IScopedService, ScopedService>();
builder.Services.AddTransient<ITransientService, TransientService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/singleton", async (ISingletonService service) =>
{
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
