using Microsoft.EntityFrameworkCore;
using lab3.Models;
using lab3.Migrations;

namespace lab3.Models
{
	public class ITIContext:DbContext
	{
		public ITIContext(DbContextOptions options) : base(options)
		{

		}
		public ITIContext()
		{

		}

		public virtual DbSet<Student> Students { get; set; }
		public virtual DbSet<Department> Departments { get; set; }
		public virtual DbSet<StudentCourses> StudentCourses { get; set; }
		public virtual DbSet<User> User { get; set; }
		public virtual DbSet<Role> Role { get; set; }
        public DbSet<lab3.Models.Courses> Courses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-5VF3BKB;Database=_alex2;Trusted_Connection=True;encrypt=false");
			base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourses>()
            .HasKey(x => new { x.StudentId, x.CrsId });
            


            modelBuilder.Entity<Courses>()
                .HasKey(a => a.Crs_Id);

            modelBuilder.Entity<Courses>()
                .Property(a => a.Crs_Name)
                .IsRequired()
                .HasMaxLength(20);

            base.OnModelCreating(modelBuilder);
        }
        
    }
}
