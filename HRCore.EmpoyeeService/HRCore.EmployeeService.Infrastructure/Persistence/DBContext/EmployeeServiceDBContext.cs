using HRCore.EmployeeService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRCore.EmployeeService.Infrastructure.Persistence.DBContext;

public class EmployeeServiceDBContext(DbContextOptions<EmployeeServiceDBContext> options) : DbContext(options)
{
    public DbSet<Employee> Employee { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("emp");
        base.OnModelCreating(modelBuilder);
    }
}
