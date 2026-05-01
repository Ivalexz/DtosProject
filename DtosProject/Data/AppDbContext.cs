using DtosProject.Models;
using Microsoft.EntityFrameworkCore;
namespace DtosProject.Data;

public class AppDbContext : DbContext // вирішила робити через список, тому що не грузило в Azure
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Client> Clients { get; set; }
}