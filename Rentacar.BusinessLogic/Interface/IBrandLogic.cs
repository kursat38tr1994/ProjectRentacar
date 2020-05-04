using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentacar.DataAccess.Data.Dto.BrandDto;

namespace Rentacar.BusinessLogic
{
    public interface IBrandLogic
    {
        IEnumerable<ReadBrandDto> GettAll();
        ReadBrandDto Upsert(ReadBrandDto createbranddto);
        ReadBrandDto GetId(int? id);
        ReadBrandDto Delete(int? id);
        IEnumerable<SelectListItem> GetDropDown();
    }
}