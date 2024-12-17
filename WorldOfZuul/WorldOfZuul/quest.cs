namespace SkyGarden;

public class Badge
{
    public string Name { get; private set; } = string.Empty;

    public Badge(string name)
    {
        Name = name;
    }

    private List<Badge> badges;

    public Badge()
    {
        badges = new List<Badge>
        {
            new("Eco-Enthusiast"),
            new("A Recycled Memory"),
            new("Pollution Pro"),
            new("Farmer"),
            new("Renewable Energy Pioneer"),
            new("Plumbing Captain"),
            new("Social Connectivity"),
            new("Biodiversity"),
            new("Silence!")
        };
    }

    public List<Badge> GetBadges()
    {
        return badges;
    }
}

public class Quest
{
    public string Title { get; private set; }
    public Neighbour Name { get; private set; }
    public string Description { get; private set; }
    public List<Item> RequiredItems { get; private set; }
    public Badge Reward { get; }
    public bool IsCompleted { get; set; } = false;

    public bool CanComplete(List<Item> inventory)
    {
        return RequiredItems.All(item => inventory.Contains(item));
    }

    public Quest(string title, Neighbour name, string description, List<Item> requireditems, Badge reward)
    {
        Title = title;
        Name = name;
        Description = description;
        RequiredItems = requireditems;
        Reward = reward;
    }
}