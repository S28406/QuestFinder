using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Mas_Project.Models;
using Mas_Project.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Mas_Project;

public partial class HomePage : Page
{
    private readonly QuestBoardViewModel _viewModel;
    public HomePage()
    {
        InitializeComponent();

        _viewModel = App.ServiceProvider.GetRequiredService<QuestBoardViewModel>();
        DataContext = _viewModel;

        _viewModel.LoadBoardsAsync();
    }

    private void Board_Click(object sender, MouseButtonEventArgs e)
    {
        var stackPanel = (StackPanel)sender;
        var board = (QuestBoard)stackPanel.DataContext;

        var mainWindow = Window.GetWindow(this) as MainWindow;
        mainWindow.MainFrame.Navigate(new QuestList(board.QuestBoardID, board.Name));
    }
}