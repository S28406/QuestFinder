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
    public string Name { get; set; }
    [Required]
    [MinLength(5)]
    public string Location { get; set; }

    public string ImageUrl { get; set; } = "Images/EldoriaForest.png";

    public ICollection<Quest> Quests { get; set; } = new List<Quest>();
    public QuestBoard(){}
    public QuestBoard(Guid questBoardId, string location, string name)
    {
        QuestBoardID = questBoardId;
        Location = location;
        Name = name;
    }
}