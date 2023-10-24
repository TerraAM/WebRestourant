using WebRestaurant.Domain.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.App.Mappers
{
    public static class RoleMapper
    {
        public static RoleDto ToDto(this Role Role)
        {
            if (Role == null)
            {
                return null;
            }

            RoleDto RoleDto = new RoleDto()
            {
                Id = Role.Id,
                Name = Role.Name
            };

            return RoleDto;
        }
        public static Role ToEntity(this RoleDto RoleDto)
        {
            if (RoleDto == null)
            {
                return null;
            }

            Role Role = new Role()
            {
                Id = RoleDto.Id,
                Name = RoleDto.Name
            };

            return Role;
        }
    }
}
