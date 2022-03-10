using AspDemo.Api.DTOs;
using AspDemo.Api.Models;

namespace AspDemo.Api
{
    public static class Extensions
    {
        public static ItemDTO AsDTO(this Item item)
        {
            return new ItemDTO 
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedTime = item.CreatedTime
            };
        }
    }
}