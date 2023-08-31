using Business_Logic_Layer.Models;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Data;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(option => option.AddPolicy(name: "CompanyOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));

builder.Services.AddCors(option => option.AddPolicy(name: "EmployeeOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.Configure<AppSettingModel>(builder.Configuration.GetSection("AppSettings"));

var SecretKey = builder.Configuration["AppSettings:SecretKey"];
var SecretKeyBytes = Encoding.UTF8.GetBytes(SecretKey);
var Issuer = builder.Configuration["AppSettings:Issuer"];
var Audience = builder.Configuration["AppSettings:Audience"];

//Authentication don't use keycloak
/*builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //Tự cấp token
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,

        //Ký vào token
        ValidateIssuerSigningKey = true,
        ValidIssuer = Issuer,
        ValidAudience = Audience,
        IssuerSigningKey = new SymmetricSecurityKey(SecretKeyBytes),

        ClockSkew = TimeSpan.Zero
    };
});*/

//Authentication use keycloak
builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    string keycloakServerUrl = builder.Configuration["Keycloak:auth-server-url"] + $"realms/{builder.Configuration["Keycloak:realm"]}/";
    string clientId = builder.Configuration["Keycloak:resource"];
    options.Authority = keycloakServerUrl;
    options.Audience = clientId;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = keycloakServerUrl,
        ValidAudiences = new List<string> { "master-realm", "account", clientId },
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ClockSkew = TimeSpan.Zero
    };
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.Validate();
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CompanyOrigins");

app.UseCors("EmployeeOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
