using System.Windows;
using System.Windows.Controls;
using Mas_Project.Services;
using Mas_Project.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Mas_Project;

// public partial class QuestList : Page
// {
//     // public QuestList()
//     // {
//     //     InitializeComponent();
//     //     DataContext = App.ServiceProvider.GetRequiredService<QuestBoardViewModel>();
//     // }
//     private readonly QuestService _questService;
//     private readonly Guid _questBoardId;
//
//     public QuestList(Guid questBoardId)
//     {
//         InitializeComponent();
//         _questBoardId = questBoardId;
//         _questService = App.ServiceProvider.GetRequiredService<QuestService>();
//
//         Loaded += QuestList_Loaded;
//     }
//
//     private async void QuestList_Loaded(object sender, RoutedEventArgs e)
//     {
//         var allQuests = await _questService.GetAllAsync();
//         var questsForBoard = allQuests.Where(q => q.QuestBoardId == _questBoardId).ToList();
//
//         if (questsForBoard.Count == 0)
//             Console.WriteLine($"[DEBUG] No quests found for board {_questBoardId}");
//
//         QuestItemsControl.ItemsSource = questsForBoard;
//     }
// }

public partial class QuestList : Page
{
    private readonly QuestService _questService;
    private readonly Guid _questBoardId;
    private readonly string _boardName;

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
        var boardQuests = allQuests.Where(q => q.QuestBoardId == _questBoardId).ToList();

        if (boardQuests.Count == 0)
            Console.WriteLine($"[DEBUG] No quests found for board {_questBoardId}");

        QuestItemsControl.ItemsSource = boardQuests;
    }
    private void ViewQuest_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Guid questId)
        {
            NavigationService?.Navigate(new QuestDescription(questId));
        }
    }
}