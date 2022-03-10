using System;

namespace AspDemo.Api.DTOs
{
    public record ItemDTO
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedTime { get; set; }
    }
}