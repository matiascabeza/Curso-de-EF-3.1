using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEFCoreWinforms.Models
{
    class ApplicationDbContext : DbContext
    {
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=DESKTOP-75L2R7E\\SQLEXPRESS;Initial Catalog=EFCore_Mod3;Integrated Security=True")
				.EnableSensitiveDataLogging(true)
				.UseLoggerFactory(MyLoggerFactory);
		}

		public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
		{
			builder
			   .AddFilter((category, level) =>
				   category == DbLoggerCategory.Database.Command.Name
				   && level == LogLevel.Information).AddConsole();
		});

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var student1 = new Student() { Id = 25, Name = "Juan Carlos", DateBirth = new DateTime(1999, 2, 3), ItsErased = false };
			modelBuilder.Entity<Student>().HasData(new Student[] { student1 });
			modelBuilder.Entity<Student>().Property(x => x.Name).HasField("_name");
			modelBuilder.Entity<Student>().HasQueryFilter(x => x.ItsErased == false);
			base.OnModelCreating(modelBuilder);
		}
		
		public DbSet<Student> Students { get; set; }
		
	}
}
