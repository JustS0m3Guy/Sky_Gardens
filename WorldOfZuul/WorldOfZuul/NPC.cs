using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public class NPC
    {
        public string Name { get; set; } //Biodiversity Ben -- "Biodiversity_Ben"
        public Room Home { get; set; }
        public string[] Dialogue { get; set; }
        public NPC(string name, Room home)
        {
            Name = name;
            Home = home;
            Dialogue = File.ReadAllLines("dialogues/" + Name.Split(' ')[0] + "_" + Name.Split(' ')[1]);
        }
    }
}
