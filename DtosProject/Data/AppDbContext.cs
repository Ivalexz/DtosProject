using DtosProject.Models;
using Microsoft.EntityFrameworkCore;
namespace DtosProject.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Client> Clients { get; set; }
}