using System.ComponentModel.DataAnnotations;

namespace RestoranTestTechinal.Models
{
    public class NewMenusVM
    {
        public int Id { get; set; }

        [Display(Name = "Menu name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Menu description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in Rp")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Menu poster URL")]
        [Required(ErrorMessage = "Menu poster URL is required")]
        public string ImageURL { get; set; }
        [Display(Name = "Menu Category")]
        [Required(ErrorMessage = "Menu Category is required")]
        public string Category { get; set; }

        [Display(Name = "Status Menu")]
        [Required(ErrorMessage = "Status Menu is required")]
        public bool IsInStock { get; set; }




    }
}
