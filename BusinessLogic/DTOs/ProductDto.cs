using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required, MinLength(3, ErrorMessage = "Title must has at least 3 characters.")]
        [RegularExpression(@"^[A-Z].*", ErrorMessage = "Title must start with a capital letter.")]
        public string Title { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }

        [Range(0, 100)]
        public int Discount { get; set; }
        public int Quantity { get; set; }

        [MinLength(10), MaxLength(3000)]
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
