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
        public List<List<string>> Dialogues = new List<List<string>>();
        public int Interacted = 0;
        public NPC(string name, Room home)
        {
            Name = name;
            Home = home;
            //Biodiversity Ben -> "Biodiversity_Ben" in order for it to find file
            string[] dialogue = File.ReadAllLines("dialogues/" + Name.Split(' ')[0] + "_" + Name.Split(' ')[1]).Select(x => x.Trim()).ToArray();
            for (int i = 0; i < dialogue.Length; i++)
            {
                if (dialogue[i] == "0")
                {
                    Dialogues.Add(new List<string>());
                }
                else
                {
                    Dialogues[Dialogues.Count - 1].Add(dialogue[i]);
                }
            }
        }
        public void Talk()
        {
            //formatting update required
        }
    }
}