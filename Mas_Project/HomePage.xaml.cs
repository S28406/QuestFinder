using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mas_Project;

public partial class HomePage : Page
{
    public HomePage()
    {
        InitializeComponent();
    }

    private void Eldoria_Click(object sender, MouseButtonEventArgs e)
    {
        var mainWindow = Window.GetWindow(this) as MainWindow;
        mainWindow.MainFrame.Navigate(new QuestList());
    }
}