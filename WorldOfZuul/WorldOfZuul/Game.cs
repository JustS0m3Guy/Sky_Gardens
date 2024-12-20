﻿using System.Reflection.Metadata.Ecma335;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

namespace SkyGarden
{
    public class Game
    {
        private Room? currentRoom;
        private Room? previousRoom;
        private Inventory inv = new();
        private List<NPC>? npcs;

        public Game()
        {
            CreateScene();
        }
        private void CreateScene()
        {
            // To create a room, type Room? <roomname> = new("Short description", "Long description");
            // To create an item, type Item? <itemname> = new("Item name", "Item description");
            // To create an NPC, type NPC? <npcname> = new("NPC name", <roomname>);
            // To add dialogues to an NPC, add the dialogues to the dialogues folder, and name the file <npcfirstname>_<npclastname>.txt
            // To add an item to a room, type <roomname>.AddItem(<itemname>);
            // To add an NPC to a room, type <roomname>.AddNPC(<npcname>);
            // to set an exit from a room, type <roomname>.SetExit("direction", <roomname>);

            Badge badges = new();
            List<Badge> badgeList = badges.GetBadges();

            Item? item1 = new("Item1", "This is item 1");

            NPC? Emma = new("Eco-enthusiast Emma", new Quest("Eco-enthusiast Emma's Quest", "Emma quest description", new List<Item>{item1}, badgeList[0]));
            NPC? Walter = new("Wasteful Walter", new Quest("Wasteful Walter's Quest", "Walter quest description", null, badgeList[1]));
            NPC? Paula = new("Polluted Paula", new Quest("Polluted Paula's Quest", "Paula quest description", null, badgeList[2]));
            NPC? Fiona = new("Farmer Fiona", new Quest("Farmer Fiona's Quest", "Fiona quest description", null, badgeList[3]));
            NPC? Ethan = new("Energy-efficient Ethan", new Quest("Energy-efficient Ethan's Quest", "Ethan quest description", null, badgeList[4]));
            NPC? Fred = new("Flooded Fred", new Quest("Flooded Fred's Quest", "Fred quest description", null, badgeList[5]));
            NPC? Lucy = new("Lonely Lucy", new Quest("Lonely Lucy's Quest", "Lucy quest description", null, badgeList[6]));
            NPC? Ben = new("Biodiversity Ben", new Quest("Biodiversity Ben's Quest", "Ben quest description", null, badgeList[7]));
            NPC? Nora = new("Noisy Nora", new Quest("Noisy Nora's Quest", "Nora quest description", null, badgeList[8]));
            npcs = new List<NPC> { Emma, Walter, Paula, Fiona, Ethan, Fred, Lucy, Ben, Nora };

            Room? TCC = new("The City Center","You find yourself in the city center. There are people bustling about, and you can see a large fountain in the middle of the square. From here you can see The Town Hall, The Botanical Garden, The Store and the entrence to your new apartment building.");
            Room? TABE = new("The Apartment Building Entrance","You are standing in the entrance of your new apartment building. You can see the elevator and the stairs leading up to your apartment and a small door leading down to the basement. You can also see the city center from here.");
            Room? TB = new("The Basement","You are in the basement of your apartment building. It is dark and damp, and you can hear the sound of water dripping from the ceiling. You can see a small door leading back up to the entrance of the building.");
            Room? TRG = new("The Rooftop Garden","You are on the roof of your apartment building. You can see the city center from here, and you can see the roof garden that you have been hearing so much about. You can see a small door leading back down to the elevator of the building.");
            Room? TTH = new("The Town Hall","You are in the town hall. You can see the mayor's office, the reception and the city council room. You can see the city center from here.");
            Room? TS = new("The Store","You are in the store. You can see the cashier, the shelves and the exit. You can see the city center from here.");
            Room? TGB = new("The Botanical Garden","You are in the botanical garden. You can see the plants, the flowers and the trees. You can see the city center from here.");
            currentRoom = TABE;

            TCC.SetExits(TTH, TABE, TGB, TS, null);
            TTH.SetExit("south", TCC);
            TGB.SetExit("north", TCC);
            TS.SetExit("east", TCC);
            TABE.SetExits(null, null, TB, TCC, TRG);
            foreach (NPC npc in npcs)
            {
                TABE.SetExit("elevator", npc.Home);
                TRG.SetExit("elevator", npc.Home);
                foreach (NPC npc2 in npcs)
                {
                    if (npc != npc2)
                        npc.Home.SetExit("elevator", npc2.Home);
                }
                npc.Home.SetExit("elevator", TABE);
                npc.Home.SetExit("elevator", TRG);
            }
            TRG.SetExit("elevator", TABE);

            TABE.AddNPC(Ben);
        }
        public void Play()
        {
            Parser parser = new();
            PrintIntro();

            //new PreQuiz().StartPreQuiz();

            bool continuePlaying = true;
            while (continuePlaying)
            {
                Console.WriteLine(currentRoom?.ShortDescription);
                Console.Write("> ");

                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a command.");
                    continue;
                }

                Command? command = parser.GetCommand(input);

                if (command == null)
                {
                    Console.WriteLine("I don't know that command.");
                    continue;
                }

                switch(command.Name)
                {
                    case "look":
                        Console.WriteLine($"{currentRoom?.LongDescription}\n");

                        // foreach function that itterates over the exits of the current room and prints them
                        Console.WriteLine("From here you can go:");
                        if (currentRoom?.Exits.Count > 0)
                        {
                            foreach (string direction in currentRoom.Exits.Keys)
                            {
                                if (direction != "elevator")
                                    Console.WriteLine($"To the {direction}, you can see {currentRoom.Exits[direction].ShortDescription}");
                                else
                                    Console.WriteLine($"You can also see an elevator");
                            }
                            Console.WriteLine();
                        }

                        if (currentRoom?.Items.Count > 0)
                        {
                            Console.WriteLine("You see the following items:");
                            foreach (Item i in currentRoom.Items)
                            {
                                Console.WriteLine($"- {i.Name}: {i.Description}");
                            }
                            System.Console.WriteLine();
                        }
                        else
                            Console.WriteLine("There are no items in this room\n");
                        
                        if (currentRoom?.NPCs.Count > 0)
                        {
                            Console.WriteLine("In the room you see");
                            foreach (NPC n in currentRoom.NPCs)
                            {
                                Console.WriteLine($"- {n.Name}");
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("There are no NPCs in this room\n");
                        }
                        break;

                    case "back":
                        if (previousRoom == null)
                            Console.WriteLine("You can't go back from here!");
                        else
                            currentRoom = previousRoom;
                        break;

                    case "north":
                    case "south":
                    case "east":
                    case "west":
                        Move(command.Name);
                        break;
                    
                    case "elevator":
                        if (currentRoom?.ElevatorButtons.Count > 0)
                        {
                            Console.WriteLine("Where would you like to go?");
                            foreach (Room r in currentRoom.ElevatorButtons)
                            {
                                Console.WriteLine(r.ShortDescription);
                            }
                            string? destination = Console.ReadLine();
                            bool valid = false;
                            foreach (Room r in currentRoom.ElevatorButtons)
                            {
                                if (r.ShortDescription.ToLower() == destination?.ToLower())
                                {
                                    previousRoom = currentRoom;
                                    currentRoom = r;
                                    valid = true;
                                }
                            }
                            if (!valid)
                            {
                                Console.WriteLine("There is no such place in this building");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no elevator in this room");
                        }
                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;

                    case "take":
                        Console.WriteLine("What item would you like to take?");
                        string? item = Console.ReadLine();
                        if (currentRoom?.Items.Count > 0)
                        {
                            // A tempitem is needed to remove the item from the room after it has been picked up because the foreach loop can't remove items from a list while iterating over it.
                            Item? tempitem = null;
                            foreach (Item i in currentRoom.Items)
                            {
                                if (i.Name == item)
                                {
                                    inv.PickUp(i);
                                    tempitem = i;
                                }
                                else
                                {
                                    Console.WriteLine("There is no such item in the room");
                                }
                            }
                            if (tempitem != null)
                            {
                                currentRoom.Items.Remove(tempitem);
                            }
                        }
                        else
                        {
                            Console.WriteLine("There are no items in this room");
                        }
                        break;

                    case "inventory":
                        inv.ViewInventory();
                        break;

                    case "talk":
                        Console.WriteLine("Who would you like to talk to?");
                        string? npcname = Console.ReadLine();
                        bool found = false;
                        if (currentRoom?.NPCs.Count > 0)
                        {
                            foreach (NPC n in currentRoom.NPCs)
                            {
                                if (n.Name.ToLower() == npcname?.ToLower() || n.Name.ToLower().Split(' ')[1] == npcname?.ToLower())
                                {
                                    found = true;
                                    n.Talk();
                                }
                            }
                            if (!found)
                            {
                                Console.WriteLine("There is no such NPC in the room");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no one in this room");
                        }
                        break;

                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }

            Console.WriteLine("Thank you for playing Sky Garden!");
        }

        private void Move(string direction)
        {
            if (currentRoom?.Exits.ContainsKey(direction) == true)
            {
                previousRoom = currentRoom;
                currentRoom = currentRoom?.Exits[direction];
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
        }

        public static void DisplayTextSlowly(string text, int delay = 33)
        {
            foreach (char c in text)
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    Console.Write(text.Substring(text.IndexOf(c)));
                    return;
                }
 
                Console.Write(c);
                Thread.Sleep(delay);
            }
        }

        private static void PrintIntro()
        {
            string gameIntroduction = "\nWelcome to Sky Garden, a festival of urban greenery!\n"
                                    + "You are about to embark on a journey filled with characters and quests.\n"
                                    + "Prepare yourself for helping a neighbourhood restore it's greenery and beauty.\n\n"
                                    + "Press any key to continue...";

            DisplayTextSlowly(gameIntroduction);

            Console.ReadKey();
            Console.WriteLine("\nLet the adventure begin!");
            PrintHelp();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
            Console.WriteLine("Type 'take' to pick up an item.");
            Console.WriteLine("Type 'inventory' to view your inventory.");
            Console.WriteLine("Type 'talk' to talk to an NPC in the current room.");
            Console.WriteLine("Type 'elevator' to use the elevator.");
        }

        private static void SelectQuest()
        {
            
        }
    }
}