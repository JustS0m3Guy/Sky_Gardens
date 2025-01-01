namespace SkyGarden
{
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
            return badges != null ? new List<Badge>(badges) : new List<Badge>();
        }
    }

    public class Quest
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public List<Item>? RequiredItems { get; private set; }
        public Badge? Reward { get; }
        public bool IsCompleted { get; set; } = false;
        public List<Room>? Places = new();
        public int QuestProgress { get; set; }
        public int QuestLength { get; set; }

        public bool CanComplete(List<Item> inventory)
        {
            return RequiredItems != null && RequiredItems.All(item => inventory.Contains(item));
        }

        public Quest(string title, string description, List<Item>? requireditems, List<Room> places, Badge? reward, int questLength)
        {
            Title = title;
            Description = description;
            RequiredItems = requireditems;
            Reward = reward;
            Places = places;
            QuestProgress = 0;
            QuestLength = questLength;
        }
        public void DisplayQuestInfo()
        {
            if (this != null)
            {
                Console.WriteLine($"Current Quest: {this.Title}");
                Console.WriteLine($"Description: {this.Description}");
            }
            else
            {
                Console.WriteLine("There is no active quest.");
            }
        }
    }
}