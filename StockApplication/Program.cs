using IdentityServer4.Models;
using StockApplication.IdentityConfiguration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityServer()
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryApiResources(StockApplication.IdentityConfiguration.Resources.ApiResources)
                .AddInMemoryApiScopes(Scopes.GetApiScopes())
                .AddTestUsers(Users.Get())
                .AddDeveloperSigningCredential();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting(); // identity
app.UseAuthentication(); // identity    
app.UseAuthorization();
app.UseIdentityServer(); // identity
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers(); // identity
//});
app.MapControllers();


app.Run();
