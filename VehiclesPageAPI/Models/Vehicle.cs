using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VehiclesPageAPI.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [MaxLength(15)]
        public string Make { get; set; } = "";

        [MaxLength(50)]
        public string Model { get; set; } = "";

        [MaxLength(15)]
        public string Category { get; set; } = "";

        [Precision(16, 2)]
        public decimal Price { get; set; }
        public string Description { get; set; } = "";

        [MaxLength(100)]
        public string ImageFileName { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
