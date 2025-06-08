using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Mas_Project.Models;

public class QuestBoard
{
    [Key]
    [Required]
    public Guid QuestBoardID { get; set; }
    [Required]
    [MinLength(5)]
    public string Location { get; set; }

    public string ImageUrl { get; set; } = "";

    public ICollection<Quest> Quests { get; set; } = new List<Quest>();
    public QuestBoard(){}
    public QuestBoard(Guid questBoardId, string location)
    {
        QuestBoardID = questBoardId;
        Location = location;
    }

    public List<Quest> GetAllQuests() => Quests as List<Quest>;

    public List<Quest> FilterQuestsByRank(int rank)
    {
        return Quests.Where(q => q.MinRank == rank).ToList();
    }

    public void AddQuest(Quest quest)
    {
        Quests.Add(quest);
    }
}