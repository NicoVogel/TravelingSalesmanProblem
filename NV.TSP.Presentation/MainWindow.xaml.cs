using Microsoft.Win32;
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

using TSP.Business;
using TSP.Entities.Controls;
using TSP.Interfaces.Business;
using TSP.Interfaces.Presentation;

namespace TSP.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        private IPresentationController m_pc;


        #region Properties


        /// <summary>
        /// public accessor
        /// </summary>
        public IPresentationController PC
        {
            get
            {
                if (m_pc == null)
                    m_pc = new PresentationController(this);
                return m_pc;
            }
            private set { m_pc = value; }
        }


        #region Interface Properties


        /// <summary>
        /// public accessor
        /// </summary>
        public ITspCanvas TspCan
        {
            get { return this.canvas; }
        }
        /// <summary>
        /// get the checked value from the raidiobutton which is responsible for displaying the best map
        /// </summary>
        public bool BestIsSelected
        {
            get
            {
                bool val = false;
                this.Dispatcher.Invoke(()=>{
                    val = this.rdiBest.IsChecked == null ? false : (bool)this.rdiBest.IsChecked;
                });
                return val;
            }
        }
        /// <summary>
        /// get the checked value from the raidiobutton which is responsible for displaying the shortest map
        /// </summary>
        public bool ShortestIsSelected
        {
            get
            {
                bool val = false;
                this.Dispatcher.Invoke(() => {
                    val = this.rdiShort.IsChecked == null ? false : (bool)this.rdiShort.IsChecked; ;
                });
                return val;
            }
        }


        #endregion

        #endregion


        public MainWindow()
        {
            InitializeComponent();
            int width = Console.WindowWidth + 40;
            Console.SetWindowSize(width, Console.WindowHeight);

            this.rdiShort.IsChecked = true;
        }


        #region Events


        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ofd = new OpenFileDialog();
                ofd.Filter = "Text Files (*.txt)|*" + PC.PointExtension;
                if (ofd.ShowDialog() == true)
                {
                    PC.LoadFilePoints(ofd.FileName);
                }
            }
            catch(Exception ex)
            {
                handleException(ex);
            }
        }

        private void btnLoadMap_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ofd = new OpenFileDialog();
                ofd.Filter = "Map Files (*.map)|*" + PC.MapExtension;
                if (ofd.ShowDialog() == true)
                {
                    PC.LoadFileMap(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                handleException(ex);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var sfd = new SaveFileDialog();
                sfd.Filter = "Map Files (*.map)|*" + PC.MapExtension;
                if (sfd.ShowDialog() == true)
                    PC.SaveFile(sfd.FileName);
            }
            catch (Exception ex)
            {
                handleException(ex);
            }
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PC.Run();
            }
            catch (Exception ex)
            {
                handleException(ex);
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PC.Stop();
            }
            catch (Exception ex)
            {
                handleException(ex);
            }
        }
        
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // check if process is still running
            // ask for saving befor closing
        }


        #endregion



        private void handleException(Exception ex)
        {
            string msg = "Exception:\n";
            Exception e = ex;
            do
            {
                msg += e.Message;
                if (e.InnerException != null)
                {
                    e = e.InnerException;
                    msg += "\n\nInnerException:\n";
                }
                else
                    e = null;
            } while (e != null);
            
            MessageBox.Show(msg);
        }
    }
}
