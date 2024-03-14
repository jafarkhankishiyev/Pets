using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Integration.WebApi;
using CommonServices.DBServices.AuthServices;
using CommonServices.DBServices.PetDB;
using CommonServices.DBServices.UserDB;
using Microsoft.Win32.SafeHandles;
using System.Reflection;
using System.Web.Http;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((container) =>
    {
        container.RegisterApiControllers(Assembly.GetExecutingAssembly());

        //container.RegisterType<PostgresUserDBFactory>().As<IUserDBFactory>();
        container.RegisterType<PostgresPetDBService>().As<IPetDBService>();
        container.RegisterType<PostgresUserDBService>().As<IUserDBService>();
        container.RegisterType<PostgresAuthService>().As<IAuthService>();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
