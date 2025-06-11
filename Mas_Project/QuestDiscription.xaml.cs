using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Mas_Project.Models;
using Mas_Project.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Mas_Project
{
    public partial class QuestDescription : Page
    {
        private readonly QuestService _questService;
        private readonly Guid _questId;

        public QuestDescription(Guid questId)
        {
            InitializeComponent();
            _questService = App.ServiceProvider.GetRequiredService<QuestService>();
            _questId = questId;

            Loaded += QuestDescription_Loaded;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService?.CanGoBack == true)
                NavigationService.GoBack();
        }
        private async void QuestDescription_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var quest = await _questService.GetByIdAsync(_questId);

            if (quest == null)
            {
                TitleText.Text = "Quest not found.";
                return;
            }

            TitleText.Text = quest.Title;
            DescriptionText.Text = quest.Description;
            DurationText.Text = $"Estimated Duration: {quest.DurationHours} hours";
            RewardText.Text = $"Reward: {quest.Reward}";
            RequirementsText.Text = $"Requirements: {quest.Requirements}";
            RankText.Text = $"Minimum Rank: {quest.MinRank}";

            var participants = quest.Participants;
            ParticipantsList.ItemsSource = participants;
        }
        private void TakeQuest_Click(object sender, RoutedEventArgs e)
        {
                NavigationService?.Navigate(new TeamChoise(_questId));
                Console.WriteLine("[DEBUG] TakeQuest Triggered");
        }
    }
}