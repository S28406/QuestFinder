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

            // Resolve services from the DI container
            var questBoardService = App.ServiceProvider.GetRequiredService<QuestBoardService>();
            var teamService = App.ServiceProvider.GetRequiredService<TeamService>();
            var guildMemberService = App.ServiceProvider.GetRequiredService<GuildMemberService>();
            var questService = App.ServiceProvider.GetRequiredService<QuestService>();
            var customerService = App.ServiceProvider.GetRequiredService<CustomerService>();
            var dbContext = App.ServiceProvider.GetRequiredService<Data.DBContext>();

            // Pass the necessary services to your main ViewModel
            var viewModel = new QuestBoardViewModel(
                questBoardService,
                teamService,
                guildMemberService,
                questService,
                customerService,
                dbContext // optional, if your ViewModel needs it
            );

            DataContext = viewModel;
        }
    }
}