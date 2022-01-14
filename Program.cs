using System.Text;
using backend.Repositories;
using backend.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Stripe;
using DotNetEnv;
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String));
// Add services to the container.
builder.Services.AddCors();

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    string ConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    return new MongoClient(connectionString: ConnectionString);
});
builder.Services.AddSingleton<IReviewRepository, ReviewRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IJWTRepository, JWTRepository>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.Configure<StripeOptions>(options =>
            {
                options.PublishableKey = Environment.GetEnvironmentVariable("STRIPE_PUBLISHABLE_KEY");
                options.SecretKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");
                options.WebhookSecret = Environment.GetEnvironmentVariable("STRIPE_WEBHOOK_SECRET");
            });
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
   {
       options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
       {
           Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
           In = ParameterLocation.Header,
           Name = "Authorization",
           Type = SecuritySchemeType.ApiKey
       });


   }


);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY"))),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options
    .WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
