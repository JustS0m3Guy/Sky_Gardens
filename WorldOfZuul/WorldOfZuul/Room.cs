using System.Diagnostics.Contracts;

namespace SkyGarden
{
    public class Room
    {
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set;}
        public List<Item> Items { get; set; } = new();
        public List<NPC> NPCs { get; set; } = new();
        public Dictionary<string, Room> Exits { get; private set; } = new();
        public List<Room> ElevatorButtons { get; set; } = new();

        public Room(string shortDesc, string longDesc)
        {
            ShortDescription = shortDesc;
            LongDescription = longDesc;
        }

        public void SetExits(Room? north, Room? east, Room? south, Room? west, Room? elevator)
        {
            SetExit("north", north);
            SetExit("east", east);
            SetExit("south", south);
            SetExit("west", west);
            SetExit("elevator", elevator);
            if (elevator != null)
                ElevatorButtons.Add(elevator);
        }

        public void SetExit(string direction, Room? neighbor)
        {
            if (neighbor != null)
                Exits[direction] = neighbor;
        }
        public void AddItem(Item item)
        {
            Items.Add(item);
        }
        public void AddNPC(NPC npc)
        {
            NPCs.Add(npc);
        }
    }
}
