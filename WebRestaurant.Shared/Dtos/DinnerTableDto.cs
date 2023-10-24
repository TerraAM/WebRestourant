using System.ComponentModel;

namespace WebRestaurant.Shared.Dtos
{
    public class DinnerTableDto
    {
        public int Id { get; set; }
        [DisplayName("Номер столика")]
        public int Number { get; set; }
    }
}
