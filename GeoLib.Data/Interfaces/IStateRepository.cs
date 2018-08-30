using System.Collections.Generic;
using GeoLib.Core.Interfaces;
using GeoLib.Data.Entities;

namespace GeoLib.Data.Interfaces
{
    public interface IStateRepository : IDataRepository<State>
    {
        State Get(string abbrev);
        IEnumerable<State> Get(bool primaryOnly);
    }
}
