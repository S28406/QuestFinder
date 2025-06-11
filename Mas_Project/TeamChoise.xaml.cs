using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Mas_Project.Models;
using Mas_Project.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Mas_Project
{
    public partial class TeamChoise : Page
    {
        private readonly Guid _questId;
        private readonly QuestService _questService;
        private readonly GuildMemberService _guildMemberService;
        private readonly TeamService _teamService;

        public TeamChoise(Guid questId)
        {
            InitializeComponent();

            _questId = questId;
            _questService = App.ServiceProvider.GetRequiredService<QuestService>();
            _guildMemberService = App.ServiceProvider.GetRequiredService<GuildMemberService>();
            _teamService = App.ServiceProvider.GetRequiredService<TeamService>();
        }

        private async void GoAlone_Click(object sender, RoutedEventArgs e)
        {
            // Simulate assigning the currently logged-in user (stubbed here)
            Guid currentUserId = _guildMemberService.GetTestUser();
            var quest = await _questService.GetQuestByIdAsync(_questId);

            if (quest == null)
            {
                MessageBox.Show("Quest not found.");
                return;
            }

            try
            {
                await _guildMemberService.AssignQuestToMemberAsync(currentUserId, quest);
                MessageBox.Show("You have successfully taken the quest solo.");
                NavigationService?.Navigate(new QuestList(quest.QuestBoardId, "Back to Board"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to take the quest: {ex.Message}");
                NavigationService?.Navigate(new QuestList(quest.QuestBoardId, "Back to Board"));
            }
        }

        private async void GoTogether_Click(object sender, RoutedEventArgs e)
        {
            Guid currentUserId = _guildMemberService.GetTestUser();
            var quest = await _questService.GetQuestByIdAsync(_questId);

            if (quest == null)
            {
                MessageBox.Show("Quest not found.");
                return;
            }

            var member =  _guildMemberService.GetMemberDTOs().Result
                .ToList().FirstOrDefault(m => m.Id == currentUserId);
            
            if (member == null)
            {
                MessageBox.Show("User not found.");
                return;
            }
            
            Console.WriteLine($"[DEBUG] member.TeamGuid = {member.TeamGuid}");

            // Check team membership
            if (member.TeamGuid == Guid.Empty)
            {
                MessageBox.Show("User not in a team.");
                return;
            }

            var team = await _teamService.GetTeamAsync(member.TeamGuid);
            if (team == null || team.Members == null || team.Members.Count == 0)
            {
                MessageBox.Show("Team not found or has no members.");
                return;
            }

            // Check if the team meets the requirements
            if (team.Members.Count < quest.MaxNumberOfParticipants)
            {
                MessageBox.Show("Team does not meet the required number of participants.");
                return;
            }

            if (team.Rank < quest.MinRank)
            {
                MessageBox.Show("The team doesnt meet the rank requirement.");
                return;
            }

            try
            {
                foreach (var teamMember in team.Members)
                {
                    await _guildMemberService.AssignQuestToMemberAsync(teamMember.UserID, quest);
                }

                MessageBox.Show("The quest has been assigned to your team!");
                NavigationService?.Navigate(new QuestList(quest.QuestBoardId, "Back to Board"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to assign quest to team: {ex.Message}");
            }
        }
    }
}