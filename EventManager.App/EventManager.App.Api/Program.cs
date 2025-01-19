using EventManager.App.Api.Basic;
using EventManager.App.Api.Basic.Interfaces;
using EventManager.App.Api.Basic.Middleware;
using EventManager.App.Api.Basic.Models;
using EventManager.App.Api.Basic.Services;
using EventManager.App.Api.Extended.Interfaces;
using EventManager.App.Api.Extended.Services;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerSetup();

builder.Logging.AddApplicationInsights(
    configureTelemetryConfiguration: (config) => config.ConnectionString = configuration["ApplicationInsights:ConnectionString"],
    configureApplicationInsightsLoggerOptions: (options) => { }
);

builder.Services.AddAuthenticationSetup(configuration);

builder.Services.Configure<JwtConfig>(configuration.GetSection(nameof(JwtConfig)));
builder.Services.Configure<AzureTableConfig>(configuration.GetSection(nameof(AzureTableConfig)));
builder.Services.Configure<EmailConfig>(configuration.GetSection(nameof(EmailConfig)));

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient<IOtpService, OtpService>();
//builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddSingleton<IEmailService, AzEmailService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthHandler, AuthHandler>();
builder.Services.AddScoped<IUserHandler, UserHandler>();

builder.Services.AddSingleton<IProfilePhotoRepository, ProfilePhotoRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileHandler, ProfileHandler>();

builder.Services.AddSingleton<INotificationRepository, NotificationRepository>();
builder.Services.AddSingleton<IMentorshipRepository, MentorshipRepository>();
builder.Services.AddSingleton<IPostRepository, PostRepository>();
builder.Services.AddSingleton<IEventsRepository, EventRepository>();
builder.Services.AddSingleton<IBusinessRepository, BusinessRepository>();

builder.Services.AddSingleton<IPostHandler, PostHandler>();
builder.Services.AddSingleton<INotificationHandler, NotificationHandler>();
builder.Services.AddSingleton<IBusinessHandler, BusinessHandler>();
builder.Services.AddSingleton<IMentorshipHandler, MentorshipHandler>();
builder.Services.AddSingleton<IEventsHandler, EventsHandler>();

builder.Services.AddSingleton<IExpenseRepository, ExpenseRepository>();
builder.Services.AddSingleton<IExpenseHandler, ExpenseHandler>();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();
app.Run();
