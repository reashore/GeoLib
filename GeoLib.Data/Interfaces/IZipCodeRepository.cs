using System.Collections.Generic;
using GeoLib.Core.Interfaces;
using GeoLib.Data.Entities;

namespace GeoLib.Data.Interfaces
{
    public interface IZipCodeRepository : IDataRepository<ZipCode>
    {
        ZipCode GetByZipCode(string zip);
        List<ZipCode> GetByState(string state);
        List<ZipCode> GetZipCodesForRange(ZipCode zip, int range);
    }
}
