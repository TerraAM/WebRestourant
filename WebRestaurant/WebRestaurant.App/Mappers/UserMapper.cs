using WebRestaurant.Domain.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.App.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User User)
        {
            if (User == null)
            {
                return null;
            }

            UserDto UserDto = new UserDto()
            {
                Id = User.Id,
                Name = User.Name,
                Email = User.Email,
                Password = User.Password,
                RoleId = User.RoleId,
                Role = User.Role.ToDto()
            };

            return UserDto;
        }
        public static User ToEntity(this UserDto UserDto)
        {
            if (UserDto == null)
            {
                return null;
            }

            User User = new User()
            {
                Id = UserDto.Id,
                Name = UserDto.Name,
                Email = UserDto.Email,
                Password = UserDto.Password,
                RoleId = UserDto.RoleId,
                Role = UserDto.Role.ToEntity()
            };

            return User;
        }
    }
}
