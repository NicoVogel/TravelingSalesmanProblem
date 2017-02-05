using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using TSP.Entities;

namespace TSP.Controls
{
    class TspTreeViewMap : DependencyObject
    {
        private int m_generation;
        private TspTreeViewMapInfos m_infos;
        private Map m_map;
        private Log m_log;


        #region Properties


        /// <summary>
        /// A reference to the log which defines the age
        /// </summary>
        public Log LogReference
        {
            get { return m_log; }
            set
            {
                m_log = value;
                Information.Age = m_log.Age;
            }
        }
        /// <summary>
        /// A reference to the map which defins the values
        /// </summary>
        public Map MapReference
        {
            get { return m_map; }
            set
            {
                m_map = value;
                Information.Intersections = m_map.IntersectionCount;
                Information.Fitness = m_map.Fitness;
                Information.Distance = m_map.Distance;
            }
        }
        /// <summary>
        /// Generation number
        /// </summary>
        public int Generation
        {
            get { return m_generation; }
            private set { m_generation = value; }
        }
        /// <summary>
        /// The informatin of this generation
        /// </summary>
        public TspTreeViewMapInfos Information
        {
            get
            {
                if (m_infos == null)
                {
                    m_infos = new TspTreeViewMapInfos();
                    m_infos.SetValue(DependencyProperty.RegisterAttached("Parent", typeof(TspTreeViewMapInfos), typeof(TspTreeViewMap)), this);
                }
                return m_infos;
            }
            private set { m_infos = value; }
        }


        #endregion


        public TspTreeViewMap(Map m, Log l)
        {
            MapReference = m;
            LogReference = l;
        }

    }
}
