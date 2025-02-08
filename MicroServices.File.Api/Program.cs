using MicroServices.File.Api;
using MicroServices.Shared.Extensions;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
//wwwroot icerisindeki picture'a erismek istedigim zaman IFileProvider interfacesini kullanmak istedigimiz classta ctor injection yaparak kullanabiliriz.

builder.Services.AddCommonServiceExt(typeof(FileAssembly));
builder.Services.AddVersioningExt();

var app = builder.Build();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.MapOpenApi();
}

app.Run();
