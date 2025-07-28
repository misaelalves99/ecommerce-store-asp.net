namespace ECommerceStore.Models
{
    public class Image : BaseEntity
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }  // Tornado anulável

        public string Url { get; set; } = null!;
        public bool IsMain { get; set; }  // Imagem principal do produto
    }
}
