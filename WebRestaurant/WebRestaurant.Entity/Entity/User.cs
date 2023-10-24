using ServiceStack.DataAnnotations;
using System.ComponentModel;

namespace WebRestaurant.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        [DisplayName("Псевдоним")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Почта")]
        public string Email { get; set; } = string.Empty;
        [DisplayName("Пароль")]
        public string Password { get; set; } = string.Empty;
        [DisplayName("Роль")]
        public int RoleId { get; set; }
        [DisplayName("Роль")]
        public Role Role { get; set; }
    }
}
