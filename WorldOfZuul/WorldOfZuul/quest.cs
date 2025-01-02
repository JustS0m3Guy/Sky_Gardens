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
        public bool IsCompleted { get; set; } = true;
        public List<Room>? Places = new();
        public int QuestProgress { get; set; }
        public int QuestLength { get; set; }
        //public int ItemRemovalIndex { get; set; }

        public bool CanComplete(List<Item> inventory)
        {
            return RequiredItems != null && RequiredItems.All(item => inventory.Contains(item));
        }

        public Quest(string title, string description, List<Item>? requireditems, List<Room> places, Badge? reward)
        {
            Title = title;
            Description = description;
            RequiredItems = requireditems;
            Reward = reward;
            Places = places;
            QuestProgress = 0;
            QuestLength = places.Count;
            //ItemRemovalIndex = itemRemovalIndex;
        }

        public void DisplayQuestInfo()
        {
            string text = "";
            if (this != null)
            {
                text += $"Current Quest: {this.Title}\n"
                     +  $"Description: {this.Description}\n";

                if (Places != null && Places.Count > 0)
                {
                    Room currentLocation = Places[QuestProgress];
                    text += $"Current Location of NPC: {currentLocation.ShortDescription}\n";
                }
                else
                {
                    text += "Current Location of NPC: Unknown\n";
                }
            }
            else
            {
                text += "There's no active quest at the moment.\n";
            }
            Game.DisplayTextSlowly(text);
        }
        public void Check()
        {
            if (QuestProgress == QuestLength)
            {
                IsCompleted = true;
                Game.DisplayTextSlowly($"{Title} Completed!");
                
            }
        }
    }
}