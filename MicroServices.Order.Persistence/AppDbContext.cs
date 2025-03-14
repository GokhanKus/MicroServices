﻿using MicroServices.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroServices.Order.Persistence
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
	    public DbSet<Address> Addresses { get; set; }
		public DbSet<Domain.Entities.Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceAssembly).Assembly);
		}
	}
}
