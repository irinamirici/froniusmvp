using Fronius;
using Fronius.Services;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true);

// Add services to the container.
builder.Services.AddHttpClient(Constants.FroniusApiClient, httpClient => {
    httpClient.BaseAddress = new Uri("https://swb-assessment.azurewebsites.net/api/");

    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json; charset=utf-8");
    httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "FroniusWebApiMVP");
    httpClient.DefaultRequestHeaders.Add("api-key", builder.Configuration["FroniusApiKey"]);
});

builder.Services.AddScoped<IPhotovoltaicService, PhotovoltaicService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
