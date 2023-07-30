using System.ComponentModel.DataAnnotations;

namespace Shared.Entities.Units
{
    public class Partner : Entity
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
    }
}