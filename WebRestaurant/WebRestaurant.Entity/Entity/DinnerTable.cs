using System.ComponentModel;

namespace WebRestaurant.Domain.Entity
{
    public class DinnerTable
    {
        public int Id { get; set; }
        [DisplayName("Номер столика")]
        public int Number { get; set; }
    }
}
