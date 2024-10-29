using System.ComponentModel.DataAnnotations;

namespace NipamInfotech_MachineTest.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string CategoryName { get; set; }

    }
}
