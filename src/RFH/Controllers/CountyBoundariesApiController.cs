using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using RFH.Infrastructure;

namespace RFH.Controllers
{
    public class Coordinate
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class CountyBoundariesApiController : ApiController
    {
        private readonly DataContext _dataContext = new DataContext();

        public IEnumerable<Coordinate> Get(int id)
        {
            var rv = new List<Coordinate>();

            var countyPoints = _dataContext.Counties.Single(t => t.Id == id).BoundaryPointDataString;

            var regex = new Regex(@"(\-?\d+(\.\d+)?),\s*(\-?\d+(\.\d+)?)");

            var matches = regex.Matches(countyPoints);

            foreach (var match in matches)
            {
                var s = match.ToString();
                var index = s.IndexOf(',');
                var lat = double.Parse(s.Substring(index + 1));
                var lng = double.Parse(s.Substring(0, index));

                var coord = new Coordinate {Lat = lat, Lng = lng};
                rv.Add(coord);
            }

            return rv;
        }
    }
}
