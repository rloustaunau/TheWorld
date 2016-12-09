using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldContextSeedData
    {
        private WorldContext _context;

        public WorldContextSeedData(WorldContext context)
        {
            _context = context;
        }

        public async Task EnsureSeedData()
        {
            if (!_context.Trips.Any())
            {
                var usTrip = new Trip()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "US Trip",
                    UserName = "",  //TODO Add UserName
                    Stops = new List<Stop>()
                    {
                        new Stop() { Name = "Atlanta, GA", Arrival = new DateTime(2016,6,4), Latitude =33.74831, Longitude=-84.2317, Order=0 },
                        new Stop() { Name = "New York, NY", Arrival = new DateTime(2016,6,4), Latitude =40.712784, Longitude=-74.005941, Order=1 }
                    }
                };

                _context.Trips.Add(usTrip);

                _context.Stops.AddRange(usTrip.Stops);

                var worldTrip = new Trip()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "WorldTrip",
                    UserName = "",  //TODO Add UserName
                    Stops = new List<Stop>()
                    {
                        new Stop() { Name = "Mexico, MX", Arrival = new DateTime(2016,6,4), Latitude =19.43195, Longitude=-99.13313, Order=3 },
                        new Stop() { Name = "buenos Aires, AR", Arrival = new DateTime(2016,6,4), Latitude =34.60851, Longitude=-58.37349, Order=4 }
                    }
                };
                _context.Trips.Add(worldTrip);

                _context.Stops.AddRange(worldTrip.Stops);

                await _context.SaveChangesAsync();
            }
        }
    }
}
