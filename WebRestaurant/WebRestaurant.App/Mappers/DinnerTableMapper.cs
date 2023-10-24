using WebRestaurant.Domain.Entity;
using WebRestaurant.Shared.Dtos;

namespace WebRestaurant.App.Mappers
{
    public static class DinnerTableMapper
    {
        public static DinnerTableDto ToDto(this DinnerTable DinnerTable)
        {
            if (DinnerTable == null)
            {
                return null;
            }

            DinnerTableDto DinnerTableDto = new DinnerTableDto()
            {
                Id = DinnerTable.Id,
                Number = DinnerTable.Number
            };

            return DinnerTableDto;
        }
        public static DinnerTable ToEntity(this DinnerTableDto DinnerTableDto)
        {
            if (DinnerTableDto == null)
            {
                return null;
            }

            DinnerTable DinnerTable = new DinnerTable()
            {
                Id = DinnerTableDto.Id,
                Number = DinnerTableDto.Number
            };

            return DinnerTable;
        }
    }
}
