using Microsoft.EntityFrameworkCore;
using OrderManagement.Data;
using OrderManagement.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Ovde registrujem servise za kontrolere
builder.Services.AddOpenApi(); // ovde registrujem servise za generisanje OepenAPI dokumentacije

// AddScoped - registruje servis koji ce biti kreiran za svaki zahtev (request) i unisten nakon zavrsetka obrade zahteva
// Tu je razlika u odnosu na javu jer je u javi default singleton a ovo bi bio ekvivalent @RequestScope

builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Konekcija ka bazi podataka se konfiguriše u appsettings.json fajlu, a ovde se samo registruje servis za DbContext koji ce koristiti tu konekciju. U Spring Boot-u bi ovo bilo ekvivalentno @EnableJpaRepositories i @EntityScan anotacijama, kao i konfiguraciji DataSource-a u application.properties ili application.yml fajlu.
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Middleware komponente obrađuju HTTP request redosledom kojim su konfigurisane. Isto kao ServletFilterChain, odnosno Spring Security Filter Chain za security deo U Spring Boot
app.UseHttpsRedirection();

app.UseAuthorization(); // Ne ukljucuje automatski login ili JWT autorizaciju, vec samo proverava da li je korisnik autorizovan za pristup resursu. Za autentifikaciju i autorizaciju se koriste dodatne biblioteke kao sto su Microsoft.AspNetCore.Authentication.JwtBearer ili IdentityServer4

app.MapControllers(); // registruje endpoint-e definisane atributima u Controllerima

app.Run(); // Fire! Pokrece web aplikaciju i blokira thread dok se ne zavrsi obrada zahteva. U Spring Boot-u je to ekvivalent SpringApplication.run()
