using System;

namespace AspDemo.Api.Models
{
    public class Item
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedTime { get; init; }
    }
}