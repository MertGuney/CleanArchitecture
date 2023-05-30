var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddApplicationLayer();

    builder.Services.AddInfrastructureLayer();

    builder.Services.AddPersistenceLayer(builder.Configuration);

    builder.Services.AddControllers(opts =>
    {
        opts.Filters.Add(new ValidationFilterAttribute());
    }).ConfigureApiBehaviorOptions(opts =>
    {
        opts.SuppressModelStateInvalidFilter = true;
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    logger.Info("Starting host...");

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //app.UseMiddleware<ExceptionMiddlewareExtensions>();
    //app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}