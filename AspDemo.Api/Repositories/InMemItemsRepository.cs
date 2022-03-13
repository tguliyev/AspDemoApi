using System.Collections.Generic;
using System.Linq;
using AspDemo.Api.Models;

namespace AspDemo.Api.Repositories
{
    //public class InMemItemsRepository : IItemsRepository
    public class InMemItemsRepository
    {
        private List<Item> Items;

        public InMemItemsRepository()
        {
            this.Items = new List<Item>()
            {
                new Item() { Id = 1, Name = "Audi RS3", Price = 50000 },
                new Item() { Id = 2, Name = "Audi RS6", Price = 120000 },
                new Item() { Id = 3, Name = "Audi RS7", Price = 130000 }
            };

        }

        public IEnumerable<Item> GetItemsAsync()
        {
            return this.Items;
        }

        public Item? GetItemAsync(int Id)
        {
            return this.Items.Where(Item => Item.Id == Id).FirstOrDefault();
        }

        public void CreateItemAsync(Item ItemToCreate)
        {
            Items.Add(ItemToCreate);
        }

        public void UpdateItemAsync(Item ItemToUpdate)
        {
            int ItemIndex = Items.FindIndex(i => i.Id == ItemToUpdate.Id);
            Items[ItemIndex] = ItemToUpdate;
        }

        public void DeleteItemAsync(int Id)
        {
            int Index = Items.FindIndex(Item => Item.Id == Id);
            Items.RemoveAt(Index);
        }
    }
}