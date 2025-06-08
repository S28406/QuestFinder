using Mas_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Mas_Project.Data;

public class DBContext: DbContext
{
    public DbSet<GuildMember> GuildMembers { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Quest> Quests { get; set; }
    public DbSet<QuestBoard> QuestBoards { get; set; }
    public DbSet<Customer> Customers { get; set; }
    // Add others...

    public DBContext(DbContextOptions<DBContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent config (optional)
    }
}