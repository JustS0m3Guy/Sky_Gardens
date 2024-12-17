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
        public List<List<List<string>>> Dialogues = new List<List<List<string>>>();
        public int Interacted = 0;
        public NPC(string name, Room home)
        {
            Name = name;
            Home = home;
            //Biodiversity Ben -> "Biodiversity_Ben" in order for it to find file in the dialogues folder
            //uses a lambda expression to format the name of the npc to match the file name
            string[] dialogue = File.ReadAllLines("dialogues/" + Name.Split(' ')[0] + "_" + Name.Split(' ')[1]).Select(x => x.Trim()).ToArray();
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
            // The dialogue is stored as Dialogue[interaction][Line/coice][option]
            for (int i = 0; i < Dialogues[Interacted].Count; i++)
            {
                bool isChoice = Dialogues[Interacted][i][0] == "/c";
                if (!isChoice)
                {
                    System.Console.WriteLine(Dialogues[Interacted][i][0]);
                }
                else
                {
                    System.Console.WriteLine("Choose an option:");
                    for (int j = 1; j < Dialogues[Interacted][i].Count; j++)
                    {
                        if (j/2 == 1)
                        {
                            System.Console.WriteLine(Dialogues[Interacted][i][j]);
                        }
                    }
                    for (int j = 1; j < Dialogues[Interacted][i].Count; j++)
                    {
                        if (j/2 == 0)
                        {
                            System.Console.WriteLine(Dialogues[Interacted][i][j]);
                        }
                    }
                }
            }
            Interacted++;
        }
    }
}