using System.Collections.Generic;
using Rentacar.DataAccess.Data.Dto.BrandDto;

namespace Rentacar.BusinessLogic
{
    public interface IBrandLogic
    {
        IEnumerable<ReadBrandDto> GettAll();
        ReadBrandDto Upsert(ReadBrandDto createbranddto);
        ReadBrandDto GetId(int? id);
    }
}