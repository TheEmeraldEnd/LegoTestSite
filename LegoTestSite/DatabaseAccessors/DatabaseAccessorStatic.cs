using LegoTestSite.DatabaseAccessors.DatabaseConnectionManagers;

namespace LegoTestSite.DatabaseAccessors
{
    public static class DatabaseAccessorStatic
    {
        static DatabaseAccessorStatic()
        {
            IsDatabaseRefSameAsTest = false;
            WasAbleToConnectToMain = false;
            DatabaseRef = null;
        }

        #region Database Connection
        public static bool IsDatabaseRefSameAsTest
        {
            get;
            private set;
        }

        public static bool WasAbleToConnectToMain
        {
            get;
            private set;
        }

        public static IDatabaseAccessor DatabaseRef
        {
            get;
            private set;
        }

        private static IDatabaseAccessor _testDatabaseRefInstance = null;
        private static IDatabaseAccessor _mainDatabaseRefInstance = null;

        public static void InitializeDatabaseRef(
            IDatabaseAccessor tempMainRef,
            IDatabaseAccessor tempTestRef,
            bool isDefaultToTest)
        {
            _testDatabaseRefInstance = tempTestRef;
            _mainDatabaseRefInstance = tempMainRef;

            _testDatabaseRefInstance.IInitializeConnection();
            _mainDatabaseRefInstance.IInitializeConnection();

            WasAbleToConnectToMain = _mainDatabaseRefInstance.IIsInstantiationConnectionConnected;

            DetermineDatabaseRef(isDefaultToTest);
        }

        private static void DetermineDatabaseRef(bool isDefaultToTest)
        {
            //? Main isn't being connected?
            //TODO: Use test to figure out why it isn't being connected
            bool isMainConnectable = _mainDatabaseRefInstance.IIsInstantiationConnectionConnected;

            if (isDefaultToTest || !isMainConnectable)
            {
                DatabaseRef = _testDatabaseRefInstance;
                IsDatabaseRefSameAsTest = true;
            }
            else
            {
                DatabaseRef = _mainDatabaseRefInstance;
                IsDatabaseRefSameAsTest = false;
            }
                
        }
        #endregion

        #region Website Data Methods
        public static string GetSetDetailsBagsInfo(string setID)
        {
            return DatabaseRef.IGetSetDetailsBagsInfo(setID);
        }

        public static string GetSetDetails(string setID)
        {
            return DatabaseRef.IGetSetDetails(setID);
        }

        public static string GetSetDetailsNotesInfo(string setID)
        {
            return DatabaseRef.IGetSetDetailsNotesInfo(setID);
        }

        public static string GetSetGallery()
        {
            return DatabaseRef.IGetSetGallery();
        }
        #endregion

        #region Website Data Methods
        public static string MainGetSetDetailsBagsInfo(string setID)
        {
            return _mainDatabaseRefInstance.IGetSetDetailsBagsInfo(setID);
        }

        public static string MainGetSetDetails(string setID)
        {
            return _mainDatabaseRefInstance.IGetSetDetails(setID);
        }

        public static string MainGetSetDetailsNotesInfo(string setID)
        {
            return _mainDatabaseRefInstance.IGetSetDetailsNotesInfo(setID);
        }

        public static string MainGetSetGallery()
        {
            return _mainDatabaseRefInstance.IGetSetGallery();
        }
        #endregion

        #region Test Methods
        public static string TestGetSetDetailsBagsInfo(string setID)
        {
            return _testDatabaseRefInstance.IGetSetDetailsBagsInfo(setID);
        }

        public static string TestGetSetDetails(string setID)
        {
            return _testDatabaseRefInstance.IGetSetDetails(setID);
        }

        public static string TestGetSetDetailsNotesInfo(string setID)
        {
            return _testDatabaseRefInstance.IGetSetDetailsNotesInfo(setID);
        }

        public static string TestGetSetGallery()
        {
            return _testDatabaseRefInstance.IGetSetGallery();
        }
        #endregion

        #region Old Section
        //private static IDatabaseAccessor? _databaseConnection;
        //public static IDatabaseAccessor DatabaseConnection
        //{
        //    set
        //    {
        //        if (_databaseConnection is null)
        //            _databaseConnection = value;
        //    }
        //    private get
        //    {
        //        return _databaseConnection;
        //    }
        //}

        ///// <summary>
        ///// Starts the connection to the database
        ///// </summary>
        ///// <param name="isTestEnviroment"></param>
        //public static void InitializeDatabaseConnection(bool isTestEnviroment)
        //{
        //    //Attempt intended database connection
        //    //!? 3/10/2026 Not sure why mysql connects, but says it's not
        //    //TODO: Fix messaging of MySQL connection (has something to do with null connection instance)
        //    //! MySQL Connection Works
        //    //! SQLite connection works
        //    DatabaseConnection = GetDatabase(isTestEnviroment);
        //    DatabaseConnection.IInitializeConnection();

        //    //If initial connection can't be reached, then default to test database
        //    if (DatabaseConnection.IIsInstantiationConnectionConnected == false &&
        //            isTestEnviroment == false)
        //    {
        //        DatabaseConnection = GetDatabase(true);
        //        DatabaseConnection.IInitializeConnection();
        //    }
        //}

        //private static IDatabaseAccessor GetDatabase(bool isTestEnviroment)
        //{
        //    IDatabaseAccessor tempDatabaseHolder;

        //    if (isTestEnviroment == true)
        //        tempDatabaseHolder = new SQLiteConnectionManager();
        //    else
        //        tempDatabaseHolder = new MySQLConnectionManager();


        //    return tempDatabaseHolder;

        //}

        //public static string GetSetDetailsBagsInfo(string setID)
        //{
        //    return DatabaseConnection.IGetSetDetailsBagsInfo(setID);
        //}

        //public static string GetSetDetails(string setID)
        //{
        //    return DatabaseConnection.IGetSetDetails(setID);
        //}

        //public static string GetSetDetailsNotesInfo(string setID)
        //{
        //    return DatabaseConnection.IGetSetDetailsNotesInfo(setID);
        //}

        //public static string GetSetGallery()
        //{
        //    return DatabaseConnection.IGetSetGallery();
        //}
        #endregion
    }
}
