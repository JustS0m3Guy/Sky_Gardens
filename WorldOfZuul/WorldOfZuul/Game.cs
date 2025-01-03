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
        private Quest? activeQuest;
        private int day = -1;
        private List<NPC>? npcs;
        private List<Quest> quests = new();
        private NPC? Niko2;
        private bool introDay = true;
        public bool canSleep;

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
            Item? solarPanels = new("Solar Panels", "Solar panels for renewable energy");
            List<Item>? items = new() { compostBins, posters, recyclingBins, wrench, barrels, treeSaplings, plantSeeds, sprinkler, benches, communityBoard, gardenTools, birdFeeders, birdFeed, localFlowers, denseBush, noiseMeter, solarPanels };

            Room? CC = new("City Center", "You find yourself in the city center. There are people bustling about, and you can see a large fountain in the middle of the square. From here you can see The Town Hall, The Botanical Garden, The Store and the entrence to your new apartment building.");
            Room? ABE = new("Apartment Building Entrance", "You are standing in the entrance of your new apartment building. You can see the elevator and the stairs leading up to your apartment and a small door leading down to the basement. You can also see the city center from here.");
            Room? B = new("Basement", "You are in the basement of your apartment building. It is dark and damp, and you can hear the sound of water dripping from the ceiling. You can see a small door leading back up to the entrance of the building.");
            Room? RG = new("Rooftop Garden", "You are on the roof of your apartment building. You can see the city center from here, and you can see the roof garden that you have been hearing so much about. You can see a small door leading back down to the elevator of the building.");
            Room? TH = new("Town Hall", "You are in the town hall. You can see the mayor's office, the reception and the city council room. You can see the city center from here.");
            Room? CW = new("Company Warehouse", "You are in the company warehouse. You can see the storage shelves, the forklift and Wade! You can see the city center from here.");
            Room? BG = new("Botanical Garden", "You are in the botanical garden. You can see the plants, the flowers and the trees. You can see the city center from here.");
            Room? YR = new("Your Room", "You are in your room. You can see the bed, the desk and the window. You can see the city center from here.");

            NPC? Emma = new("Eco-enthusiast Emma", new Quest("Eco-enthusiast Emma's Quest", "Eco Emma is a local environmental advocate in the city. She is passionate about reducing waste and promoting sustainability. Emma notices that the apartment building where the player works on the Sky Garden project has been throwing away large amounts of organic waste that could be composted. She tasks the player with helping to set up a composting system for the rooftop garden and educating the residents about its benefits.", new List<Item> { compostBins }, new List<Room> { BG, RG, RG, BG }, badgeList[0], 2));
            NPC? Walter = new("Wasteful Walter", null);
            Walter.NPCQuest = new Quest("Wasteful Walter's Quest", "Wasteful Walter is an elderly man living in the Apartment complex who tends to be quite forgetful. He has recently had problems when it comes to recycling and ended up hoarding a large amount of waste in his living space because he refuses to throw anything out without properly sorting the trash first. He tasks the player with setting up respective trash containers that are easily recognizable by colour and posters detailing information on how to recycle, so Walter would have no problem sorting his waste.", new List<Item> { recyclingBins, solarPanels }, new List<Room> { CC, Walter.Home, BG, Walter.Home, RG }, badgeList[1], 2);
            NPC? Paula = new("Polluted Paula", new Quest("Polluted Paula's Quest", "Polluted Paula is deeply concerned about the air quality in her community, which has suffered due to emissions from nearby factories. The poor air quality has led to increased health issues, especially respiratory problems among children. Paula is determined to create a healthier environment for her family and neighbors by implementing sustainable solutions, such as planting trees and establishing green spaces. She needs assistance to turn her vision into a reality and to rally the community for collective action.", new List<Item> { treeSaplings }, new List<Room> { ABE, RG, RG }, badgeList[2], 1));
            NPC? Fiona = new("Farmer Fiona", new Quest("Farmer Fiona's Quest", "Farmer Fiona is a local urban farmer who runs a small community-supported agriculture (CSA) farm on the outskirts of the city. She’s passionate about creating sustainable farming practices that minimize environmental impact. With the growing popularity of the Sky Garden, Fiona sees an opportunity to integrate her farm into the rooftop garden and create a farm-to-table system. The idea is to provide fresh, local produce directly to the residents while promoting sustainable food practices and reducing the carbon footprint of food transportation. Fiona needs the player’s help to bring her vision to life.", new List<Item> { plantSeeds, sprinkler }, new List<Room> { RG, RG, RG, ABE }, badgeList[3], 2));
            NPC? Ethan = new("Energy-efficient Ethan", null);
            Ethan.NPCQuest = new Quest("Energy-efficient Ethan's Quest", "Ethan, a tech-savvy resident, is concerned about high energy bills and the building's carbon footprint. The player must assist Ethan in implementing renewable energy solutions and promoting energy-saving practices. The player task is reducing energy consumption in an apartment building.", new List<Item> { posters }, new List<Room> { Ethan.Home, RG, RG, RG, RG }, badgeList[4], 1);
            NPC? Piper = new("Plumber Piper", null);
            Piper.NPCQuest = new Quest("Plumber Piper's Quest", "An eccentric individual with a big scruffy beard and a pipe for a peg leg. Piper says that he’s a retired plumber and is in need of an assistant willing to help him tame the “Kraken” (normally known as the piping system) and conquer the “Mighty seas” (also known as his flooded basement),  which are currently in that condition due to poor water management systems.", new List<Item> { wrench, barrels }, new List<Room> { B, RG, B, B }, badgeList[5], 2);
            NPC? Lucy = new("Lonely Lucy", new Quest("Lonely Lucy's Quest", "Lucy, a long-time resident, struggles with community disconnection and seeks ways to rebuild social connections within the building. The player must help Lucy organise community spaces and events. The player task is addressing social isolation in an apartment building.", new List<Item> { benches, communityBoard, gardenTools }, new List<Room> { ABE, RG, RG }, badgeList[6], 2));
            NPC? Ben = new("Biodiversity Ben", null);
            Ben.NPCQuest = new Quest("Biodiversity Ben's Quest", "Biodiversity Ben is an enthusiastic advocate for urban nature, always wearing his signature yellow jacket with a bee patch and a cosy green beanie, a nod to his love for all things natural and sustainable. His bright demeanour, boundless energy and endless optimism makes him ready for any task at hand.", new List<Item> { birdFeed, birdFeeders, localFlowers }, new List<Room> { RG, Ben.Home, RG, RG }, badgeList[7], 2);
            NPC? Nora = new("Noisy Nora", null);
            Nora.NPCQuest = new Quest("Noisy Nora's Quest", "Noisy Nora lives in a neighborhood overwhelmed by traffic noise, disrupting sleep and daily life. Determined to improve the community's quality of life, she seeks sustainable landscaping solutions to reduce noise pollution and needs help implementing her ideas and rallying community support.", new List<Item>{ noiseMeter, denseBush }, new List<Room>{Nora.Home, RG, RG}, badgeList[8], 2);
            NPC? Niko = new("Mayor Niko", null);
            Niko.NPCQuest = new Quest("Mayor Niko's Quest", "Get aquired with the mayor of the city.", null, new List<Room>{TH, TH}, null, -1);

            NPC? Wade = new("Worker Wade", null);
            NPC? Sally = new("Secretary Sally", null);
            Niko2 = new("Niko Mayor", null);
            npcs = new() { Emma, Walter, Paula, Fiona, Ethan, Piper, Lucy, Ben, Nora };
            if (Emma.NPCQuest != null && Walter.NPCQuest != null && Paula.NPCQuest != null && Fiona.NPCQuest != null && Ethan.NPCQuest != null && Piper.NPCQuest != null && Lucy.NPCQuest != null && Ben.NPCQuest != null && Nora.NPCQuest != null)
            {
                quests = new() { Emma.NPCQuest, Walter.NPCQuest, Paula.NPCQuest, Fiona.NPCQuest, Ethan.NPCQuest, Piper.NPCQuest, Lucy.NPCQuest, Ben.NPCQuest, Nora.NPCQuest };
            }
            currentRoom = TH;
            inv.Items.Add(birdFeed);
            inv.Items.Add(birdFeeders);
            inv.Items.Add(localFlowers);
            activeQuest = Niko.NPCQuest;

            CC.SetExits(TH, ABE, BG, CW, null);
            TH.SetExit("south", CC);
            BG.SetExit("north", CC);
            CW.SetExit("east", CC);
            CW.AddNPC(Wade);
            ABE.SetExits(null, null, B, CC, null);
            ABE.SetExit("elevator", YR);
            YR.SetExit("elevator", ABE);
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
            }
            RG.SetExit("elevator", ABE);
            ABE.SetExit("elevator", YR);
            TH.AddNPC(Niko);
            TH.AddNPC(Sally);
            ABE.SetExit("elevator", RG);
            foreach (Item i in items)
            {
                CW.AddItem(i);
            }

            RG.AddNPC(Ethan);
        }
        public void Play()
        {
            Parser parser = new();
            Console.Clear();
            new PreQuiz().StartPreQuiz();
            PrintIntro();
            activeQuest?.DisplayQuestInfo();

            bool continuePlaying = true;
            bool firstNewsToday = true;
            bool transfer;
            while (continuePlaying)
            {
                if (AllQuestsCompleted() == true)
                {
                    Console.WriteLine("You have completed all quests! Visit the Mayor's Office for a special dialogue.");
                }

                transfer = false;
                Console.WriteLine("\n" + currentRoom?.ShortDescription);
                activeQuest?.Check();

                if (day == 0)
                {
                    introDay = false;
                }

                if (!introDay && npcs != null)
                {
                    foreach (NPC n in npcs)
                    {
                        if (n.NPCQuest != null && n.NPCQuest.Places != null && !n.NPCQuest.IsCompleted)
                        {
                            var place = n.NPCQuest.Places[n.NPCQuest.QuestProgress];
                            MoveNPC(n, place);
                        }
                    }
                }

                if (activeQuest != null && activeQuest.IsCompleted)
                {
                    if (activeQuest.Reward != null)
                        inv.Badges.Add(activeQuest.Reward);
                    canSleep = true;
                    activeQuest = null;
                }

                Console.Write("> ");
                string? input = Console.ReadLine();
                
                Console.WriteLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a command.");
                    continue;
                }
                input = input.ToLower().Trim();
                Command? command = parser.GetCommand(input);

                if (command == null)
                {
                    Console.WriteLine("I don't know that command.");
                    continue;
                }

                switch (command.Name)
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
                                Thread.Sleep(250);
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
                        if (!introDay)
                        {
                            Move(command.Name);
                            if (currentRoom?.IsFirstIteration == true)
                            {
                                currentRoom.IsFirstIteration = false;
                                goto case "look";
                            }
                        }
                        else if (activeQuest != null)
                        {
                            Console.WriteLine("You can't leave the town hall. You have to talk to Mayor Niko.");
                        }
                        else if (command.Name == "south")
                        {
                            DisplayTextSlowly("After a long day of traveling and meeting the Mayor, you decide to go home.");
                            currentRoom = currentRoom?.Exits["south"].Exits["east"].ElevatorButtons[0];
                        }
                        else
                        {
                            Console.WriteLine($"You can't go {command.Name}!");
                        }
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
                                if (currentRoom?.ElevatorButtons[i].ShortDescription.ToLower() == destination || (i + 1).ToString() == destination)
                                {
                                    previousRoom = currentRoom;
                                    currentRoom = currentRoom?.ElevatorButtons[i];
                                    valid = true;
                                    if (currentRoom != null && currentRoom.IsFirstIteration)
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

                    case "news":
                        if (!introDay)
                        {
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
                        }
                        else
                        {
                            Console.WriteLine("You don't have the news app downloaded, you decide to get it tomorrow.");
                        }
                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;

                    case "take":
                        Console.Write("What item would you like to take?\n> ");
                        string? item = Console.ReadLine();
                        bool itemFound = false;
                        if (currentRoom?.Items.Count > 0 && item != null)
                        {
                            // A tempitem is needed to remove the item from the room after it has been picked up because the foreach loop can't remove items from a list while iterating over it.
                            Item? tempitem = null;
                            item = item.ToLower().Trim();
                            foreach (Item i in currentRoom.Items)
                            {
                                if (i.Name.ToLower().Trim() == item)
                                {
                                    itemFound = true;
                                    inv.PickUp(i);
                                    tempitem = i;
                                }
                            }
                            if (tempitem != null)
                            {
                                currentRoom.Items.Remove(tempitem);
                            }
                            if (itemFound == false)
                            {
                                Console.WriteLine("There is no such item in the room");
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
                                    if (introDay && !(npcname == "niko" || npcname == "mayor niko"))
                                    {
                                        Console.WriteLine("Go talk to Mayor Niko. He is waiting for you.");
                                    }
                                    else if (AllQuestsCompleted() == true)
                                    {
                                        continuePlaying = false;
                                    }
                                    else if (activeQuest == n.NPCQuest || activeQuest == null)
                                    {
                                        bool canQuest = false;
                                        if (n.NPCQuest != null && n.NPCQuest.RequiredItems != null && n.NPCQuest.CanComplete(inv.Items) && n.NPCQuest.QuestProgress == n.NPCQuest.ItemRemovalIndex)
                                        {
                                            canQuest = true;
                                            foreach (Item i in n.NPCQuest.RequiredItems)
                                            {
                                                inv.Items.Remove(i);
                                            }
                                        }
                                        n.Talk(canQuest);
                                        if (n.NPCQuest != null && !n.NPCQuest.IsCompleted && activeQuest == null)
                                        {
                                            activeQuest = n.NPCQuest;
                                            Console.WriteLine($"\nYou have received a new quest: {n.NPCQuest.Title}");
                                            Console.WriteLine(n.NPCQuest.Description);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("You seem busy with someone else right now. Come back when you are free.");
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
                        if (currentRoom?.ShortDescription == "Your Room" && activeQuest == null && canSleep)
                        {
                            day++;
                            canSleep = false;
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
                    case "completeall":
                        foreach (Quest quest in quests)
                        {
                            quest.IsCompleted = true;
                        }
                        break;

                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }

            if (!AllQuestsCompleted())
                Console.WriteLine("Thank you for playing Sky Garden! Hope you come back another time to play the game through to the end!");
            else
            {
                Niko2?.Talk(true);
                new PostQuiz().StartPostQuiz();   
            }
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

        private bool AllQuestsCompleted()
        {
            foreach (Quest quest in quests)
            {
                if (!quest.IsCompleted)
                {
                    return false;
                }
            }
            return true;
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
                                    + "You're an epmloyee sent by the company Big Green to find out what is needed to build the perfect Sky Garden.\n"
                                    + "You just finished moving into an old apartment building full of residents and have gone to the Town Hall to meet the Mayor\n"
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