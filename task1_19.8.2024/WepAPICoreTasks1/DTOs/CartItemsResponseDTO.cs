namespace WepAPICoreTasks1.DTOs
{
    public class CartItemsResponseDTO
    {
       

        public int? CartId { get; set; }

        public int Quantity { get; set; }

        public ProductRsponseDTO? ProductRsponseDTO  { get; set; }
    }
}
