using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SkyGarden
{
    public class NPC
    {
        public string Name { get; set; } 
        public Room Home { get; set; } 
        public Room? CurrentRoom { get; set; }
        private List<List<List<string>>> Dialogues = new();
        public Quest? Quest { get; set; }
        public NPC(string name, Quest? quest)
        {
            Name = name;
            Quest = quest;
            Room? NPCHome = new ($"{Name}'s flat", $"This is the home of {Name}");
            Home = NPCHome;
            CurrentRoom = Quest?.Places?[0];
            //Biodiversity Ben -> "Biodiversity_Ben" in order for it to find file in the dialogues folder
            //uses a lambda expression to format the name of the npc to match the file name
            string fileName = $"dialogues/{Name.Split(' ')[0]}_{Name.Split(' ')[1]}.txt";
            string[] dialogue = File.ReadAllLines(fileName).Select(x => x.Trim()).ToArray();
            
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

        public void LoadDialogues(string filePath)
        {
            try
            {
                string[] dialogue = File.ReadAllLines(filePath).Select(x => x.Trim()).ToArray();
                Dialogues.Clear();
                for (int i = 0; i < dialogue.Length; i++)
                {
                    if (dialogue[i] == "/d")
                        Dialogues.Add(new List<List<string>>() { new List<string>() });
                    else if (dialogue[i] == "/c")
                        Dialogues[^1].Add(new List<string>());
                    else
                        Dialogues[^1][^1].Add(dialogue[i]);
                }
            } 
            catch (IOException e) 
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Diagnostics(string name,string[] dialogue, int i = -1)
        {
            if (Name == name)
            {
                if (i != -1)
                {
                    Console.WriteLine("i: " + i);
                    Console.WriteLine(dialogue[i]);
                }
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
            if (Quest != null && Quest.QuestProgress > Quest.QuestLength)
            {
                Console.WriteLine(Dialogues[^1][^1][^1]);
                Quest.IsCompleted = true;
            }
            else
            {
                foreach (List<string> dialogue in Dialogues[Quest.QuestProgress])
                {
                    if (dialogue.Count%2 == 1)
                        Game.DisplayTextSlowly(dialogue[0]);
                    else if (dialogue.Count > 0)
                    {
                        int selectedIndex = 0;
                        bool selected = false;
                        int optionsStartLine = Console.CursorTop;
                        bool firstLoop = true;
                        while (!selected)
                        {
                            for (int i = 0; i < dialogue.Count; i += 2)
                            {
                                if (i / 2 == selectedIndex)
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write("> " + dialogue[i]);
                                }
                                else
                                    Console.Write(dialogue[i] + "  ");
                                if (firstLoop)
                                    Thread.Sleep(750);
                                Console.ResetColor();
                                Console.WriteLine();
                            }

                            var key = Console.ReadKey(true).Key;
                            if (key == ConsoleKey.UpArrow)
                            {
                                selectedIndex = (selectedIndex == 0) ? (dialogue.Count / 2 - 1) : selectedIndex - 1;
                                Console.SetCursorPosition(0, optionsStartLine - dialogue.Count / 2);
                            }
                            else if (key == ConsoleKey.DownArrow)
                            {
                                selectedIndex = (selectedIndex == dialogue.Count / 2 - 1) ? 0 : selectedIndex + 1;
                                Console.SetCursorPosition(0, optionsStartLine - dialogue.Count / 2);
                            }
                            else if (key == ConsoleKey.Enter)
                            {
                                Game.DisplayTextSlowly(dialogue[selectedIndex * 2 + 1]);
                                selected = true;
                            }
                            firstLoop = false;
                        }
                    }
                }
                Quest.QuestProgress++;
            }
        }
    }
}