using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Mod4
{
    class ApplicationDbContext : DbContext
    {
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=DESKTOP-75L2R7E\\SQLEXPRESS;Initial Catalog=EFCore_Mod4;Integrated Security=True")
				.EnableSensitiveDataLogging(true)
				//.UseLazyLoadingProxies()
				.UseLoggerFactory(MyLoggerFactory);
		}
		public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
		{
			builder
			   .AddFilter((category, level) =>
				   category == DbLoggerCategory.Database.Command.Name
				   && level == LogLevel.Information)
			   .AddConsole();
		});
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var student1 = new Student() { Id = 1, Name = "Juan Carlos", DateBirth = new DateTime(1999, 2, 3) };
			var student2 = new Student() { Id = 2, Name = "Pepe Juan", DateBirth = new DateTime(1999, 2, 3)};
			modelBuilder.Entity<Student>().HasData(new Student[] { student1, student2 });
			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Student> Students { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<StudentDetail> StudentDetails { get; set; }
		
	}
}
