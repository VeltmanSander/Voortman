using DataModel;
using NLog;
using System.ComponentModel;

namespace Voortman.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region protected declaration

        protected static Logger logger;
        protected Model model;

        #endregion

        #region events declaration

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region constructors
        
        public BaseViewModel(Model model)
        {
            this.model = model;
        }

        #endregion

        #region events

        protected void OnPropertyChanged(string propName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Refresh all text 
        /// </summary>
        protected virtual void Relocalize()
        {

        }

        #endregion
    }
}
