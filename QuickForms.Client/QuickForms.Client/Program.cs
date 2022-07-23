using Microsoft.Net.Http.Headers;
using MudBlazor.Services;
using QuickForms.Client;
using QuickForms.Client.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddHttpClient(Constants.QuickFormsApi.Name, httpClient =>
{
    httpClient.BaseAddress = new Uri("http://localhost:5122");
    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});
builder.Services.AddSingleton<ISurveyRepository, SurveyRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
