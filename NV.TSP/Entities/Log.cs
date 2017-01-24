using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Entities
{
    /// <summary>
    /// Diese Klasse wird zu sammeln von Informationen verwendet.
    /// </summary>
    public class Log
    {
        private int m_gen;
        private int m_fit;
        private double m_dis;
        private int m_age;
        private int m_inter;


        #region Properties


        /// <summary>
        /// Wie viele generationen schlechter waren bis diese entstanden ist
        /// </summary>
        public int Age
        {
            get { return m_age; }
            set { m_age = value; }
        }
        /// <summary>
        /// Die gesamt Strecke von dieser Generation
        /// </summary>
        public double Distance
        {
            get { return m_dis; }
            set { m_dis = value; }
        }
        /// <summary>
        /// Die Fitness von dieser Generations
        /// </summary>
        public int Fitness
        {
            get { return m_fit; }
            set { m_fit = value; }
        }
        /// <summary>
        /// Die Generations nummer
        /// </summary>
        public int Generation
        {
            get { return m_gen; }
            set { m_gen = value; }
        }

        /// <summary>
        /// Die Generations nummer
        /// </summary>
        public int Intersections
        {
            get { return m_inter; }
            set { m_inter = value; }
        }
        public string Text
        {
            get { return GetText(); }
        }


        #endregion


        /// <summary>
        /// Ertsellt einen neuen Log über die naechst bessere Generation
        /// </summary>
        public Log()
        {
            Age = 0;
        }
        /// <summary>
        /// Ertsellt einen neuen Log über die naechst bessere Generation
        /// </summary>
        /// <param name="generation"></param>
        /// <param name="age"></param>
        public Log(int generation, int age) : this()
        {
            Generation = generation;
            Age = age;
        }
        /// <summary>
        /// Ertsellt einen neuen Log über die naechst bessere Generation
        /// </summary>
        /// <param name="generation"></param>
        /// <param name="fitness"></param>
        /// <param name="distance"></param>
        /// <param name="age"></param>
        public Log(int generation, int age, int fitness, double distance) : this(generation, age)
        {
            Fitness = fitness;
            Distance = distance;
        }


        public string GetText()
        {
            return String.Format("Generation: {0,-5}\tDistance: {1}\tFitness: {2,-9}\tAge: {3,-4}\tIntersection: {4,-4}", Generation, Distance, Fitness, Age, Intersections);
        }

    }
}
