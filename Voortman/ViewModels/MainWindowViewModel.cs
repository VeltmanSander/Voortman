using DataAccess.FileAccess;
using DataModel;
using NLog;

namespace Voortman.ViewModels
{
    public class MainWindowViewModel
    {
        #region private declaration

        protected static Logger logger;
        private Model model;
        private int selectedStatisticsTab;

        #endregion

        #region constructors

        public MainWindowViewModel()
        {
            logger = LogManager.GetLogger("MainWindowViewModel");
            
            //initialize data
            Initialize();

            TemperatureViewModel = new TemperatureViewModel(model);
            SockerViewModel = new SockerViewModel(model);
        }

        #endregion

        #region public declaration

        public TemperatureViewModel TemperatureViewModel { get; set; }

        public SockerViewModel SockerViewModel { get; set; }

        public int SelectedStatisticsTab
        {
            get
            {
                return selectedStatisticsTab;
            }
            set
            {
                logger.Log(LogLevel.Info, $"Tab switched to: {value}");
                selectedStatisticsTab = value;
            }
        }

        #endregion

        #region methods

        private void Initialize()
        {
            //initialize model
            model = new Model();

            model.SockerRankings = FileHandler.ReadSocker();
            model.TemperatureRankings = FileHandler.ReadTemperature();
        }

        #endregion
    }
}
