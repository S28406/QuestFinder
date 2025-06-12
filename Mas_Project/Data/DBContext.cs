using Mas_Project.Enums;
using Mas_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Mas_Project.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        // === DbSet declarations ===
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DateTaken> DatesTaken { get; set; }
        public DbSet<DateUnlocked> DatesUnlocked { get; set; }
        public DbSet<GuildMember> GuildMembers { get; set; }
        public DbSet<Mage> Mages { get; set; }
        public DbSet<Mount> Mounts { get; set; }
        public DbSet<Priest> Priests { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<QuestBoard> QuestBoards { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Warrior> Warriors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<GuildMember>().ToTable("GuildMembers");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Quest>()
                .HasOne(q => q.QuestBoard)
                .WithMany(qb => qb.Quests)
                .HasForeignKey(q => q.QuestBoardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Quest>()
                .HasIndex(q => new { q.QuestBoardId, q.Priority })
                .IsUnique();

            modelBuilder.Entity<GuildMember>().Property(gm => gm.MemberRoles)
                .HasConversion(v => string.Join(",", v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(Enum.Parse<MemberRole>).ToList());
            

            modelBuilder.Entity<Team>()
                .HasMany(t => t.Members)
                .WithMany();
            
            modelBuilder.Entity<GuildMember>()
                .HasMany(gm => gm.Mounts)
                .WithOne(m => m.GuildMember)
                .HasForeignKey(m => m.GuildMemberId);
            
            modelBuilder.Entity<DateUnlocked>()
                .HasOne(du => du.GuildMember)
                .WithMany(gm => gm.DatesUnlocked)
                .HasForeignKey(du => du.GuildMemberId);

            modelBuilder.Entity<DateUnlocked>()
                .HasOne(du => du.Achievement)
                .WithMany(a => a.DatesUnlocked)
                .HasForeignKey(du => du.AchievementId);
            
            modelBuilder.Entity<DateTaken>()
                .HasOne(dt => dt.GuildMember)
                .WithMany(gm => gm.DatesTaken)
                .HasForeignKey(dt => dt.GuildMemberId);

            modelBuilder.Entity<DateTaken>()
                .HasOne(dt => dt.Quest)
                .WithMany(q => q.DateTakens)
                .HasForeignKey(dt => dt.QuestId);
            

            modelBuilder.Entity<User>().HasKey(u => u.UserID);
            modelBuilder.Entity<Team>().HasKey(t => t.TeamID);
            modelBuilder.Entity<Quest>().HasKey(q => q.QuestID);
            modelBuilder.Entity<QuestBoard>().HasKey(qb => qb.QuestBoardID);
            modelBuilder.Entity<Achievement>().HasKey(a => a.AchievementID);
            modelBuilder.Entity<Mount>().HasKey(m => m.MountID);
            modelBuilder.Entity<DateTaken>().HasKey(dt => new {dt.GuildMemberId, dt.QuestId});
            modelBuilder.Entity<DateUnlocked>().HasKey(dt => new {dt.GuildMemberId, dt.AchievementId});

        }
    }
}
