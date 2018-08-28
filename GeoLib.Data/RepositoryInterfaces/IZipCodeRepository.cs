using System.Collections.Generic;
using GeoLib.Core;
using GeoLib.Data.Entities;

namespace GeoLib.Data.RepositoryInterfaces
{
    public interface IZipCodeRepository : IDataRepository<ZipCode>
    {
        ZipCode GetByZip(string zip);
        IEnumerable<ZipCode> GetByState(string state);
        IEnumerable<ZipCode> GetZipsForRange(ZipCode zip, int range);
    }
}
