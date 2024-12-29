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
        public int QuestProgress { get; set; }
        public int QuestLength { get; set; }

        public bool CanComplete(List<Item> inventory)
        {
            return RequiredItems != null && RequiredItems.All(item => inventory.Contains(item));
        }

        public Quest(string title, string description, List<Item>? requireditems, Badge? reward, int questLength)
        {
            Title = title;
            Description = description;
            RequiredItems = requireditems;
            Reward = reward;
            QuestProgress = 0;
            QuestLength = questLength;
        }
    }
}