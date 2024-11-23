using System.Reflection.Metadata.Ecma335;

namespace WorldOfZuul
{
    public class Game
    {
        private Room? currentRoom;
        private Room? previousRoom;
        private Inventory inv = new();

        public Game()
        {
            CreateScene();
        }
        private void CreateScene()
        {
            Item? popcorn = new("Popcorn", "A bag of popcorn. It's a bit stale.");
            Room? outside = new("Outside", "You are standing outside the main entrance of the university. If you go east you will enter the building where the Theatre is.");
            Room? theatre = new("Theatre", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.");
            outside.SetExit("east", theatre); // North, East, South, West

            theatre.SetExit("west", outside);
            theatre.AddItem(popcorn);

            NPC? ben = new("Biodiversity Ben", outside);
            for (int i = 0; i < ben.Dialogues.Count; i++)
            {
                for (int j = 0; j < ben.Dialogues[i].Count; j++)
                {
                    Console.WriteLine(ben.Dialogues[i][j]);
                }
            }
            
            currentRoom = outside;
        }

        public void Play()
        {
            Parser parser = new();

            PrintWelcome();
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
                        Console.WriteLine(currentRoom?.LongDescription);
                        if (currentRoom?.Items.Count > 0)
                        {
                            Console.WriteLine("You see the following items:");
                            foreach (Item i in currentRoom.Items)
                            {
                                Console.WriteLine($"- {i.Name}: {i.Description}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There are no items in this room");
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
                        if (currentRoom?.NPCs.Count > 0)
                        {
                            foreach (NPC n in currentRoom.NPCs)
                            {
                                if (n.Name == npcname)
                                {
                                    n.Talk();
                                }
                                else
                                {
                                    Console.WriteLine("There is no such NPC in the room");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("There are no one in this room");
                        }
                        break;

                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }

            Console.WriteLine("Thank you for playing World of Zuul!");
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


        private static void PrintWelcome()
        {
            Console.WriteLine("Welcome to the World of Zuul!");
            Console.WriteLine("World of Zuul is a new, incredibly boring adventure game.");
            PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("You are lost. You are alone. You wander");
            Console.WriteLine("around the university.");
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
            Console.WriteLine("Type 'take' to pick up an item.");
        }
    }
}
