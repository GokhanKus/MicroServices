using MicroServices.Basket.Api.Features.Baskets;
using MicroServices.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommonServiceExt(typeof(BasketAssembly));
//TODO: BasketService'in interfaceyi yazilmadi concrete olarak kullaniyoruz ioc kaydi yapilabilir
builder.Services.AddScoped<BasketService>();
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = builder.Configuration.GetConnectionString("redis");
});

builder.Services.AddVersioningExt();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.AddBasketGroupEndpointExt(app.AddVersionSetExt());

app.Run();
