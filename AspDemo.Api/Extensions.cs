using AspDemo.Api.DTOs;
using AspDemo.Api.Models;

namespace AspDemo.Api
{
    public static class Extensions
    {
        public static ItemDTO AsDTO(this Item Item)
        {
            return new ItemDTO 
            {
                Id = Item.Id,
                Name = Item.Name,
                Price = Item.Price,
                CreatedTime = Item.CreatedTime
            };
        }
    }
}