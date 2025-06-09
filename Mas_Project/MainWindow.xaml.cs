using System.Windows;
using Mas_Project.Services;
using Mas_Project.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Mas_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new HomePage());
        }
    }
}