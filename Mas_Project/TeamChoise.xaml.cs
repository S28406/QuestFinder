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

        public TeamChoise(Guid questId)
        {
            InitializeComponent();

            _questId = questId;
            _questService = App.ServiceProvider.GetRequiredService<QuestService>();
            _guildMemberService = App.ServiceProvider.GetRequiredService<GuildMemberService>();
        }

        private async void GoAlone_Click(object sender, RoutedEventArgs e)
        {
            // Simulate assigning the currently logged-in user (stubbed here)
            Guid currentUserId = _guildMemberService.GetTestUser(); // Ensure you have this available
            var quest = await _questService.GetByIdAsync(_questId);

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
            }
        }

        private void FindTeam_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new HomePage());
        }
    }
}