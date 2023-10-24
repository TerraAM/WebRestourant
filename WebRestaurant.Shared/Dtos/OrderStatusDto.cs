using System.ComponentModel;

namespace WebRestaurant.Shared.Dtos
{
    public class OrderStatusDto
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; } = string.Empty;
    }
}
