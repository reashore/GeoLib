using System.Collections.Generic;
using GeoLib.Core.Interfaces;
using GeoLib.Data.Entities;

namespace GeoLib.Data.Interfaces
{
    public interface IStateRepository : IDataRepository<State>
    {
        State Get(string abbrev);
        List<State> Get(bool primaryOnly);
    }
}
