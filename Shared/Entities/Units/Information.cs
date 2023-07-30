using System.ComponentModel.DataAnnotations;

namespace Shared.Entities.Units
{
    public class Information : Entity
    {
        [Required]
        [StringLength(50, ErrorMessage = "Title is too long.")]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public List<Category> Categories { get; set; }

        public List<string> Tops { get; set; } = new List<string>();

        public record Category(string Name, string Description);
    }
}