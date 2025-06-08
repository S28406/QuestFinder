using Mas_Project.Models;
using Mas_Project.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Mas_Project.Data;

namespace Mas_Project.ViewModels;

public class QuestBoardViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Quest> Quests { get; set; } = new();
    public int FilterRank { get; set; } = 0;

    public ICommand LoadQuestsCommand { get; }
    public ICommand FilterCommand { get; }
    
    private readonly QuestBoardService _questBoardService;
    private readonly TeamService _teamService;
    private readonly GuildMemberService _guildMemberService;
    private readonly QuestService _questService;
    private readonly CustomerService _customerService;
    private readonly DBContext _context;
    public QuestBoardViewModel(
        QuestBoardService questBoardService,
        TeamService teamService,
        GuildMemberService guildMemberService,
        QuestService questService,
        CustomerService customerService,
        DBContext context)
    {
        _questBoardService = questBoardService;
        _teamService = teamService;
        _guildMemberService = guildMemberService;
        _questService = questService;
        _customerService = customerService;
        _context = context;
    }
    public QuestBoardViewModel(QuestBoardService questBoardService)
    {
        _questBoardService = questBoardService;

        LoadQuestsCommand = new RelayCommand(async _ => await LoadQuestsAsync());
        FilterCommand = new RelayCommand(async _ => await FilterQuestsAsync());
    }

    public async Task LoadQuestsAsync()
    {
        var questList = await _questBoardService.GetAllQuestsAsync(/* boardId */ Guid.Parse("YOUR_BOARD_ID_HERE"));
        Quests.Clear();
        foreach (var quest in questList)
            Quests.Add(quest);
    }

    public async Task FilterQuestsAsync()
    {
        var filtered = await _questBoardService.FilterQuestsByRankAsync(Guid.Parse("YOUR_BOARD_ID_HERE"), FilterRank);
        Quests.Clear();
        foreach (var quest in filtered)
            Quests.Add(quest);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}