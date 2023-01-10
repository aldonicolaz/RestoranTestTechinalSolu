using System.ComponentModel.DataAnnotations;

namespace RestoranTestTechinal.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public Menus Menus { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
        public string? NomorMeja { get; set; }
    }
}
