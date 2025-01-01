using System.Reflection.Metadata.Ecma335;
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
        private Quest? activeQuest;
        private int day = 0;

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

            Item? posters = new("Posters", "Posters on recycling");
            Item? recyclingBins = new("Recycling Bins", "Recycling bins");
            Item? wrench = new("Wrench", "Wrench");
            Item? barrels = new("Barrels", "Barrels");
            List<Item>? items = new() { posters, recyclingBins, wrench, barrels };

            NPC? Emma = new("Eco-enthusiast Emma", new Quest("Eco-enthusiast Emma's Quest", "Emma quest description", new List<Item>{}, badgeList[0], 1));
            NPC? Walter = new("Wasteful Walter", new Quest("Wasteful Walter's Quest", "Walter quest description", null, badgeList[1], 1));
            NPC? Paula = new("Polluted Paula", new Quest("Polluted Paula's Quest", "Paula quest description", null, badgeList[2], 1));
            NPC? Fiona = new("Farmer Fiona", new Quest("Farmer Fiona's Quest", "Fiona quest description", null, badgeList[3], 1));
            NPC? Ethan = new("Energy-efficient Ethan", new Quest("Energy-efficient Ethan's Quest", "Ethan quest description", null, badgeList[4], 5));
            NPC? Piper = new("Plumber Piper", new Quest("Plumber Piper's Quest", "Piper quest description", null, badgeList[5], 1));
            NPC? Lucy = new("Lonely Lucy", new Quest("Lonely Lucy's Quest", "Lucy quest description", null, badgeList[6], 1));
            NPC? Ben = new("Biodiversity Ben", new Quest("Biodiversity Ben's Quest", "Ben quest description", null, badgeList[7], 3));
            NPC? Nora = new("Noisy Nora", new Quest("Noisy Nora's Quest", "Nora quest description", null, badgeList[8], 1));
            NPC? Wade = new("Worker Wade", new Quest("Worker wade's Quest", "Wade quest description", null, null, 0));
            NPC? Sally = new("Secretary Sally", new Quest("Secretary Sally's Quest", "Sally quest description", null, null, 0));
            List<NPC>? npcs = new() { Emma, Walter, Paula, Fiona, Ethan, Piper, Lucy, Ben, Nora, Wade, Sally };

            Room? TCC = new("The City Center","You find yourself in the city center. There are people bustling about, and you can see a large fountain in the middle of the square. From here you can see The Town Hall, The Botanical Garden, The Store and the entrence to your new apartment building.");
            Room? TABE = new("The Apartment Building Entrance","You are standing in the entrance of your new apartment building. You can see the elevator and the stairs leading up to your apartment and a small door leading down to the basement. You can also see the city center from here.");
            Room? TB = new("The Basement","You are in the basement of your apartment building. It is dark and damp, and you can hear the sound of water dripping from the ceiling. You can see a small door leading back up to the entrance of the building.");
            Room? TRG = new("The Rooftop Garden","You are on the roof of your apartment building. You can see the city center from here, and you can see the roof garden that you have been hearing so much about. You can see a small door leading back down to the elevator of the building.");
            Room? TTH = new("The Town Hall","You are in the town hall. You can see the mayor's office, the reception and the city council room. You can see the city center from here.");
            Room? TW = new("The Company Warehouse","You are in the company warehouse. You can see the storage shelves, the forklift and Wade! You can see the city center from here.");
            Room? TGB = new("The Botanical Garden","You are in the botanical garden. You can see the plants, the flowers and the trees. You can see the city center from here.");
            Room? YR = new("Your Room","You are in your room. You can see the bed, the desk and the window. You can see the city center from here.");
            currentRoom = TABE;
            activeQuest = null;     //Set to null to start the game without a quest

            TCC.SetExits(TTH, TABE, TGB, TW, null);
            TTH.SetExit("south", TCC);
            TGB.SetExit("north", TCC);
            TW.SetExit("east", TCC);
            TW.AddNPC(Wade);
            TABE.SetExits(null, null, TB, TCC, TRG);
            foreach (NPC npc in npcs)
            {
                TABE.SetExit("elevator", npc.Home);
                TRG.SetExit("elevator", npc.Home);
                TABE.AddNPC(npc);
                foreach (NPC npc2 in npcs)
                {
                    if (npc != npc2)
                        npc.Home.SetExit("elevator", npc2.Home);
                }
                npc.Home.SetExit("elevator", TABE);
                npc.Home.SetExit("elevator", TRG);
            }
            TRG.SetExit("elevator", TABE);
            YR.SetExit("elevator", TABE);
            TABE.SetExit("elevator", YR);
            TTH.AddNPC(Sally);
            foreach (Item i in items)
            {
                TW.AddItem(i);
            }

            // Assign NPCs to rooms and load their dialogues
            Ethan.LoadDialogues("dialogues/Energy-efficient_Ethan.txt");
            TRG.AddNPC(Ethan);
        }
        public void Play()
        {
            Parser parser = new();
            Console.Clear();
            PrintIntro();
            //new PreQuiz().StartPreQuiz();
            //new PostQuiz().StartPostQuiz();
            
            bool continuePlaying = true;
            bool firstNewsToday = true;
            bool transfer = false;
            while (continuePlaying)
            {
                transfer = false;
                Console.WriteLine("\n" + currentRoom?.ShortDescription);
                Console.Write("> ");
                string? input = Console.ReadLine();
                Console.WriteLine();
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
                        DisplayTextSlowly(currentRoom?.LongDescription + "\n" ?? "You are in a room with no description.", 10);

                        // foreach function that itterates over the exits of the current room and prints them
                        DisplayTextSlowly("From here you can go:", 10);
                        if (currentRoom?.Exits.Count > 0)
                        {
                            foreach (string direction in currentRoom.Exits.Keys)
                            {
                                if (direction != "elevator")
                                    Console.WriteLine($"To the {direction}, you can see {currentRoom.Exits[direction].ShortDescription}");
                                else
                                    Console.WriteLine($"You also see an elevator");
                                Thread.Sleep(750);
                            }
                            Console.WriteLine();
                        }

                        if (currentRoom?.Items.Count > 0)
                        {
                            DisplayTextSlowly("You see the following items:");
                            foreach (Item i in currentRoom.Items)
                            {
                                Console.WriteLine($"- {i.Name}: {i.Description}");
                                Thread.Sleep(750);
                            }
                            Console.WriteLine();
                        }
                        else
                            DisplayTextSlowly("There are no items in this room.\n");
                        
                        if (currentRoom?.NPCs.Count > 0)
                        {
                            DisplayTextSlowly("In the room, you see:");
                            foreach (NPC n in currentRoom.NPCs)
                            {
                                Console.WriteLine($"- {n.Name}");
                                if (currentRoom.NPCs.Count < 4)
                                    Thread.Sleep(750);
                                else if (currentRoom.NPCs.Count < 8)
                                    Thread.Sleep(500);
                                else
                                    Thread.Sleep(250);
                            }
                            Console.WriteLine();
                        }
                        else
                            DisplayTextSlowly("There is no one in this room.\n");
                        if (currentRoom?.ShortDescription == "The City Center" && firstNewsToday)
                            {
                                transfer = true;
                                goto case "news";
                            }

                        if (currentRoom?.ShortDescription == "The Rooftop Garden")
                        {
                            Console.WriteLine("You can type 'startreworks' to start the reworks if you have the required items.");
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
                        goto case "look";
                    
                    case "news":
                        if (!transfer)
                            firstNewsToday = false;

                        if (!firstNewsToday)
                            DisplayTextSlowly("You take out your phone and start reading the news:");
                        else
                            DisplayTextSlowly("A newspaper vendor hands you a newspaper and on your way to your next destination you start reading it:");
                        string[] newspaper = File.ReadAllLines($"newspaper_stories/Story_{day}.txt");
                        foreach(string segment in newspaper)
                        {
                            DisplayTextSlowly(segment);
                        }
                        firstNewsToday = false;
                        break;
                        
                    case "elevator":
                        if (currentRoom?.ElevatorButtons.Count > 0)
                        {
                            Console.WriteLine("Where would you like to go?");
                            foreach (Room r in currentRoom.ElevatorButtons)
                            {
                                Console.WriteLine(r.ShortDescription);
                                Thread.Sleep(250);
                            }
                            System.Console.Write("> ");
                            string? destination = Console.ReadLine();
                            destination = destination?.ToLower().Trim();
                            bool valid = false;
                            foreach (Room r in currentRoom.ElevatorButtons)
                            {
                                if (r.ShortDescription.ToLower() == destination || r.ShortDescription.ToLower().Split(' ')[1] == destination)
                                {
                                    previousRoom = currentRoom;
                                    currentRoom = r;
                                    valid = true;
                                    if (currentRoom.IsFirstIteration)
                                    {
                                        currentRoom.IsFirstIteration = false;
                                        goto case "look";
                                    }
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
                        Console.WriteLine("What item would you like to take?\n> ");
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
                        Console.Write("> ");
                        string? npcname = Console.ReadLine();
                        bool found = false;
                        if (currentRoom?.NPCs.Count > 0)
                        {
                            npcname = npcname?.ToLower().Trim();
                            foreach (NPC n in currentRoom.NPCs)
                            {
                                if (n.Name.ToLower() == npcname || n.Name.ToLower().Split(' ')[1] == npcname)
                                {
                                    found = true;
                                    n.Talk();
                                    ReceiveQuest(n);
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
                    
                    case "map":
                        string[] map = File.ReadAllLines("misc_txt/map.txt");
                        foreach (string line in map)
                            Console.WriteLine(line);
                        break;
                    
                    case "sleep":
                        if (currentRoom?.ShortDescription == "Your Room" && activeQuest != null && activeQuest.IsCompleted)
                        {
                            day++;
                            activeQuest = null;
                            PrintNextDay();
                        }
                        break;

                    case "startreworks":
                        StartReworks();
                        break;

                    case "questinfo":
                        DisplayQuestInfo();
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
            if (currentRoom?.IsFirstIteration == true)
                currentRoom.IsFirstIteration = false;
        }

        private void StartReworks()
        {
            if (currentRoom?.ShortDescription == "The Rooftop Garden" && activeQuest != null)
            {
                if (activeQuest.RequiredItems != null && inv.HasRequiredItems(activeQuest.RequiredItems))
                {
                    Console.WriteLine("You have all the required items to start the reworks in the rooftop garden.");
                    // Logic to start the reworks
                    activeQuest.IsCompleted = true;
                    Console.WriteLine("Reworks have started successfully!");
                }
                else if (activeQuest == null)
                {
                    Console.WriteLine("You don't have a quest to start the reworks.");
                }
                else
                {
                    Console.WriteLine("You don't have all the required items to start the reworks.");
                }
            }
            else
            {
                Console.WriteLine("You need to be in the Rooftop Garden to start the reworks.");
            }
        }

        private void ReceiveQuest(NPC npc)
        {
            if (npc.Quest != null && !npc.Quest.IsCompleted)
            {
                activeQuest = npc.Quest;
                Console.WriteLine($"You have received a new quest: {npc.Quest.Title}");
                Console.WriteLine(npc.Quest.Description);
            }
        }

        private void DisplayQuestInfo()
        {
            if (activeQuest != null)
            {
                Console.WriteLine($"Current Quest: {activeQuest.Title}");
                Console.WriteLine($"Description: {activeQuest.Description}");
            }
            else
            {
                Console.WriteLine("There is no active quest.");
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
                    Console.WriteLine();
                    return;
                }
 
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }
        
        
        // public static void DisplayOptionsSlowly(List<Type> options, int delay = 750)
        // {
        //     foreach (Type option in options)
        //     {
        //         if(Console.KeyAvailable)
        //         {
        //             Console.ReadKey(true);
        //             delay = 0;
        //             return;
        //         } 
        //         Console.WriteLine(option);
        //         Thread.Sleep(delay);
        //     }
        // }

        private static void PrintIntro()
        {
            string gameIntroduction = "Welcome to Sky Garden, a festival of urban greenery!\n"
                                    + "You are about to embark on a journey filled with characters and quests.\n"
                                    + "Prepare yourself for helping a neighbourhood restore it's greenery and beauty.\n"
                                    + "\nPress any key to continue...";

            DisplayTextSlowly(gameIntroduction);

            Console.ReadKey();
            Console.WriteLine("\nLet the adventure begin!");
            PrintHelp();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details. Will also run after entering a room for the first time.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
            Console.WriteLine("Type 'take' to pick up an item.");
            Console.WriteLine("Type 'inventory' to view your inventory.");
            Console.WriteLine("Type 'talk' to talk to an NPC in the current room.");
            Console.WriteLine("Type 'elevator' to use the elevator.");
            Console.WriteLine("Type 'map' to view the map.");
            Console.WriteLine("Type 'sleep' to sleep in your room.");
            Console.WriteLine("Type 'news' to read the news.\n");
            string gameHelp = "Navigate by typing 'north', 'south', 'east', or 'west'.\n"
                            + "Type 'look' for more details. Will also run after entering a room for the first time.\n"
                            + "Type 'back' to go to the previous room.\n"
                            + "Type 'take' to pick up an item.\n"
                            + "Type 'inventory' to view your inventory.\n"
                            + "Type 'talk' to talk to an NPC in the current room.\n"
                            + "Type 'elevator' to use the elevator.\n"
                            + "Type 'newspaper' to read the latest news.\n"
                            + "Type 'questinfo' to check your active quest.\n"
                            + "Type 'help' to print this message again.\n"
                            + "Type 'quit' to exit the game.\n";

            Console.WriteLine(gameHelp);
        }
        private static void PrintNextDay()
        {
            DisplayTextSlowly("You slept soundly after a long day of helping a newfound friend.");
            DisplayTextSlowly("You wake up to a new day, ready to continue helping your neighbourhood.\n");
        }
    }
}