using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGarden
{
    public class CommandWords
    {
        public List<string> ValidCommands { get; } = new List<string> { "north", "east", "south", "west", "look", "back", "elevator", "quit", "take", "inventory", "talk", "newspaper", "map", "sleep" };
        public bool IsValidCommand(string command)
        {
            return ValidCommands.Contains(command);
        }
    }
}
