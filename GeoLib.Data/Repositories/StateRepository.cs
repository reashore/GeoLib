using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GeoLib.Core;
using GeoLib.Data.Entities;
using GeoLib.Data.RepositoryInterfaces;

namespace GeoLib.Data.Repositories
{
    public class StateRepository : DataRepositoryBase<State, GeoLibDbContext>, IStateRepository
    {
        protected override DbSet<State> DbSet(GeoLibDbContext entityContext)
        {
            return entityContext.StateSet;
        }

        protected override Expression<Func<State, bool>> IdentifierPredicate(GeoLibDbContext entityContext, int id)
        {
            return e => e.StateId == id;
        }

        public State Get(string abbrev)
        {
            using (GeoLibDbContext geoLibDbContext = new GeoLibDbContext())
            {
                // ReSharper disable once SpecifyStringComparison
                return geoLibDbContext.StateSet.FirstOrDefault(e => e.Abbreviation.ToLower() == abbrev.ToLower());
            }
        }

        public IEnumerable<State> Get(bool primaryOnly)
        {
            using (GeoLibDbContext geoLibDbContext = new GeoLibDbContext())
            {
                return geoLibDbContext.StateSet.Where(e => e.IsPrimaryState == primaryOnly).ToList();
            }
        }
    }
}
