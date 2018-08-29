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
    public class ZipCodeRepository : DataRepositoryBase<ZipCode, GeoLibDbContext>, IZipCodeRepository
    {
        protected override DbSet<ZipCode> DbSet(GeoLibDbContext entityContext)
        {
            return entityContext.ZipCodeSet;
        }

        protected override Expression<Func<ZipCode, bool>> IdentifierPredicate(GeoLibDbContext entityContext, int id)
        {
            return e => e.ZipCodeId == id;
        }

        public override IEnumerable<ZipCode> Get()
        {
            using (GeoLibDbContext entityContext = new GeoLibDbContext())
            {
                return entityContext.ZipCodeSet.Include(e => e.State).ToList();
            }
        }

        public ZipCode GetByZipCode(string zip)
        {
            using (GeoLibDbContext geoLibDbContext = new GeoLibDbContext())
            {
                return geoLibDbContext.ZipCodeSet.Include(e => e.State).FirstOrDefault(e => e.Zip == zip);
            }
        }

        public IEnumerable<ZipCode> GetByState(string state)
        {
            using (GeoLibDbContext geoLibDbContext = new GeoLibDbContext())
            {
                return geoLibDbContext.ZipCodeSet
                    .Include(e => e.State)
                    .Where(e => e.State.Abbreviation == state).ToList(); 
            }
        }

        public IEnumerable<ZipCode> GetZipCodesForRange(ZipCode zip, int range)
        {
            using (GeoLibDbContext geoLibDbContext = new GeoLibDbContext())
            {
                double degrees = range / 69.047;

                return geoLibDbContext.ZipCodeSet
                    .Include(e => e.State)
                    .Where(e => zip.Latitude - degrees <= e.Latitude && e.Latitude <= zip.Latitude + degrees && 
                                zip.Longitude - degrees <= e.Longitude && e.Longitude <= zip.Longitude + degrees)
                    .ToList();
            }
        }
    }
}
