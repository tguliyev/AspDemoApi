using System.Collections.Generic;
using System.Threading.Tasks;
using AspDemo.Api.Models;

namespace AspDemo.Api.Repositories
{
    public interface IItemsRepository
    {
        Task<Item?> GetItemAsync(int id);
        Task<IEnumerable<Item>?> GetItemsAsync();
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int id);
    }
}
    