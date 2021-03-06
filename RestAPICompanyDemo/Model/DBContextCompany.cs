using Microsoft.EntityFrameworkCore;

namespace RestAPICompanyDemo.Model
{
    public class DBContextCompany : DbContext
    {
        public DBContextCompany(DbContextOptions<DBContextCompany> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne<User>(u => u.User)
                .WithOne(e => e.Employee)
                .HasForeignKey<User>(u => u.CurentEmployeeId);

            modelBuilder.Entity<Image>()
                .HasOne<Employee>(u => u.Employee)
                .WithOne(e => e.Image)
                .HasForeignKey<Employee>(u => u.ImageId);

            modelBuilder.Entity<Employee>()
                .HasOne<Department>(s => s.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.CurentDepartmentId);

        }
        public DbSet<Employee> employees { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Image> images { get; set; }
    }
}
