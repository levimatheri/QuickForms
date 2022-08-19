using MongoDB.Driver.Core.Configuration;
using QuickForms.API.Database;
using QuickForms.API.Extensions;
using QuickForms.API.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("QuickFormsDatabase"));

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddAutoMapper(typeof(SurveyProfile));

builder.Services.AddMongoDb();

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
// cd /data/
// docker run -d -p 27017:27017 --name test-mongo -v backup:/backup mongo:latest
// docker exec -t c0c mongorestore --gzip --archive=/backup/quickforms.gz
// docker exec -t 047 mongodump --gzip --db QuickForms --archive=/backup/quickforms.gz
