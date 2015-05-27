namespace NetZkouskaFull.DAL
{
	using NetZkouskaFull.Models;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;
	using System.Linq;

	public class NetContext : DbContext
	{
		
		public NetContext()	: base("name=NetContext"){}

		public DbSet<Student> Students { get; set; }
		public DbSet<Exam> Exams { get; set; }
		public DbSet<Work> Works { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}