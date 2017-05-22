using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SQLite.CodeFirst.Convention;
using SQLite.CodeFirst.Extensions;
using SQLite.CodeFirst.Utility;
using SQLite.CodeFirst;
namespace OwnersPets.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext()
			: base("DefaultConnection")
		{
			Database.SetInitializer<AppDbContext>(new CreateDatabaseIfNotExists<AppDbContext>());
		}

		public DbSet<Owner> Owners { get; set; }
		public DbSet<Pet> Pets { get; set; }

		public static AppDbContext Create()
		{
			return new AppDbContext();
		}

		//protected override void OnModelCreating(DbModelBuilder modelBuilder)
		//{
		//	var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<AppDbContext>(modelBuilder);
		//	Database.SetInitializer(sqliteConnectionInitializer);
		//}

	}
}