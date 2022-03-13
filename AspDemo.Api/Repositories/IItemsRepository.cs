using System.Collections.Generic;
using System.Threading.Tasks;
using AspDemo.Api.Models;

namespace AspDemo.Api.Repositories
{
    public interface IItemsRepository
    {
        Task<Item?> GetItemAsync(int Id);
        Task<IEnumerable<Item>?> GetItemsAsync();
        Task CreateItemAsync(Item ItemToCreate);
        Task UpdateItemAsync(Item ItemToUpdate);
        Task DeleteItemAsync(Item ItemToDelete);
    }
}
    