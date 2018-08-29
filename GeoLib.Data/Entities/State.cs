using GeoLib.Core;
using GeoLib.Core.Interfaces;

namespace GeoLib.Data.Entities
{
    public class State : IIdentifiableEntity
    {
        public int StateId { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public bool IsPrimaryState { get; set; }

        public int EntityId
        {
            get => StateId;
            set => StateId = value;
        }
    }
}
