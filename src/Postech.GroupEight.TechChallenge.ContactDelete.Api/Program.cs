using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Postech.GroupEight.TechChallenge.ContactDelete.Api.Setup;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Http.Adapters;
using Prometheus;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:5013");
builder.Configuration.AddJsonFileByEnvironment(builder.Environment.EnvironmentName);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenConfiguration();
builder.Services.AddDependencyFluentValidation();
builder.Services.AddDependencyRequestCorrelationId();
builder.Services.AddDependencyNotifier();
builder.Services.AddDependencyRabbitMQ(builder.Configuration);
builder.Services.AddDependencyEventPublisher();
builder.Services.AddDependencyUseCase();
builder.Services.AddHealthChecks().AddRabbitMQHealthCheck();

WebApplication app = builder.Build();

app.MapHealthChecks("/health"); 
app.MapHealthChecks("/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.UseSwagger();
app.MapSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));

app.UseMetricServer();
app.UseHttpMetrics();
app.UseHttpsRedirection();

AspNetCoreAdapter http = new(app);
_ = new ContactsController(http);
http.Run();

[ExcludeFromCodeCoverage]
public partial class Program
{
    protected Program() { }
}