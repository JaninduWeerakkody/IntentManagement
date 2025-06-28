using System.Text;
using IntentManagementAPI.Data;
using IntentManagementAPI.Data.Repositories;
using IntentManagementAPI.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using IntentManagementAPI.Models.Core;
using IntentManagementAPI.Models.Supporting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext
builder.Services.AddDbContext<IntentManagementContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IIntentRepository, IntentRepository>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add Newtonsoft.Json support for JsonPatch
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    })
    .AddJsonOptions(options =>
    {
        // Configure System.Text.Json to ignore null values so they appear as undefined
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var keyString = jwtSettings["Key"];
if (string.IsNullOrEmpty(keyString))
{
    throw new InvalidOperationException("Jwt:Key not found in configuration.");
}
var key = Encoding.ASCII.GetBytes(keyString);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"]
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IntentManagementAPI", Version = "v1" });

    // Configure Swagger to use JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Test")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.EnvironmentName != "Test")
{
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
}

app.MapControllers();

// SEED DATABASE FOR DEVELOPMENT
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Test")
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<IntentManagementContext>();
        db.Database.EnsureCreated();

        // Only seed if empty
        if (!db.Intents.Any())
        {
            var intent = new Intent {
                Id = 1,
                Name = "EventLiveBroadcast",
                Description = "Seeded intent for EventLiveBroadcast",
                Type = "Intent",
                CreationDate = DateTime.UtcNow,
                LastUpdate = DateTime.UtcNow,
                StatusChangeDate = DateTime.UtcNow,
                LifecycleStatus = "Created"
            };
            db.Intents.Add(intent);
            db.SaveChanges();
        }
        if (!db.IntentSpecifications.Any())
        {
            var spec = new IntentSpecification {
                Id = 1,
                Name = "DefaultSpec",
                Description = "Seeded intent specification",
                Type = "IntentSpecification",
                Version = "1.0"
            };
            db.IntentSpecifications.Add(spec);
            db.SaveChanges();
        }
        
        // Ensure IntentReports exist for all Intents - create multiple reports per intent
        var existingIntents = db.Intents.ToList();
        foreach (var intent in existingIntents)
        {
            // Create multiple reports per intent for better test coverage
            for (int i = 1; i <= 3; i++)
            {
                var reportId = intent.Id * 10 + i; // Generate unique IDs
                if (!db.IntentReports.Any(r => r.Id == reportId))
                {
                    var report = new IntentReport {
                        Id = reportId,
                        Name = $"Report{reportId}",
                        Description = $"Seeded intent report {i} for {intent.Name}",
                        Type = "IntentReport",
                        BaseType = "Entity",
                        SchemaLocation = "https://schema.example.com/IntentReport",
                        CreationDate = DateTime.UtcNow,
                        Intent = intent
                    };
                    db.IntentReports.Add(report);
                }
            }
        }
        db.SaveChanges();
    }
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<IntentManagementContext>();
        var intentRepository = services.GetRequiredService<IRepository<Intent>>();
        
        // Initialize database
        await context.Database.EnsureCreatedAsync();
        await DataSeeder.SeedData(intentRepository);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initializing the database.");
        throw; // Re-throw the exception to prevent the app from starting
    }
}

app.Run();
