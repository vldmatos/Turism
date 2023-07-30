using System.ComponentModel.DataAnnotations;

namespace Shared.Entities
{
    public abstract class Entity
    {
        #region Properties

        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdateAt { get; set; }

        public bool Enabled { get; set; } = true;

        #endregion Properties
    }
}