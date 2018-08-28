using System.Collections.Generic;
using GeoLib.Core;
using GeoLib.Data.Entities;

namespace GeoLib.Data.RepositoryInterfaces
{
    public interface IStateRepository : IDataRepository<State>
    {
        State Get(string abbrev);
        IEnumerable<State> Get(bool primaryOnly);
    }
}
