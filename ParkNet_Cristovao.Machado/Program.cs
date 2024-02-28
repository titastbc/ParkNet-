
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParkNet_Cristovao.Machado.Data;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;
using ParkNet_Cristovao.Machado.Data.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<FloorRepository>();
builder.Services.AddScoped<LayoutGestorService>();
builder.Services.AddScoped<ParkingSpaceRepository>();
builder.Services.AddScoped<ParkRepository>();
builder.Services.AddScoped<GeneralRepository>();
builder.Services.AddScoped<VehicleRepository>();
builder.Services.AddScoped<Checker>();
builder.Services.AddScoped<TariffPermitRepository>();
builder.Services.AddScoped<InicialConfigurator>();
builder.Services.AddScoped<StringHelper>();
builder.Services.AddScoped<TariffTicketRepository>();
builder.Services.AddScoped<WalletManager>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<PriceCalculator>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Customer>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
