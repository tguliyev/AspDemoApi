using System.Collections.Generic;
using System.Linq;
using AspDemo.Api.Models;

namespace AspDemo.Api.Repositories
{
    //public class InMemItemsRepository : IItemsRepository
    public class InMemItemsRepository
    {
        private List<Item> items;

        public InMemItemsRepository()
        {
            this.items = new List<Item>()
            {
                new Item() { Id = 1, Name = "Audi RS3", Price = 50000 },
                new Item() { Id = 2, Name = "Audi RS6", Price = 120000 },
                new Item() { Id = 3, Name = "Audi RS7", Price = 130000 }
            };

        }

        public IEnumerable<Item> GetItemsAsync()
        {
            return this.items;
        }

        public Item? GetItemAsync(int id)
        {
            return this.items.Where(item => item.Id == id).FirstOrDefault();
        }

        public void CreateItemAsync(Item item)
        {
            items.Add(item);
        }

        public void UpdateItemAsync(Item item)
        {
            int itemIndex = items.FindIndex(i => i.Id == item.Id);
            items[itemIndex] = item;
        }

        public void DeleteItemAsync(int id)
        {
            int index = items.FindIndex(item => item.Id == id);
            items.RemoveAt(index);
        }
    }
}