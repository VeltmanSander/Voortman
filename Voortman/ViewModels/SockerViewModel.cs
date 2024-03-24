using DataModel;
using DataModel.Football;
using NLog;
using System.Linq;
using System.Windows.Input;
using Voortman.Handlers;

namespace Voortman.ViewModels
{
    public class SockerViewModel : BaseViewModel
    {
        #region private declaration

        private string sockerLabelText;
        private string sockerButtonText;
        private string sockerValue;

        #endregion

        #region command declaration

        private ICommand sockerButtonCommand;

        #endregion

        #region constructors

        public SockerViewModel(Model model) : base(model)
        {
            logger = LogManager.GetLogger("SockerViewModel");

            //Force relocalize to set translations and units
            Relocalize();
        }

        #endregion

        #region commands

        public ICommand SockerButtonCommand
        {
            get
            {
                return sockerButtonCommand ?? (sockerButtonCommand = new CommandHandler(() => CalculateCommand(), () => CanExecute));
            }
        }

        #endregion

        #region public declaration

        public string SockerLabelText
        {
            get
            {
                return sockerLabelText;
            }
            set
            {
                sockerLabelText = value;
                OnPropertyChanged("SockerLabelText");
            }
        }

        public string SockerValue
        {
            get
            {
                return sockerValue;
            }
            set
            {
                sockerValue = value;
                OnPropertyChanged("SockerValue");
            }
        }

        public string SockerButtonText
        {
            get
            {
                return sockerButtonText;
            }
            set
            {
                sockerButtonText = value;
                OnPropertyChanged("SockerButtonText");
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Calculate temperature value
        /// </summary>
        private void CalculateCommand()
        {
            logger.Log(LogLevel.Info, "Calculate socker ranking");

            if (model?.SockerRankings?.Rankings != null && model.SockerRankings.Rankings.Count > 0)
            {
                Statistics<Socker> sockerRankings = model.SockerRankings;
                Socker socker = sockerRankings.Rankings.OrderBy(i => i.Difference).FirstOrDefault();

                SockerValue = socker.Name;
            }
        }

        /// <summary>
        /// Check if user may execute the function
        /// </summary>
        public bool CanExecute
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Refresh all text 
        /// </summary>
        protected override void Relocalize()
        {
            base.Relocalize();

            //normally based on culture and region
            SockerButtonText = "Calculate";
            SockerLabelText = "Socker team name:";

            OnPropertyChanged("SockerLabelText");
            OnPropertyChanged("SockerButtonText");
            OnPropertyChanged("SockerValue");
        }

        #endregion
    }
}
