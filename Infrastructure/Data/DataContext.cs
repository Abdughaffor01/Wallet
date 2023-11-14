using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> opt) : base(opt)=>Database.EnsureCreated();
    
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Card> Cards  { get; set; }


    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
