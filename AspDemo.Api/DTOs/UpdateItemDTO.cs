using System.ComponentModel.DataAnnotations;

namespace AspDemo.Api.DTOs
{
    public record UpdateItemDTO
    {
        [Required]
        public string? Name { get; init; }
        [Required]
        [Range(50000, 250000)]
        public decimal Price { get; set; }
    }
}