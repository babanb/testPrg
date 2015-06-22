using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Model;

namespace Repository.Implementations
{
    public class TimeZoneRepository : GenericRepository<TimeZonePet>
    {
        private static Dictionary<TimeZoneEnum, TimeZonePet> timeZoneInfoIds;

        public TimeZoneRepository(DbContext context) : base(context)
        {

        }

        public string GetTimeZoneInfoId(TimeZoneEnum id)
        {
            if (timeZoneInfoIds == null)
            {
                timeZoneInfoIds = GetAll().ToDictionary(t => t.Id);
            }
            return timeZoneInfoIds[id].TimeZoneInfoId;
        }
    }
}
