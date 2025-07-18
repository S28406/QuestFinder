﻿using Mas_Project.Enums;
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
        if (!context.GuildMembers.Any())
        {
            
            var memberForTesting = new Warrior(
                username: "AAATestMember",
                email: "TestMember",
                rank: 4,
                experiencePoints: 9999,
                memberRole: [MemberRole.RegularMember],
                armorClass: 5
            );
            var memberForTestingTheTeam = new Mage(
                username: "BBBTestMemberTeam",
                email: "TestMember",
                rank: 3,
                experiencePoints: 9999,
                memberRole: [MemberRole.RegularMember],
                mp: 250
            );
            
            
            var member1 = new Priest(
                username: "Caoimhe",
                email: "auron@guild.com",
                rank: 2,
                experiencePoints: 100,
                memberRole: [MemberRole.RegularMember],
                divinePower: 100
            );

            var member2 = new Warrior(
                username: "Siobhan",
                email: "lina@guild.com",
                rank: 2,
                experiencePoints: 200,
                memberRole: [MemberRole.RegularMember],
                armorClass: 100
            );
            

            var manager = new Warrior(
                username: "Niamh Manager",
                email: "august@guild.com",
                rank: 5,
                experiencePoints: 500,
                memberRole: [MemberRole.GuildManager],
                armorClass: 100,
                assignedRoom: 15
                
            );

            var customer = new Customer(
                username: "Seamus Customer",
                email: "august@guild.com",
                reputationScore: 100,
                registrationDate: DateTime.Now
            );

            await context.GuildMembers.AddRangeAsync(memberForTesting, memberForTestingTheTeam, member1, member2, manager);
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();

            var team = await teamService.CreateTeamAsync(2, manager.UserID);
            var testteam = await teamService.CreateTeamAsync(3, manager.UserID);
            await teamService.AddMemberToTeamAsync(team.TeamID, manager.UserID, member2.UserID);
            await teamService.AddMemberToTeamAsync(team.TeamID, manager.UserID, member1.UserID);
            await teamService.AddMemberToTeamAsync(testteam.TeamID, manager.UserID, memberForTesting.UserID);
            await teamService.AddMemberToTeamAsync(testteam.TeamID, manager.UserID, memberForTestingTheTeam.UserID);

            var quest = new Quest(
                questId: Guid.NewGuid(),
                title: "Test Quest",
                description: "A dangerous troll haunts the woods near the village.",
                maxParticipants: 1,
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
                title: "Slay the Forest Troll (High Rank)",
                description: "A dangerous troll haunts the woods near the village.",
                maxParticipants: 2,
                minRank: 5,
                durationHours: 5,
                reward: "100 gold",
                priority: 3,
                type: QuestType.SlayTheMonsters,
                requirements: "No Additional Requirments",
                status: QuestStatus.Created
            );
            var quest3 = new Quest(
                questId: Guid.NewGuid(),
                title: "Slay the Forest Troll (Full)",
                description: "A dangerous troll haunts the woods near the village.",
                maxParticipants: 2,
                minRank: 1,
                durationHours: 5,
                reward: "100 gold",
                priority: 1,
                type: QuestType.SlayTheMonsters,
                requirements: "No Additional Requirments",
                status: QuestStatus.Full
            );
            var quest4 = new Quest(
                questId: Guid.NewGuid(),
                title: "Fetch some feathers",
                description: "Fetch the feathers from the fenix",
                maxParticipants: 1,
                minRank: 4,
                durationHours: 5,
                reward: "1000 gold",
                priority: 7,
                type: QuestType.FetchTheTreasure,
                requirements: "No Additional Requirments",
                status: QuestStatus.Created
            );
            
            var board = await questBoardService.AddBoardAsync("Town Center", "Eldoria Forest");
            var board2 = await questBoardService.AddBoardAsync("East Plains", "Ironpeak Mountains");
            Console.WriteLine($"[SEEDING] QuestBoardID: {board.QuestBoardID}");
            await questService.CreateQuestAsync(board.QuestBoardID, customer.UserID, quest);
            await questService.CreateQuestAsync(board.QuestBoardID, customer.UserID, quest2);
            await questService.CreateQuestAsync(board.QuestBoardID, customer.UserID, quest3);
            await questService.CreateQuestAsync(board.QuestBoardID, customer.UserID, quest4);
            
            await guildMemberService.AssignQuestToMemberAsync(member1.UserID, quest);
            await guildMemberService.AssignQuestToMemberAsync(member2.UserID, quest);
    

            await context.SaveChangesAsync();
        }
    }
}
