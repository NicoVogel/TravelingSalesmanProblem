using System.ComponentModel;
using PropertyChanged;

namespace TSP.Controls
{
    /// <summary>
    /// This is the base view model which implements the property changed event
    /// </summary>
    [ImplementPropertyChanged]
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// This event is called when a property is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
