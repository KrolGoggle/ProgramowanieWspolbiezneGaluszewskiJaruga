namespace DataLayer
{
    public abstract class DataAbstractAPI

    {
        public static DataAbstractAPI createDataAPI()
        {
            return new DataLayer();
        }

        public abstract int GetBoardWidth();
        public abstract int GetBoardLength();

        public abstract int GetRadius();

    }

}
