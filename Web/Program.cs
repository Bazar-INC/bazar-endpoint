using Application;
using Application.Features.AuthFeatures.Services;
using Core.Entities;
using Infrastructure;
using Infrastructure.UnitOfWork;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using Shared;
using Web;
using Web.Extensions;
using Web.Middlewares.ErrorsHandlingMiddleware;

var logger = LogManager.Setup().LoadConfigurationFromFile(AppSettings.Constants.NLogConfigPath).GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    // Add services to the container.

    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

    builder.Services.AddBearer(builder.Configuration.GetValue<string>("Jwt:Secret"));

    builder.Services.AddIdentity<UserEntity, RoleEntity>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<JwtService>();

    builder.Services.AddMediatR(typeof(MediatrAssemblyReference).Assembly);

    builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseSeed();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseMiddleware<ErrorsHandlingMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();


}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}