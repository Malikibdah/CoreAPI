namespace WepAPICoreTasks1.DTOs
{
    public class ProductDTOcs
    {
       
        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public int? Price { get; set; }

        public int? CategoryId { get; set; }

        public IFormFile? ProductImage { get; set; }
    }
}
