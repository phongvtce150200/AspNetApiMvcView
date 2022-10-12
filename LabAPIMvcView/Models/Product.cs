using System.ComponentModel.DataAnnotations;

namespace LabAPIMvcView.Models
{
    public class Product
    {
        [Key]
        public int productId { get; set; }
        [Required]
        public string productName { get; set; }
        [Required]
        public string weight { get; set; }
        [Required]
        public int unitPrice { get; set; }
        [Required]
        public int unitslnStock { get; set; }
    }
}