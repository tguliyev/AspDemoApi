using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspDemo.Api.EntityFramework;
using AspDemo.Api.Models;

namespace AspDemo.Api.Repositories
{
    public class EFCoreItemsRepository : IItemsRepository
    {
        private IDataContext Context;
        public EFCoreItemsRepository(IDataContext Context)
        {
            this.Context = Context;
        }

        public async Task<IEnumerable<Item>?> GetItemsAsync()
        {
            return await Task.Run(() => Context.Items?.AsEnumerable());
        }

        public async Task<Item?> GetItemAsync(int Id)
        {
            return await Task.Run<Item?>(() => Context.Items?
                                            .Where(item => item.Id == Id)
                                            .FirstOrDefault());
        }

        public async Task CreateItemAsync(Item ItemToCreate)
        {
            await Context.Items.AddAsync(ItemToCreate);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(Item ItemToUpdate)
        {    
            Context.Items?.Update(ItemToUpdate);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(Item ItemToDelete)
        {
            Context.Items?.Remove(ItemToDelete);
            await Context.SaveChangesAsync();
        }
    }
}