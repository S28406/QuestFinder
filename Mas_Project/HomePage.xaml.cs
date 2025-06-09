using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Mas_Project.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Mas_Project;

public partial class HomePage : Page
{
    private readonly QuestBoardViewModel _viewModel;
    public HomePage()
    {
        InitializeComponent();

        // Get ViewModel from DI container
        _viewModel = App.ServiceProvider.GetRequiredService<QuestBoardViewModel>();
        DataContext = _viewModel;

        // Load data
        _viewModel.LoadBoardsAsync();
    }

    private void Board_Click(object sender, MouseButtonEventArgs e)
    {
        var mainWindow = Window.GetWindow(this) as MainWindow;
        mainWindow.MainFrame.Navigate(new QuestList());
    }
}