using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentacar.DataAccess.Dto.BrandDto;

namespace Rentacar.BusinessLogic
{
    public interface IBrandLogic
    {
        IEnumerable<BrandDto> GettAll();
        BrandDto Upsert(BrandDto createbranddto);
        BrandDto GetId(int? id);
        BrandDto Delete(int? id);
        IEnumerable<SelectListItem> GetDropDown();
    }
}