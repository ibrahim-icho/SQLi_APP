using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sqli_CRM_Web_Application.Connection;
using Sqli_CRM_Web_Application.Context;
using Sqli_CRM_Web_Application.Helpers;
using Sqli_CRM_Web_Application.Models;
using Sqli_CRM_Web_Application.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
            "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
            "Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
//IdentityBuilder
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders();
/////////////////////////////////////////////////////////////


// Add configuration
var Configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
builder.Services.AddSingleton<IConfiguration>(Configuration);

//JWT configuration
builder.Services.Configure<JWT>(Configuration.GetSection("JWT"));

//Connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
);

// Register implementations

builder.Services.AddScoped<Dynamics365Connection>();
builder.Services.AddScoped<IOpportunitiesService, OpportunitiesService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IIncidentsService, IncidentsService>();
builder.Services.AddScoped<IOpportunityProductsService, OpportunityProductsService>();
<<<<<<< HEAD
=======
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
>>>>>>> 19842453ff4d060109eca9a963038f557ac61371
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = Configuration["JWT:Issuer"],
            ValidAudience = Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]!))
        };
    });
    

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.WithOrigins("https://localhost:44351", "http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
