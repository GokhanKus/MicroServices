using MicroServices.Catalog.Api;
using MicroServices.Catalog.Api.Features.Categories;
using MicroServices.Catalog.Api.Features.Courses;
using MicroServices.Catalog.Api.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();

builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));

var app = builder.Build();

app.AddSeedDataExt().ContinueWith(x =>
{  
	Console.WriteLine(x.IsFaulted ? x.Exception?.Message : "Seed data has been saved successfully");
});

app.AddCategoryGroupEndpointExt();
app.AddCourseGroupEndpointExt();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.Run();