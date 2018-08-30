using System.Collections.Generic;
using GeoLib.Core.Interfaces;
using GeoLib.Data.Entities;

namespace GeoLib.Data.RepositoryInterfaces
{
    public interface IZipCodeRepository : IDataRepository<ZipCode>
    {
        ZipCode GetByZipCode(string zip);
        IEnumerable<ZipCode> GetByState(string state);
        IEnumerable<ZipCode> GetZipCodesForRange(ZipCode zip, int range);
    }
}
