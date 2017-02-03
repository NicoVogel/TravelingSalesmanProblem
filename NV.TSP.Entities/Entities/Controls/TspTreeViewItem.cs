using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSP.Entities;

namespace TSP.Entities.Entities.Controls
{
    class TspTreeViewItem
    {
        private int m_generation;
        private int m_age;
        private double m_distance;
        private int m_fitness;
        private int m_intersections;
        private Map m_map;
        private Log m_log;
        

        #region Properties


        /// <summary>
        /// A reference to the log which defines the age
        /// </summary>
        public Log LogReference
        {
            get { return m_log; }
            private set
            {
                m_log = value;
                Age = m_log.Age;
            }
        }
        /// <summary>
        /// A reference to the map which defins the values
        /// </summary>
        public Map MapReference
        {
            get { return m_map; }
            private set
            {
                m_map = value;
                Intersections = m_map.Intersection;
                Fitness = m_map.Fitness;
                Distance = m_map.Distance;
            }
        }
        /// <summary>
        /// Amount of intrseptions in this genration
        /// </summary>
        public int Intersections
        {
            get { return m_intersections; }
            private set { m_intersections = value; }
        }
        /// <summary>
        /// Fitness of this generation
        /// </summary>
        public int Fitness
        {
            get { return m_fitness; }
            private set { m_fitness = value; }
        }
        /// <summary>
        /// Total distance of this generation
        /// </summary>
        public double Distance
        {
            get { return m_distance; }
            private set { m_distance = value; }
        }
        /// <summary>
        /// Age of this generation
        /// </summary>
        public int Age
        {
            get { return m_age; }
            private set { m_age = value; }
        }
        /// <summary>
        /// Generation number
        /// </summary>
        public int Generation
        {
            get { return m_generation; }
            private set { m_generation = value; }
        }


        #endregion

        public TspTreeViewItem(Map m, Log l)
        {

        }

    }
}
