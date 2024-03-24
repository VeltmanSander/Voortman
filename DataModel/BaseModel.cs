namespace DataModel
{
    public abstract class BaseModel
    {
        #region constructors

        public BaseModel(int id)
        {
            Id = id;
        }

        #endregion

        #region public declaration

        public int Id { get; private set; }

        public abstract double Difference { get; }

        #endregion
    }
}
