using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Entities
{
    public static class Extensions
    {
        /// <summary>
        /// Clone the current log
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static Log Clone(this Log log)
        {
            return new Log(log.Generation, log.Age, log.Fitness, log.Distance) { Intersections = log.Intersections };
        }
    }
}
