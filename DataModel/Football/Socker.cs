using NLog;
using System;

namespace DataModel.Football
{
    public class Socker : BaseModel
    {
        #region private declaration

        protected static Logger logger;
        private int forGoals;
        private int againstGoals;

        #endregion

        #region constructors

        public Socker(int id, string name, int forGoals, int againstGoals) : base(id)
        {
            logger = LogManager.GetLogger("Socker");

            Name = name;
            this.forGoals = forGoals;
            this.againstGoals = againstGoals;
        }

        #endregion

        #region static declaration

        /// <summary>
        /// Parse data and create Socker object
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Socker ParseSocker(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                logger.Log(LogLevel.Debug, "ParseSocker data empty");
                return null;
            }

            Socker socker = null;
            string[] delimiters = new string[] { " " };
            string[] values = data.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            if (values.Length != 10)
            {
                logger.Log(LogLevel.Debug, "ParseTemperature data invalid");
                return null;
            }

            int idValue = -1;
            string nameValue = string.Empty;
            int forGoalsValue = -1;
            int againstGoalsValue = -1;

            nameValue = values[1];

            if(int.TryParse(values[0].Replace(".",""), out idValue))
            {
                logger.Log(LogLevel.Error, | $"ParseSocker data invalid -> id value {idValue}");
                return null;
            }
            if(int.TryParse(values[6], out forGoalsValue))
            {
                logger.Log(LogLevel.Error, | $"ParseSocker data invalid -> for goals value {forGoalsValue}");
                return null;
            }
            if(int.TryParse(values[8], out againstGoalsValue))
            {
                logger.Log(LogLevel.Error, | $"ParseSocker data invalid -> against goals value {againstGoalsValue}");
                return null;
            }

            socker = new Socker(idValue, nameValue, forGoalsValue, againstGoalsValue);
            return socker;
        }

        #endregion

        #region public declaration

        public string Name { get; private set; }

        public override double Difference
        {
            get
            {
                return Math.Abs(forGoals - againstGoals);
            }
        }

        #endregion

        #region methods

        public override string ToString()
        {
            return $"Id:{Id}, ForGoals:{forGoals}, AgainstGoals:{againstGoals}, Difference:{Difference}";
        }

        #endregion
    }
}
