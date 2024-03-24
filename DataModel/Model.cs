using DataModel.Football;
using DataModel.Temp;

namespace DataModel
{
    public class Model
    {
        #region constructors

        public Model()
        {
        }

        #endregion

        #region public declaration

        public Statistics<Socker> SockerRankings { get; set; }

        public Statistics<Temperature> TemperatureRankings { get; set; }

        #endregion
    }
}
