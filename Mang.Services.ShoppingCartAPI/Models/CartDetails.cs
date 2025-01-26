using Mang.Services.ShoppingCartAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mang.Services.ShoppingCartAPI.Models
{
    public class CartDetails
    {
        [Key]
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public ProductDto product { get; set; }
        public int Count { get; set; }
    }
}
