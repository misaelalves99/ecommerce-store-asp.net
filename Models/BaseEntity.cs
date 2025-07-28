using System;

namespace ECommerceStore.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } // Propriedade UpdatedAt pode ser nula, o que é comum antes da primeira atualização.
    }
}
