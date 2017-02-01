using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace TSP.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LineCollection m_lines;
        List<Ellipse> m_points;


        #region Properties


        /// <summary>
        /// Public accessor
        /// </summary>
        public LineCollection Lines
        {
            get
            {
                if (m_lines == null)
                    m_lines = new LineCollection(this.canvas);
                return m_lines;
            }
            private set { m_lines = value; }
        }
        /// <summary>
        /// Public accessor
        /// </summary>
        public List<Ellipse> Points
        {
            get
            {
                if (m_points == null)
                    m_points = new List<Ellipse>();
                return m_points;
            }
            private set { m_points = value; }
        }
        /// <summary>
        /// Public accessor
        /// </summary>
        public Canvas ObjCanvas { get { return this.canvas; } }

        #endregion


        public MainWindow()
        {
            InitializeComponent();
        }


        #region Events


        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {

        }


        #endregion


        public Ellipse CreatePoint(TSP.Entities.Point p)
        {
            var e = new Ellipse();
            e.Width = 5;
            e.Height = 5;
            e.StrokeThickness = 0;
            e.Fill = Brushes.Red;
            return e;
        }
        
    }
}
