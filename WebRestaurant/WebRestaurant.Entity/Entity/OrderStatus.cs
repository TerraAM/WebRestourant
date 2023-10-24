using System.ComponentModel;

namespace WebRestaurant.Domain.Entity
{
    public class OrderStatus
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; } = string.Empty;
    }
}
