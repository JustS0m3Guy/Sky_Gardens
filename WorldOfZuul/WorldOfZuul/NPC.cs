using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGarden
{
    public class NPC
    {
        public string Name { get; set; } 
        public Room Home { get; set; } 
        public List<List<List<string>>> Dialogues = new List<List<List<string>>>();
        public int Interacted = 0;
        public Quest Quest { get; set; }
        public NPC(string name, Quest quest)
        {
            Name = name;
            Quest = quest;
            Room? NPCHome = new Room($"{Name}'s flat", $"This is the home of {Name}");
            Home = NPCHome;
            //Biodiversity Ben -> "Biodiversity_Ben" in order for it to find file in the dialogues folder
            //uses a lambda expression to format the name of the npc to match the file name
            string[] dialogue = File.ReadAllLines("dialogues/" + Name.Split(' ')[0] + "_" + Name.Split(' ')[1] + ".txt").Select(x => x.Trim()).ToArray();
            for (int i = 0; i < dialogue.Length; i++)
            {
                if (dialogue[i] == "/d")
                {
                    Dialogues.Add(new List<List<string>>());
                }
                else 
                {
                    Dialogues[Dialogues.Count - 1].Add(new List<string>());
                    if (dialogue[i] == "/c")
                    {
                        Dialogues[Dialogues.Count - 1][Dialogues[Dialogues.Count - 1].Count - 1].Add("/c");
                        i++;
                        while (dialogue[i] != "/ec")
                        {
                            Dialogues[Dialogues.Count - 1][Dialogues[Dialogues.Count - 1].Count - 1].Add(dialogue[i]);
                            i++;
                        }
                    }
                    else
                    {
                        Dialogues[Dialogues.Count - 1][Dialogues[Dialogues.Count - 1].Count - 1].Add(dialogue[i]);
                    }
                }
            }
            //Diagnostic code to check if the dialogues are stored correctly
            System.Console.WriteLine("Dialogues:");
            for (int i = 0; i < Dialogues.Count; i++)
            {
                for (int j = 0; j < Dialogues[i].Count; j++)
                {
                    for (int k = 0; k < Dialogues[i][j].Count; k++)
                    {
                        System.Console.WriteLine("i: " + i + " j: " + j + " k: " + k);
                        Console.WriteLine(Dialogues[i][j][k]);
                    }
                }
            }
        }
        public void Talk()
        {
            // Implementation needed
        }
    }
}