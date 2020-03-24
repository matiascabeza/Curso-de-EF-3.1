using EFCore_Mod4Facturas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Mod4Facturas
{
    class ApplicationDbContext : DbContext
    {
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=DESKTOP-75L2R7E\\SQLEXPRESS;Initial Catalog=EFCore_Mod4Facturas;Integrated Security=True")
				.EnableSensitiveDataLogging(true)
				//.UseLazyLoadingProxies()
				.UseLoggerFactory(GetLoggerFactory);
		}
		public static readonly ILoggerFactory GetLoggerFactory = LoggerFactory.Create(builder =>
		{
			builder
			   .AddFilter((category, level) =>
				   category == DbLoggerCategory.Database.Command.Name
				   && level == LogLevel.Information)
			   .AddConsole();
		});
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var productos = new List<Product>()
			{
				new Product(){Id=1, Name = "Lámpara", Description = "Para iluminar tu vida", Price = 40.99m},
				new Product(){Id=2, Name = "Tableta Inteligente", Description = "Para que sus hijos se críen solos", Price = 180.99m}
			};
			modelBuilder.Entity<Product>().HasData(productos);
			modelBuilder.Entity<InvoiceDetail>().Property(x => x.Total).HasComputedColumnSql("[Quantity] * [Price]");
			modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("decimal(9,2)");
			modelBuilder.Entity<InvoiceDetail>().Property(x => x.Price).HasColumnType("decimal(9,2)");
			modelBuilder.Entity<InvoiceDetail>().Property(x => x.Total).HasColumnType("decimal(12,2)");
			modelBuilder.Entity<Invoice>().Property(x => x.Total).HasColumnType("decimal(16,2)");

			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Invoice> Invoices { get; set; }
		public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
		public DbSet<Product> Products { get; set; }

	}
}
