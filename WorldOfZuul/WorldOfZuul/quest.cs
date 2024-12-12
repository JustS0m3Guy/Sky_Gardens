using SkyGarden;

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
            new Badge("Eco-Enthusiast"),
            new Badge("A Recycled Memory"),
            new Badge("Pollution Pro"),
            new Badge("Farmer"),
            new Badge("Renewable Energy Pioneer"),
            new Badge("Plumbing Captain"),
            new Badge("Social Connectivity"),
            new Badge("Biodiversity"),
            new Badge("Silence!")
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