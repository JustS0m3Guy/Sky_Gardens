namespace SkyGarden;

public class Badge
{
    public string? Name { get; private set; }
    private List<Badge>? badges;

    public Badge(string name)
    {
        Name = name;
    }
    public Badge()
    {
        Initialization();
    }
    private void Initialization()
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
        return new List<Badge>(badges);
    }
}

public class Quest
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public List<Item>? RequiredItems { get; private set; }
    public Badge? Reward { get; }
    public bool IsCompleted { get; set; } = false;

    public bool CanComplete(List<Item> inventory)
    {
        return RequiredItems.All(item => inventory.Contains(item));
    }

    public void CompleteQuest(List<Item> inventory)
    {
        if (CanComplete(inventory))
        {
            IsCompleted = true;
            foreach (var item in RequiredItems)
            {
                inventory.Remove(item);
            }
            Console.WriteLine("You have all the required items to complete the quest.");
        }
        else
        {
            Console.WriteLine($"You do not have all the required items to complete the quest '{Title}'.");
        }
    }

    public Quest(string title, string description, List<Item>? requireditems, Badge? reward)
    {
        Title = title;
        Description = description;
        RequiredItems = requireditems;
        Reward = reward;
    }
}