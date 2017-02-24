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
using TSP.Controls.TspTreeView;
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
        private bool m_actionWithMap;

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
                this.Dispatcher.Invoke(() =>
                {
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
                this.Dispatcher.Invoke(() =>
                {
                    val = this.rdiShort.IsChecked == null ? false : (bool)this.rdiShort.IsChecked; ;
                });
                return val;
            }
        }

        /// <summary>
        /// this will enable or disable buttons on the ui. It is used to enable them when loading a map.
        /// </summary>
        public bool ActivateActionsWithLoadedMap
        {
            get { return m_actionWithMap; }
            set
            {
                m_actionWithMap = value;
                this.rdiShort.IsEnabled = value;
                this.rdiBest.IsEnabled = value;
                this.btnRun.IsEnabled = value;
                this.btnStop.IsEnabled = value;
                this.btnSave.IsEnabled = value;
                this.btnClear.IsEnabled = value;
                this.chkIntersection.IsEnabled = value;
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
            ActivateActionsWithLoadedMap = false;

            var ctx = new TreeViewStructureViewModel();
            this.DataContext = ctx;
            PC.TreeView = ctx;
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
            catch (Exception ex)
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

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnClearMap(object sender, RoutedEventArgs e)
        {
            if (PC.HasUnsavedInformation == false ||
                PC.HasUnsavedInformation && MessageBox.Show(
                "There are unsaved results from processes. If you clear the map these results will be lost. Do you want to clear the map?",
                "Confirmation",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                TspCan.RemoveAllLines();
                TspCan.RemoveAllPoints();
                PC.ClearCurrentState();
                ActivateActionsWithLoadedMap = false;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // check if process is still running
            if (PC.ThreadsAreRunning && MessageBox.Show(
                "There are processes running. The current status will not be saved. Do you want to close the application?",
                "Confirmation",
                MessageBoxButton.YesNo) == MessageBoxResult.No)
                e.Cancel = true;

            // check if there are unsaved maps
            if (PC.HasUnsavedInformation && MessageBox.Show(
                "There are unsaved results from processes. If you close the application these results will be lost. Do you want to close the application?",
                "Confirmation",
                MessageBoxButton.YesNo) == MessageBoxResult.No)
                e.Cancel = true;
        }
        
        private void chkIntersection_Changed(object sender, RoutedEventArgs e)
        {
            TspCan.ShowIntersections = (bool)(sender as CheckBox).IsChecked;
            PC.Redraw();
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
