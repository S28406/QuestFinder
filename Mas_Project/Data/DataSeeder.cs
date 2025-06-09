using Mas_Project.Enums;
using Mas_Project.Models;
using Mas_Project.Services;
using Microsoft.EntityFrameworkCore;

namespace Mas_Project.Data;

public static class DataSeeder
{
    public static void Clean(DBContext context)
    {
        context.Customers.RemoveRange(context.Customers);
        context.QuestBoards.RemoveRange(context.QuestBoards);
        context.GuildMembers.RemoveRange(context.GuildMembers);
        context.Quests.RemoveRange(context.Quests);
        context.Achievements.RemoveRange(context.Achievements);
        context.Customers.RemoveRange(context.Customers);
        context.DatesUnlocked.RemoveRange(context.DatesUnlocked);
        context.DatesTaken.RemoveRange(context.DatesTaken);
        context.Mages.RemoveRange(context.Mages);
        context.Mounts.RemoveRange(context.Mounts);
        context.Priests.RemoveRange(context.Priests);
        context.Teams.RemoveRange(context.Teams);
        context.Warriors.RemoveRange(context.Warriors);
        
        context.SaveChanges();
    }
    public static async Task SeedAsync(
        DBContext context,
        TeamService teamService,
        GuildMemberService guildMemberService,
        QuestService questService,
        QuestBoardService questBoardService
    )
    {
        Clean(context);
        // Ensure the database and tables exist
        await context.Database.MigrateAsync();
        // Seed only if empty
        if (!context.GuildMembers.Any())
        {
            
            var memberForTesting = new GuildMember(
                username: "TestMember",
                email: "TestMember",
                rank: 1,
                experiencePoints: 9999,
                memberRole: MemberRole.RegularMember
            );
            
            // Create sample members
            var member1 = new GuildMember(
                username: "Aaron",
                email: "auron@guild.com",
                rank: 2,
                experiencePoints: 100,
                memberRole: MemberRole.RegularMember
            );

            var member2 = new GuildMember(
                username: "Lina",
                email: "lina@guild.com",
                rank: 2,
                experiencePoints: 200,
                memberRole: MemberRole.RegularMember
            );
            

            var manager = new GuildMember(
                username: "August Manager",
                email: "august@guild.com",
                rank: 5,
                experiencePoints: 500,
                memberRole: MemberRole.GuildManager
            );

            var customer = new Customer(
                username: "August Customer",
                email: "august@guild.com",
                reputationScore: 100,
                registrationDate: DateTime.Now
            );

            // Save members to get valid references
            await context.GuildMembers.AddRangeAsync(memberForTesting, member1, member2, manager);
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
                minParticipants: 1,
                minRank: 2,
                durationHours: 5,
                reward: "100 gold",
                priority: 2,
                type: QuestType.SlayTheMonsters,
                requirements: "No Additional Requirments",
                status: QuestStatus.Created
            );
            var quest2 = new Quest(
                questId: Guid.NewGuid(),
                title: "Slay the Forest Troll",
                description: "A dangerous troll haunts the woods near the village.",
                minParticipants: 2,
                minRank: 1,
                durationHours: 5,
                reward: "100 gold",
                priority: 3,
                type: QuestType.SlayTheMonsters,
                requirements: "No Additional Requirments",
                status: QuestStatus.Created
            );
            var quest3 = new Quest(
                questId: Guid.NewGuid(),
                title: "Slay the Forest Troll",
                description: "A dangerous troll haunts the woods near the village.",
                minParticipants: 2,
                minRank: 1,
                durationHours: 5,
                reward: "100 gold",
                priority: 1,
                type: QuestType.SlayTheMonsters,
                requirements: "No Additional Requirments",
                status: QuestStatus.Created
            );
            
            var board = await questBoardService.AddBoardAsync("Town Center", "Eldoria Forest");
            var board2 = await questBoardService.AddBoardAsync("East Plains", "Ironpeak Mountains");
            Console.WriteLine($"[SEEDING] QuestBoardID: {board.QuestBoardID}");
            await questService.CreateAndAssignQuestAsync(board.QuestBoardID, customer.UserID, quest);
            await questService.CreateAndAssignQuestAsync(board.QuestBoardID, customer.UserID, quest2);
            await questService.CreateAndAssignQuestAsync(board.QuestBoardID, customer.UserID, quest3);
            
            await guildMemberService.AssignQuestToMemberAsync(member1.UserID, quest);
            await guildMemberService.AssignQuestToMemberAsync(member2.UserID, quest);
    

            // Create quest board and attach quest
            await context.SaveChangesAsync();
        }
    }
}
