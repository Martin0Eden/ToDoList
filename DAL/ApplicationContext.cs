using Microsoft.EntityFrameworkCore;
using Domain.Entity;

namespace DAL
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<UserProject> UserProject { get; set; }
        public DbSet<Sprint> Sprint { get; set; }
        public DbSet<Domain.Entity.Task> Task { get; set; }
        public DbSet<FileUploadModel> FileUploadModel { get; set; }
        public DbSet<UserTask> UserTask { get; set; }
        public DbSet<Comments> Comments { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=ToDoListt;Integrated Security=True;TrustServerCertificate=True;");
        }
    }
}
