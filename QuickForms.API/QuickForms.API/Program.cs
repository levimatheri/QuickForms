using QuickForms.API.Models;
using QuickForms.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("QuickFormsDatabase"));

builder.Services.AddSingleton<SurveyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
//docker run -d -p 27017:27017 --name test-mongo -v data:/data/db mongo:latest
//docker exec -t e0 mongodump --archive --gzip --db QuickForms > quickforms_db.gz
//docker exec -t e0 mongorestore --archive --gzip < wings_db.gz
