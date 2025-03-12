using MicroServices.Order.Api;
using MicroServices.Order.Api.Endpoints.Orders;
using MicroServices.Order.Application;
using MicroServices.Order.Application.Contracts.Repositories;
using MicroServices.Order.Application.Contracts.UnitOfWorks;
using MicroServices.Order.Persistence;
using MicroServices.Order.Persistence.Repositories;
using MicroServices.Order.Persistence.UnitOfWork;
using MicroServices.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));

builder.Services.AddDbContext<AppDbContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddVersioningExt();

var app = builder.Build();
app.AddOrderGroupEndpointExt(app.AddVersionSetExt());

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
