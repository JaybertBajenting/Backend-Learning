using Microsoft.EntityFrameworkCore;

using Backend_Learning.Models;
namespace Backend_Learning.Data
{
    public class StudentContext: DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;  
        

        public StudentContext(DbContextOptions<StudentContext> options): base(options)
        {

        }

    }
}
