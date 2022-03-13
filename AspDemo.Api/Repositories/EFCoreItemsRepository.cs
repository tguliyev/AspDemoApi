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
        private IDataContext context;
        public EFCoreItemsRepository(IDataContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<Item>?> GetItemsAsync()
        {
            return await Task.Run(() => context.Items?.AsEnumerable());
        }

        public async Task<Item?> GetItemAsync(int id)
        {
            Item? item = await Task.Run<Item?>(() => context.Items?
                                            .Where(item => item.Id == id)
                                            .FirstOrDefault());

            return item;
        }

        public async Task CreateItemAsync(Item item)
        {
            await context.Items.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {    
            context.Items.Update(item);

            await context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            Item? deletingItem = await context.Items.FindAsync(id);

            context.Items?.Remove(deletingItem);
            await context.SaveChangesAsync();
        }
    }
}