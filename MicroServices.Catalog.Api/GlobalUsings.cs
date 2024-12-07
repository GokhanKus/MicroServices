global using AutoMapper;
global using MediatR;
global using Microsoft.EntityFrameworkCore;
global using MicroServices.Shared;
global using MicroServices.Shared.Extensions;
global using MassTransit;
global using System.Net;
global using FluentValidation;
global using MicroServices.Catalog.Api.Features.Categories.Dtos;
global using MicroServices.Catalog.Api.Repositories;
//classlarimizda ortak olan using ifadelerini tek bir çatı altında topladık bu sayede her classda usingleri yazmayıp kod okunabilirligini artirdik 
//dezavantaji ise çok da olmasa performans kaybi..