using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly.GetName().Name;
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AspNetIdentityContext>();
builder.Services.AddIdentityServer().AddAspNetIdentity<IdentityUser>().AddConfigurationStore(opt =>
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

app.UseRouting();

app.UseIdentityServer();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(x =>
{
    x.MapDefaultControllerRoute();
});

app.Run();
