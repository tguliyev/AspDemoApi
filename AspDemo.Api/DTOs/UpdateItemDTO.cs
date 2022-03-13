using System.ComponentModel.DataAnnotations;

namespace AspDemo.Api.DTOs
{
    public class UpdateItemDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        [Range(50000, 250000)]
        public decimal Price { get; set; }
    }
}