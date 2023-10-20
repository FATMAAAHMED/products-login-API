using AutoMapper;
using Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProdductApplication;
using ProductDomain.Entities;
using ProductDTOS;
using prouctInfrastructure;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBconnection"));
});
//

// Add JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Set to true if you want to validate the issuer
        ValidateAudience = true, // Set to true if you want to validate the audience
        ValidateLifetime = true, // Set to true if you want to validate the expiration
        ValidateIssuerSigningKey = true, // Set to true if you want to validate the signing key
        ValidIssuer = "your_issuer", // Change to your issuer
        ValidAudience = "your_audience", // Change to your audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("IOLJYHSDSIoleJHsdsdsas98WeWsdsdQweweHgsgdf_&6#2")) // Change to your secret key
    };
});
var config = new MapperConfiguration(cfg => { cfg.AddProfile<UserProfile>(); });

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization();

    // Add other services and middleware
builder.Services.AddIdentity<User, IdentityRole<long>>(options => {
options.SignIn.RequireConfirmedAccount = false;
options.Password.RequireDigit = true;
options.Password.RequireLowercase = true;
options.Password.RequireUppercase = true;
options.Password.RequireNonAlphanumeric = true;
options.Password.RequiredLength = 8;

    // Configure lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;

    // Configure user settings
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<DContext>()
.AddDefaultTokenProviders();


//
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
                    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.ConfigureApplicationCookie(options =>
{

    options.LoginPath = "/User/SignIn";
});


builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();


app.UseAuthentication();
 app.UseAuthorization();

app.MapControllers();


app.Run();
