using System.ComponentModel.DataAnnotations;

namespace Asp.netcore_with_angular.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public required string ProductName { get; set; }
        public required string Price { get; set; }
        public required string Description { get; set; }
        public int? Rating { get; set; }
        public bool Status { get; set; }
    }
}