using System;
using System.Windows.Input;

namespace TSP.Controls.TspTreeView
{
    /// <summary>
    /// a basic command that runs an Action
    /// </summary>
    public class RelayCommand : ICommand
    {

        #region Private Members


        /// <summary>
        /// This is the action to run
        /// </summary>
        private Action m_action;


        #endregion

        #region Public Events


        /// <summary>
        /// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// default constructor
        /// </summary>
        public RelayCommand(Action action)
        {
            m_action = action;
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// a relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Execute the command Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            m_action();
        }

        #endregion

    }
}
