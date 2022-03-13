using System;

namespace AspDemo.Api.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
    }
}