using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public class NPC
    {
        public string Name { get; set; } 
        public Room Home { get; set; }
        public string[] Dialogue { get; set; }
        public int Interacted = 0;
        public NPC(string name, Room home)
        {
            Name = name;
            Home = home;
            //Biodiversity Ben -> "Biodiversity_Ben" in order for it to find the file
            Dialogue = File.ReadAllLines("dialogues/" + Name.Split(' ')[0] + "_" + Name.Split(' ')[1]).Select(x => x.Trim()).ToArray();
        }
        public void Talk()
        {
            //formatting update required
        }
    }
}
