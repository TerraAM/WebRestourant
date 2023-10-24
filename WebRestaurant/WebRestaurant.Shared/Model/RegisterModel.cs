using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRestaurant.Shared.Model
{
    public class RegisterModel
    {
        [DisplayName("Псевдоним")]
        [Required(ErrorMessage = "Не указан псевдоним")]
        public string Name { get; set; }
        [DisplayName("Почта")]
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Повторный пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
