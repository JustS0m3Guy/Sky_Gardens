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

            Item? compostBins = new("Compost Bins", "Compost bins");
            Item? posters = new("Posters", "Posters that you printed recyclying information on");
            Item? recyclingBins = new("Recycling Bins", "Recycling bins");
            Item? wrench = new("Wrench", "Wrench");
            Item? barrels = new("Barrels", "Barrels");
            Item? treeSaplings = new("Tree Saplings", "Tree saplings");
            Item? plantSeeds = new("Plant Seeds", "Complimentary Plant seeds");
            Item? sprinkler = new("Sprinkler", "Sprinkler for irrigation");
            Item? benches = new("Benches", "Benches for the garden, ready to be put together");
            Item? communityBoard = new("Community Board", "Community board for the garden");
            Item? gardenTools = new("Garden Tools", "Garden tools for community use");
            Item? birdFeeders = new("Bird Feeders", "Bird feeders for the garden");
            Item? birdFeed = new("Bird Seed", "Bird seed for the bird feeders");
            Item? localFlowers = new("Local Flowers", "Flowers native to the area. Perfect to attract bees and butterflies.");
            Item? denseBush = new("Dense evergreen bush saplings", "A dense evergreen bush that can be used to create a hedge. Perfect for reducing noise pollution.");
            Item? noiseMeter = new("Noise Meter", "A noise meter to measure the noise pollution in the area.");
            List<Item>? items = new() { compostBins, posters, recyclingBins, wrench, barrels, treeSaplings, plantSeeds, sprinkler, benches, communityBoard, gardenTools, birdFeeders, birdFeed, localFlowers, denseBush, noiseMeter };

            Room? CC = new("City Center","You find yourself in the city center. There are people bustling about, and you can see a large fountain in the middle of the square. From here you can see The Town Hall, The Botanical Garden, The Store and the entrence to your new apartment building.");
            Room? ABE = new("Apartment Building Entrance","You are standing in the entrance of your new apartment building. You can see the elevator and the stairs leading up to your apartment and a small door leading down to the basement. You can also see the city center from here.");
            Room? B = new("Basement","You are in the basement of your apartment building. It is dark and damp, and you can hear the sound of water dripping from the ceiling. You can see a small door leading back up to the entrance of the building.");
            Room? RG = new("Rooftop Garden","You are on the roof of your apartment building. You can see the city center from here, and you can see the roof garden that you have been hearing so much about. You can see a small door leading back down to the elevator of the building.");
            Room? TH = new("Town Hall","You are in the town hall. You can see the mayor's office, the reception and the city council room. You can see the city center from here.");
            Room? CW = new("Company Warehouse","You are in the company warehouse. You can see the storage shelves, the forklift and Wade! You can see the city center from here.");
            Room? BG = new("Botanical Garden","You are in the botanical garden. You can see the plants, the flowers and the trees. You can see the city center from here.");
            Room? YR = new("Your Room","You are in your room. You can see the bed, the desk and the window. You can see the city center from here.");

            NPC? Emma = new("Eco-enthusiast Emma", new Quest("Eco-enthusiast Emma's Quest", "Emma quest description", new List<Item>{compostBins}, new List<Room>{BG, RG, BG}, badgeList[0], 1));
            NPC? Walter = new("Wasteful Walter", null);
            Walter.Quest = new Quest("Wasteful Walter's Quest", "Walter quest description", new List<Item>{posters, recyclingBins}, new List<Room>{CC, Walter.Home, BG, Walter.Home}, badgeList[1], 1);
            NPC? Paula = new("Polluted Paula", new Quest("Polluted Paula's Quest", "Paula quest description", new List<Item>{treeSaplings}, new List<Room>{ABE, RG, RG}, badgeList[2], 1));
            NPC? Fiona = new("Farmer Fiona", new Quest("Farmer Fiona's Quest", "Fiona quest description", new List<Item>{plantSeeds, sprinkler}, new List<Room>{RG, RG, RG, ABE}, badgeList[3], 1));
            NPC? Ethan = new("Energy-efficient Ethan", null);
            Ethan.Quest = new Quest("Energy-efficient Ethan's Quest", "Ethan quest description", new List<Item>{posters}, new List<Room>{Ethan.Home, RG, RG}, badgeList[4], 1);
            NPC? Piper = new("Plumber Piper", null);
            Piper.Quest = new Quest("Plumber Piper's Quest", "Piper quest description", new List<Item>{wrench, barrels}, new List<Room>{B, RG, B, B}, badgeList[5], 1);
            NPC? Lucy = new("Lonely Lucy", new Quest("Lonely Lucy's Quest", "Lucy quest description", new List<Item>{benches, communityBoard, gardenTools}, new List<Room>{ABE, RG, RG}, badgeList[6], 1));
            NPC? Ben = new("Biodiversity Ben", null);
            Ben.Quest = new Quest("Biodiversity Ben's Quest", "Ben quest description", new List<Item>{birdFeed, birdFeeders, localFlowers}, new List<Room>{RG, Ben.Home, RG, RG}, badgeList[7], 3);
            NPC? Nora = new("Noisy Nora", null);
            Nora.Quest = new Quest("Noisy Nora's Quest", "Nora quest description", new List<Item>{noiseMeter, denseBush}, new List<Room>{Nora.Home, RG, RG}, badgeList[8], 1);

            NPC? Wade = new("Worker Wade", null);
            NPC? Sally = new("Secretary Sally", null);
            npcs = new() { Emma, Walter, Paula, Fiona, Ethan, Piper, Lucy, Ben, Nora, Wade, Sally };
            currentRoom = ABE;

            CC.SetExits(TH, ABE, BG, CW, null);
            TH.SetExit("south", CC);
            BG.SetExit("north", CC);
            CW.SetExit("east", CC);
            CW.AddNPC(Wade);
            ABE.SetExits(null, null, B, CC, RG);
            foreach (NPC npc in npcs)
            {
                ABE.SetExit("elevator", npc.Home);
                RG.SetExit("elevator", npc.Home);
                foreach (NPC npc2 in npcs)
                {
                    if (npc != npc2)
                        npc.Home.SetExit("elevator", npc2.Home);
                }
                npc.Home.SetExit("elevator", ABE);
                npc.Home.SetExit("elevator", RG);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                npc.CurrentRoom?.AddNPC(npc);
                Console.WriteLine($"{npc.Name} got added to {npc.CurrentRoom?.ShortDescription}");
                Console.ResetColor();
            }
            RG.SetExit("elevator", ABE);
            YR.SetExit("elevator", ABE);
            ABE.SetExit("elevator", YR);
            TH.AddNPC(Sally);
            foreach (Item i in items)
            {
                CW.AddItem(i);
            }

            // Assign NPCs to rooms and load their dialogues
            Ethan.LoadDialogues("dialogues/Energy-efficient_Ethan.txt");
            RG.AddNPC(Ethan);
        }
        public void Play()
        {
            Parser parser = new();
            //Console.Clear();
            //new PreQuiz().StartPreQuiz();
            //Console.Clear();
            //new PostQuiz().StartPostQuiz();
            PrintIntro();
            
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

                if (npcs != null)
                {
                    foreach (NPC n in npcs)
                    {
                        if (n.Quest != null && n.Quest.Places != null)
                        {
                            var place = n.Quest.Places[n.Quest.QuestProgress];
                            MoveNPC(n, place);
                        }
                    }
                }

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
                            for (int i = 0; i < currentRoom?.ElevatorButtons.Count; i++)
                            {
                                Console.WriteLine($"{i + 1} {currentRoom?.ElevatorButtons[i].ShortDescription}");
                                Thread.Sleep(250);
                            }
                            System.Console.Write("> ");
                            string? destination = Console.ReadLine();
                            destination = destination?.ToLower().Trim();
                            bool valid = false;
                            for (int i = 0; i < currentRoom?.ElevatorButtons.Count; i++)
                            {
                                if (currentRoom?.ElevatorButtons[i].ShortDescription.ToLower() == destination || (i+1).ToString() == destination)
                                {
                                    previousRoom = currentRoom;
                                    currentRoom = currentRoom?.ElevatorButtons[i];
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
                                    if (n.Quest != null && !n.Quest.IsCompleted && activeQuest == null)
                                    {
                                        activeQuest = n.Quest;
                                        Console.WriteLine($"You have received a new quest: {n.Quest.Title}");
                                        Console.WriteLine(n.Quest.Description);
                                    }
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
                        if (activeQuest != null)
                        {
                            activeQuest.DisplayQuestInfo();
                        }
                        else
                        {
                            Console.WriteLine("You don't have an active quest.");
                        }
                        break;

                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }

            Console.WriteLine("Thank you for playing Sky Garden!");
        }

        private void MoveNPC(NPC n, Room to)
        {
            n.CurrentRoom?.RemoveNPC(n);
            to.AddNPC(n);
            n.CurrentRoom = to;
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
            string gameHelp = "Navigate by typing 'north', 'south', 'east', or 'west'.\n"
                            + "Type 'look' for more details. Will also run after entering a room for the first time.\n"
                            + "Type 'back' to go to the previous room.\n"
                            + "Type 'take' to pick up an item.\n"
                            + "Type 'inventory' to view your inventory.\n"
                            + "Type 'talk' to talk to an NPC in the current room.\n"
                            + "Type 'elevator' to use the elevator.\n"
                            + "Type 'news' to read the latest news.\n"
                            + "Type 'questinfo' to check your active quest.\n"
                            + "Type 'map' to view the map.\n"
                            + "Type 'sleep' to sleep in your room once you're done with a quest.\n"
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