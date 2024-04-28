using MediConecctPro.Utils;
using MediConnectPro.Bs.Servicios;
using MediConnectPro.Core.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI
builder.Services.AddSingleton<RepoDB>();
builder.Services.AddSingleton<IDbConnection>(_ => new SqlConnection(builder.Configuration.GetConnectionString("ConnectionMediConnect")!.ToString()));
builder.Services.AddScoped<ICitaServicio, CitaServicio>();
builder.Services.AddScoped<IMedicoServicio, MedicoServicio>();
builder.Services.AddScoped<IPacientesServicio, PacientesServicio>();
builder.Services.AddScoped<IPerfilServicio, PerfilServicio>();
builder.Services.AddScoped<IUsuariosServicio, UsuariosServicio>();

builder.Services.AddSwaggerDocumentation();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddCors(o => o.AddPolicy("MediConnectPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(3);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Filters
builder.Services.AddMvcCore(m =>
{
    m.Filters.Add<FiltersAttribute>();
});

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(AuthorizationToken));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1); // Disable swagger schemas at bottom
    });
}

app.UseHttpsRedirection();

app.UseCors("PolicyPortal");
app.UseSession();

app.UseAuthorization();

app.MapControllers();

app.Run();
