using DataModel;
using DataModel.Temp;
using NLog;
using System.Linq;
using System.Windows.Input;
using Voortman.Handlers;

namespace Voortman.ViewModels
{
    public class TemperatureViewModel : BaseViewModel
    {
        #region private declaration

        private string temperatureLabelText;
        private string temperatureButtonText;
        private string temperatureUnit;
        private double temperatureValue;

        #endregion

        #region command declaration

        private ICommand temperatureButtonCommand;

        #endregion

        #region constructors

        public TemperatureViewModel(Model model) : base(model)
        {
            logger = LogManager.GetLogger("TemperatureViewModel");

            //Force relocalize to set translations and units
            Relocalize();
        }

        #endregion

        #region public declaration

        public string TemperatureLabelText
        {
            get
            {
                return temperatureLabelText;
            }
            set
            {
                temperatureLabelText = value;
                OnPropertyChanged("TemperatureLabelText");
            }
        }

        public string TemperatureUnit
        {
            get
            {
                return temperatureUnit;
            }
            set
            {
                temperatureUnit = value;
                OnPropertyChanged("TemperatureUnit");
            }
        }

        public double TemperatureValue
        {
            get
            {
                return temperatureValue;
            }
            set
            {
                temperatureValue = value;
                OnPropertyChanged("TemperatureValue");
            }
        }

        public string TemperatureButtonText
        {
            get
            {
                return temperatureButtonText;
            }
            set
            {
                temperatureButtonText = value;
                OnPropertyChanged("TemperatureButtonText");
            }
        }

        #endregion

        #region commands

        public ICommand TemperatureButtonCommand
        {
            get
            {
                return temperatureButtonCommand ?? (temperatureButtonCommand = new CommandHandler(() => CalculateCommand(), () => CanExecute));
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Calculate temperature value
        /// </summary>
        private void CalculateCommand()
        {
            logger.Log(LogLevel.Info, "Calculate temperature ranking");

            if(model?.TemperatureRankings?.Rankings != null && model.TemperatureRankings.Rankings.Count > 0)
            {
                Statistics<Temperature> temperatureRankings = model.TemperatureRankings;
                Temperature temperature = temperatureRankings.Rankings.OrderBy(x => x.Difference).FirstOrDefault();

                TemperatureValue = temperature.Id;
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
            TemperatureButtonText = "Calculate";
            TemperatureLabelText = "Temperature day number:";
            TemperatureUnit = "°";

            OnPropertyChanged("TemperatureLabelText");
            OnPropertyChanged("TemperatureButtonText");
            OnPropertyChanged("TemperatureValue");
            OnPropertyChanged("TemperatureUnit");
        }

        #endregion
    }
}
