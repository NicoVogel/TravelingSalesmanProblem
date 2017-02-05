using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TSP.Controls
{
    class TspTreeViewMapInfos : DependencyObject
    {

        private int m_age;
        private double m_distance;
        private int m_fitness;
        private int m_intersections;


        #region Properties
        

        /// <summary>
        /// Amount of intrseptions in this genration
        /// </summary>
        public int Intersections
        {
            get { return m_intersections; }
            set { m_intersections = value; }
        }
        /// <summary>
        /// Fitness of this generation
        /// </summary>
        public int Fitness
        {
            get { return m_fitness; }
            set { m_fitness = value; }
        }
        /// <summary>
        /// Total distance of this generation
        /// </summary>
        public double Distance
        {
            get { return m_distance; }
            set { m_distance = value; }
        }
        /// <summary>
        /// Age of this generation
        /// </summary>
        public int Age
        {
            get { return m_age; }
            set { m_age = value; }
        }


        #endregion



        public TspTreeViewMapInfos()
        {

        }
    }
}
