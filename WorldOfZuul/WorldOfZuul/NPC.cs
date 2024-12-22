using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGarden
{
    public class NPC
    {
        public string Name { get; set; } 
        public Room Home { get; set; } 
        private List<List<List<string>>> Dialogues = new();
        public Quest Quest { get; set; }
        public NPC(string name, Quest quest)
        {
            Name = name;
            Quest = quest;
            Room? NPCHome = new ($"{Name}'s flat", $"This is the home of {Name}");
            Home = NPCHome;
            //Biodiversity Ben -> "Biodiversity_Ben" in order for it to find file in the dialogues folder
            //uses a lambda expression to format the name of the npc to match the file name
            string[] dialogue = File.ReadAllLines("dialogues/" + Name.Split(' ')[0] + "_" + Name.Split(' ')[1] + ".txt").Select(x => x.Trim()).ToArray();
            
            for (int i = 0; i < dialogue.Length; i++)
            {
                // ^1 means the last element of a list
                // Adds Dialogue [] [] to the end of the list
                if (dialogue[i] == "/d")
                    Dialogues.Add(new List<List<string>>() { new List<string>() });
                // Adds Dialogue [] to the end of the list
                else if (dialogue[i] == "/c")
                    Dialogues[^1].Add(new List<string>());
                // Adds Dialogue a string to the end of the list
                else
                    Dialogues[^1][^1].Add(dialogue[i]);
                //Diagnostics("Biodiversity Ben", dialogue, i);
            }
        }
        private void Diagnostics(string name,string[] dialogue, int i)
        {
            if (Name == name)
            {
                Console.WriteLine("i: " + i);
                Console.WriteLine(dialogue[i]);
                for (int j = 0; j < Dialogues.Count; j++)
                {
                    Console.WriteLine(j);
                    for (int k = 0; k < Dialogues[j].Count; k++)
                    {
                        Console.WriteLine($"\t{k}");
                        for (int l = 0; l < Dialogues[j][k].Count; l++)
                            Console.WriteLine($"\t\t{l}");
                    }
                }
            }
        }
        public void Talk()
        {
            Console.Clear();
            foreach (List<string> dialogue in Dialogues[0])
            {
                if (dialogue.Count%2 == 1)
                    Console.WriteLine(dialogue[0]);
                else
                {
                    int selectedIndex = 0;
                    bool selected = false;
                    while (!selected)
                    {
                        for (int i = 0; i < dialogue.Count; i += 2)
                        {
                            if (i / 2 == selectedIndex)
                            {
                                Console.BackgroundColor = ConsoleColor.Gray;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }
                            Console.WriteLine($"{i / 2 + 1}. {dialogue[i]}");
                            Console.ResetColor();
                        }

                        var key = Console.ReadKey(true).Key;
                        if (key == ConsoleKey.UpArrow)
                            selectedIndex = (selectedIndex == 0) ? (dialogue.Count / 2 - 1) : selectedIndex - 1;
                        else if (key == ConsoleKey.DownArrow)
                            selectedIndex = (selectedIndex == dialogue.Count / 2 - 1) ? 0 : selectedIndex + 1;
                        else if (key == ConsoleKey.Enter)
                        {
                            Console.WriteLine(dialogue[selectedIndex * 2 + 1]);
                            selected = true;
                        }
                    }
                }
                
            }
        }
    }
}