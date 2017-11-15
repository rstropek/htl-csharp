using System.ComponentModel.DataAnnotations;

namespace AspNetMvcDemo.Models
{
    public class Hero
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Hero name is required")]
        [Display(Name = "Hero Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Hero's Real Name")]
        [StringLength(50)]
        public string RealName { get; set; }
    }
}
