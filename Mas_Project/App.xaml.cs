using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Mas_Project.Data;
using Mas_Project.Services;
using Mas_Project.Data.Repositories;
using Mas_Project.Data.Repositories.Interfaces;
using Mas_Project.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Mas_Project
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            var services = new ServiceCollection();

            // Replace with your actual connection string if needed
            services.AddDbContext<DBContext>(options =>
                options.UseSqlite("Data Source=mas_project.db")
                // Uncomment for queries
                    // .LogTo(Console.WriteLine, LogLevel.Information)
                    // .EnableSensitiveDataLogging()
                );

            // Register all services
            services.AddScoped<GuildMemberService>();
            services.AddScoped<QuestService>();
            services.AddScoped<QuestBoardService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<TeamService>();

            // Register all repositories
            services.AddScoped<IGuildMemberRepository, GuildMemberRepository>();
            services.AddScoped<IQuestRepository, QuestRepository>();
            services.AddScoped<IQuestBoardRepository, QuestBoardRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            
            services.AddScoped<QuestBoardViewModel>();

            ServiceProvider = services.BuildServiceProvider();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var context = ServiceProvider.GetRequiredService<DBContext>();
            var teamService = ServiceProvider.GetRequiredService<TeamService>();
            var guildMemberService = ServiceProvider.GetRequiredService<GuildMemberService>();
            var questService = ServiceProvider.GetRequiredService<QuestService>();
            var questBoardService = ServiceProvider.GetRequiredService<QuestBoardService>();

            await DataSeeder.SeedAsync(context, teamService, guildMemberService, questService, questBoardService);
        }
    }
}