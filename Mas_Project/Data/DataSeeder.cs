using Mas_Project.Enums;
using Mas_Project.Models;
using Mas_Project.Services;
using Microsoft.EntityFrameworkCore;

namespace Mas_Project.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(
        DBContext context,
        TeamService teamService,
        GuildMemberService guildMemberService,
        QuestService questService,
        QuestBoardService questBoardService
    )
    {
        // Ensure the database and tables exist
        await context.Database.MigrateAsync();

        // Seed only if empty
        if (!context.GuildMembers.Any())
        {
            // Create sample members
            var member1 = new GuildMember(
                username: "Aaron",
                userId: Guid.NewGuid(),
                email: "auron@guild.com",
                rank: 1,
                experiencePoints: 100,
                memberRole: MemberRole.RegularMember
            );

            var member2 = new GuildMember(
                username: "Lina",
                userId: Guid.NewGuid(),
                email: "lina@guild.com",
                rank: 2,
                experiencePoints: 200,
                memberRole: MemberRole.RegularMember
            );

            var manager = new GuildMember(
                username: "August",
                userId: Guid.NewGuid(),
                email: "august@guild.com",
                rank: 5,
                experiencePoints: 500,
                memberRole: MemberRole.GuildManager
            );

            var customer = new Customer(
                username: "August",
                userId: Guid.NewGuid(),
                email: "august@guild.com",
                reputationScore: 100,
                registrationDate: DateTime.Now
            );

            // Save members to get valid references
            await context.GuildMembers.AddRangeAsync(member1, member2, manager);
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();

            // Create a team using the manager (valid by domain rules)
            var team = await teamService.CreateTeamAsync(2, manager.UserID);
            await teamService.AddMemberToTeamAsync(team.TeamID, manager.UserID, member2.UserID);
            await teamService.AddMemberToTeamAsync(team.TeamID, manager.UserID, member1.UserID);

            // Create a quest
            var quest = new Quest(
                questId: Guid.NewGuid(),
                title: "Slay the Forest Troll",
                description: "A dangerous troll haunts the woods near the village.",
                minParticipants: 2,
                minRank: 1,
                durationHours: 5,
                reward: "100 gold",
                priority: 2,
                type: QuestType.SlayTheMonsters,
                requirements: "No Additional Requirments",
                status: QuestStatus.Created
            );
    
            await questService.AddQuestAsync(quest, customer.UserID);

            // Create quest board and attach quest
            var board = new QuestBoard(Guid.NewGuid(), "Town Center");
            Console.WriteLine($"[SEEDING] QuestBoardID: {board.QuestBoardID}");
            board.AddQuest(quest);
            await questBoardService.AddQuestToBoardAsync(board.QuestBoardID, quest);
            await context.SaveChangesAsync();
        }
    }
}
