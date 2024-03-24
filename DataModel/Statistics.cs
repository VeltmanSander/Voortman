using System.Collections.Generic;

namespace DataModel
{
    public class Statistics<T>
    {
        #region constructors

        public Statistics(string name)
        {
            Name = name;
            Rankings = new List<T>();
        }

        #endregion

        #region public declaration

        public string Name { get; private set; }

        public List<T> Rankings { get; set; }

        #endregion
    }
}
