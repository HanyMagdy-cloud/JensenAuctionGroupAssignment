using JensenAuctionGroupAssignment;
using JensenAuctionGroupAssignment.Entites.Repo;
using JensenAuctionGroupAssignment.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    //CORS
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5074/index.html") // Allow only this domain
              .AllowAnyHeader()                  // Allow all headers
              .AllowAnyMethod();                 // Allow GET, POST, PUT, DELETE etc.
    });

    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin() // Allow all origins
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add controllers
builder.Services.AddControllers();

// Add dependency injection
builder.Services.AddScoped<IJensenAuction, JensenContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
builder.Services.AddScoped<IBidRepository, PlaceBidRepository>();

// Define the JWT secret key
var jwtSecretKey = "mykey1234567&%%485734579453%&//1255362";

// Register AuthService and pass the JWT secret key
builder.Services.AddScoped<IAuthService>(provider =>
    new AuthService(
        provider.GetRequiredService<IUserRepository>(), // Inject IUserRepository
        jwtSecretKey 
    )
);

// Configure JWT authentication
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Ensure the token has a valid issuer
        ValidateAudience = true, // Ensure the token has a valid audience
        ValidateLifetime = true, // Ensure the token has not expired
        ValidateIssuerSigningKey = true, // Ensure the token's signing key is valid
        ValidIssuer = "http://localhost:5290", // The expected issuer
        ValidAudience = "http://localhost:5290", // The expected audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)) // Use the defined JWT secret key
    };
});

// Add Swagger with JWT Authorization button
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Jensen Auction API", Version = "v1" });

    // Add JWT Authentication configuration
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and the JWT token.\n\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Activate CORS
app.UseCors("AllowSpecificOrigins");

// Enable Swagger and Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jensen Auction API v1");
    c.RoutePrefix = string.Empty; // To make Swagger UI available at the root URL
});

// Use routing
app.UseRouting();

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapControllers();

app.Run();
