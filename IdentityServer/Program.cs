using IdentityServer;
using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var seed = args.Contains("/seed");
if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly.GetName().Name;
if (seed)
{
    SeedData.EnsureSeedData(builder.Configuration.GetConnectionString("DefaultConn"));
}
// Add services to the container.
builder.Services.AddDbContext<AspNetIdentityContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn"),b=>b.MigrationsAssembly(assembly)));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AspNetIdentityContext>();
builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(opt =>
{
    opt.ConfigureDbContext = b => b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn"), opt => opt.MigrationsAssembly(assembly));
}).AddOperationalStore(opt =>
{
    opt.ConfigureDbContext = b => b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn"), opt => opt.MigrationsAssembly(assembly));
}).AddDeveloperSigningCredential();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseIdentityServer();

app.UseRouting();

app.UseEndpoints(x =>
{
    x.MapDefaultControllerRoute();
});

app.Run();
