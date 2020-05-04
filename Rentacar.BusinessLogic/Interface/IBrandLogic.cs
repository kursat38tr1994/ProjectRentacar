using System.Collections.Generic;
using Rentacar.DataAccess.Data.Dto.BrandDto;

namespace Rentacar.BusinessLogic
{
    public interface IBrandLogic
    {
        IEnumerable<readbranddto> GettAll();
        readbranddto CreateBrand(createbranddto createbranddto);
        readbranddto GetId(int? id);
    }
}