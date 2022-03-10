using System;

namespace AspDemo.Api.Models
{
    public class Item
    {
        // private static int idCounter = 0;
        public int Id { get; init; }
        public string? Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedTime { get; init; }

        // public Item()
        // {
        //     Id = ++idCounter;
        //     CreatedTime = DateTimeOffset.UtcNow;
        // }
    }
}