using System;

namespace AspDemo.Api.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
    }
}