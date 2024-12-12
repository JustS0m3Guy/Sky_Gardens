namespace SkyGarden
{
    public class Neighbour
    {
        public string Name { get; private set; }
        public Quest Quest { get; private set; }
    
        public Neighbour(string name, Quest quest)
        {
            Name = name;
            Quest = quest;
        }

        private List<Neighbour> neighbours;

        public Neighbour()
        {
            neighbours = new List<Neighbour>();
        }

        public void AddNeighbour(string name, Quest quest)
        {
            Neighbour neighbour = new Neighbour(name, quest);
            neighbours.Add(neighbour);
        }

        public void ShowNeighbours()
        {
            Console.WriteLine("Neighbours and their quests:");
            foreach (var neighbour in neighbours)
            {
                Console.WriteLine($"Neighbour: {neighbour.Name}, Quest: {neighbour.Quest.Title} - {neighbour.Quest.Description}");
            }
        }
    }
}