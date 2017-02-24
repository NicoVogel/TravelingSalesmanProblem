using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Controls.TspTreeView
{
    public class TreeViewSettings
    {
        private string m_titleGenration;
        private string m_titleFitness;
        private string m_titleDistance;
        private string m_titleAge;
        private string m_titleIntersections;
        


        public string TitleIntersections
        {
            get { return m_titleIntersections; }
            set { m_titleIntersections = value; }
        }


        public string TitleAge
        {
            get { return m_titleAge; }
            set { m_titleAge = value; }
        }


        public string TitleDistance
        {
            get { return m_titleDistance; }
            set { m_titleDistance = value; }
        }


        public string TitleFitness
        {
            get { return m_titleFitness; }
            set { m_titleFitness = value; }
        }


        public string TitleGenration
        {
            get { return m_titleGenration; }
            set { m_titleGenration = value; }
        }










    }
}
