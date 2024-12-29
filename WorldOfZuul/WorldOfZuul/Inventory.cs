using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyGarden
{
    public class Inventory
    {
        public List<Item> Items { get; } = new();
        public List<Badge> Badges { get; } = new();
        public void PickUp(Item item)
        {
            Items.Add(item);
        }
        public void Use(Item item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
                System.Console.WriteLine($"Used {item.Name}");
            }
            else
            {
                Console.WriteLine("Item not in inventory");
            }
        }
        public void ViewInventory()
        {
            if (0 < Items.Count)
            {
                foreach (Item i in Items)
                {
                    Console.WriteLine(i.Name);
                }
            }
            else
            {
                Console.WriteLine("Inventory is empty");
            }
        }
    }
}