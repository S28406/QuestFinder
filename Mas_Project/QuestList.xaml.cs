using System.Windows;
using System.Windows.Controls;
using Mas_Project.Enums;
using Mas_Project.Models;
using Mas_Project.Services;
using Mas_Project.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Mas_Project;

public partial class QuestList : Page
{
    private readonly QuestService _questService;
    private readonly Guid _questBoardId;
    private readonly string _boardName;
    private List<Quest> _allBoardQuests = new();

    public QuestList(Guid questBoardId, string boardName)
    {
        InitializeComponent(); // this needs to be inside an instance constructor

        _questService = App.ServiceProvider.GetRequiredService<QuestService>();
        _questBoardId = questBoardId;
        _boardName = boardName;

        Loaded += QuestList_Loaded;
    }
    private async void QuestList_Loaded(object sender, RoutedEventArgs e)
    {
        PageTitle.Text = $"Available Quests - {_boardName}";

        var allQuests = await _questService.GetAllAsync();
        _allBoardQuests = allQuests.Where(q => q.QuestBoardId == _questBoardId).ToList();

        if (_allBoardQuests.Count == 0)
            Console.WriteLine($"[DEBUG] No quests found for board {_questBoardId}");

        QuestItemsControl.ItemsSource = _allBoardQuests;
    }
    private void ViewQuest_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Guid questId)
        {
            NavigationService?.Navigate(new QuestDescription(questId));
        }
    }
    
    private void RankFilter_Click(object sender, RoutedEventArgs e)
    {
        // Toggle ComboBox visibility
        RankComboBox.Visibility = RankComboBox.Visibility == Visibility.Visible
            ? Visibility.Collapsed
            : Visibility.Visible;
    }

    private void RankComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (RankComboBox.SelectedItem is ComboBoxItem selectedItem
            && int.TryParse(selectedItem.Content.ToString(), out int selectedRank))
        {
            var filtered = _allBoardQuests.Where(q => q.MinRank <= selectedRank).ToList();
            QuestItemsControl.ItemsSource = filtered;
        }
    }
    
    private void CategoryFilter_Click(object sender, RoutedEventArgs e)
    {
        CategoryComboBox.Visibility = CategoryComboBox.Visibility == Visibility.Visible
            ? Visibility.Collapsed
            : Visibility.Visible;
    }
    
    private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CategoryComboBox.SelectedItem is ComboBoxItem selectedItem &&
            Enum.TryParse<QuestType>(selectedItem.Content.ToString(), out var selectedType))
        {
            var filtered = _allBoardQuests.Where(q => q.Type == selectedType).ToList();
            QuestItemsControl.ItemsSource = filtered;
        }
    }
    
    private void ResetFilters_Click(object sender, RoutedEventArgs e)
    {
        RankComboBox.SelectedIndex = -1;
        CategoryComboBox.SelectedIndex = -1;
        RankComboBox.Visibility = Visibility.Collapsed;
        CategoryComboBox.Visibility = Visibility.Collapsed;
        QuestItemsControl.ItemsSource = _allBoardQuests;
    }
    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (NavigationService?.CanGoBack == true)
            NavigationService.GoBack();
    }
}