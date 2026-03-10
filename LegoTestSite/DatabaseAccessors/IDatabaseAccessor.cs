

namespace LegoTestSite.DatabaseAccessors
{
    public interface IDatabaseAccessor
    {
        public abstract string IGetSetDetailsBagsInfo(string setID);

        public abstract string IGetSetDetails(string setID);

        public abstract string IGetSetDetailsNotesInfo(string setID);

        public abstract string IGetSetGallery();

        public abstract void IInitializeConnection();

        public bool IIsInstantiationConnectionConnected
        {
            get;
            protected set;
        }
    }
}
