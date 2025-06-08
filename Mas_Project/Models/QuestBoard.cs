using System;
using System.Collections.Generic;
using System.Linq;

namespace Mas_Project.Models;

public class QuestBoard
{
    public Guid QuestBoardID { get; set; }
    public string Location { get; set; }

    public List<Quest> Quests { get; set; } = new();

    public QuestBoard(Guid questBoardId, string location)
    {
        QuestBoardID = questBoardId;
        Location = location;
    }

    public List<Quest> GetAllQuests() => Quests;

    public List<Quest> FilterQuestsByRank(int rank)
    {
        return Quests.Where(q => q.MinRank == rank).ToList();
    }

    public void AddQuest(Quest quest)
    {
        Quests.Add(quest);
    }
}