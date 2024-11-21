using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public class Inventory
    {
        public List<Item> items { get; } = new();
        public int space { get; } = 10;
        public void PickUp(Item item)
        {
            if ((items.Count < space))
            {
                items.Add(item);
                System.Console.WriteLine($"Added {item.Name} to inventory");
            }
            else
            {
                Console.WriteLine("Inventory is full");
            }
        }
        public void Use(Item item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
                System.Console.WriteLine($"Used {item.Name}");
            }
            else
            {
                Console.WriteLine("Item not in inventory");
            }
        }
        public void ViewInventory()
        {
            if (0 < items.Count)
            {
                foreach (Item i in items)
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