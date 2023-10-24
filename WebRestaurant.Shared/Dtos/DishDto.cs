using System.ComponentModel;

namespace WebRestaurant.Shared.Dtos
{
    public class DishDto
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Цена")]
        public double Price { get; set; }
        [DisplayName("Вес (г)")]
        public double Weight { get; set; }
        [DisplayName("Картинка")]
        public string PhotoPath { get; set; } = string.Empty;
        [DisplayName("Описание")]
        public string Description { get; set; } = string.Empty;
    }
}
