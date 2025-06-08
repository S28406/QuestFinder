using System.Windows.Controls;
using Mas_Project.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Mas_Project;

public partial class QuestList : Page
{
    public QuestList()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<QuestBoardViewModel>();
    }
}