namespace Mas_Project.Models;


public class Mount
{
    public Guid MountID { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }

    public Mount(Guid mountId, string name, string type)
    {
        MountID = mountId;
        Name = name;
        Type = type;
    }
}