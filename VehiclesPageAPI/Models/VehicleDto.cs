using System.ComponentModel.DataAnnotations;

namespace VehiclesPageAPI.Models
{
    public class VehicleDto
    {
        [Required, MaxLength(15)]
        public string Make { get; set; } = "";

        [Required, MaxLength(50)]
        public string Model { get; set; } = "";

        [Required, MaxLength(15)]
        public string Category { get; set; } = "";

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; } = "";
        public IFormFile? ImageFile { get; set; }
    }
}
