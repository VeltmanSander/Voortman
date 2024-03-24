using NLog;

namespace DataModel.Temp
{
    public class Temperature : BaseModel
    {
        #region private declaration

        private static readonly int[] fieldWidths = { 0, 4, 8, 14, 19, 28 };
        protected static Logger logger;
        private int min;
        private int max;

        #endregion

        #region constructors

        public Temperature(int id, int min, int max) : base(id)
        {
            logger = LogManager.GetLogger("Temperature");

            this.min = min;
            this.max = max;
        }

        #endregion

        #region static declaration

        /// <summary>
        /// Parse data and create Temperature object
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Temperature ParseTemperature(string data)
        {
            if(string.IsNullOrEmpty(data))
            {
                logger.Log(LogLevel.Debug, "ParseTemperature data empty");
                return null;
            }

            Temperature temperature = null;
            string idValue = data.Substring(fieldWidths[0], fieldWidths[1] - fieldWidths[0]);
            string maxValue = data.Substring(fieldWidths[1], fieldWidths[2] - fieldWidths[1]);
            string minValue = data.Substring(fieldWidths[2], fieldWidths[3] - fieldWidths[2]);
            int id = -1;
            int max = -1;
            int min = -1;

            if (int.TryParse(idValue, out id))
            {
                logger.Log(LogLevel.Error, | $"ParseTemperature data invalid -> id value {idValue}");
                return null;
            }
            if (int.TryParse(maxValue.Replace("*", ""), out max))
            {
                logger.Log(LogLevel.Error, $"ParseTemperature data invalid -> max value {maxValue}");
                return null;
            }
            if (int.TryParse(minValue.Replace("*", ""), out min))
            {
                logger.Log(LogLevel.Error, $"ParseTemperature data invalid -> min value {minValue}");
                return null;
            }
            
            temperature = new Temperature(id, min, max);
            return temperature;
        }

        #endregion

        #region public declaration

        public override double Difference
        {
            get
            {
                return max - min;
            }
        }

        #endregion

        #region public methods

        public override string ToString()
        {
            return $"Id:{Id}, min:{min}, max:{max}, Difference:{Difference}";
        }

        #endregion
    }
}
