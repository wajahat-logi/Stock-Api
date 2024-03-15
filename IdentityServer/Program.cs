using IdentityServer.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var defaultConnString = builder.Configuration.GetConnectionString("DefaultConnection");

var assembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<StockDBContext>(option => option.UseSqlServer(defaultConnString));


builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<StockDBContext>();


builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assembly));
    })
    .AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();
var app = builder.Build();

#region Initialized Database
//using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
//{
//    serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

//    var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
//    context.Database.Migrate();
//    if (!context.Clients.Any())
//    {
//        foreach (var client in Config.GetClients())
//        {
//            context.Clients.Add(client.ToEntity());
//        }
//        context.SaveChanges();
//    }

//    if (!context.IdentityResources.Any())
//    {
//        foreach (var resource in Config.GetIdentityResources())
//        {
//            context.IdentityResources.Add(resource.ToEntity());
//        }
//        context.SaveChanges();
//    }

//    if (!context.ApiScopes.Any())
//    {
//        foreach (var resource in Config.ApiScopes.ToList())
//        {
//            context.ApiScopes.Add(resource.ToEntity());
//        }

//        context.SaveChanges();
//    }

//    if (!context.ApiResources.Any())
//    {
//        foreach (var resource in Config.GetApis())
//        {
//            context.ApiResources.Add(resource.ToEntity());
//        }
//        context.SaveChanges();
//    }
//}
#endregion
// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseRouting(); // Add this line to configure endpoint routing
app.UseIdentityServer();
app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute(); // Configure endpoints for controllers
                                // Additional endpoints configuration if needed
});

app.Run();