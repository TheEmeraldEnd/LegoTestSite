using LegoTestSite.DatabaseAccessors.DatabaseConnectionManagers;

namespace LegoTestSite.DatabaseAccessors
{
    public static class DatabaseAccessorStatic
    {
        private static IDatabaseAccessor? _databaseConnection;
        public static IDatabaseAccessor DatabaseConnection
        {
            set
            {
                if (_databaseConnection is null)
                    _databaseConnection = value;
            }
            private get
            {
                return _databaseConnection;
            }
        }

        /// <summary>
        /// Starts the connection to the database
        /// </summary>
        /// <param name="isTestEnviroment"></param>
        public static void InitializeDatabaseConnection(bool isTestEnviroment)
        {
            //Attempt intended database connection
            //!? 3/10/2026 Not sure why mysql connects, but says it's not
            //TODO: Fix messaging of MySQL connection (has something to do with null connection instance)
            //! MySQL Connection Works
            //! SQLite connection works
            DatabaseConnection = GetDatabase(isTestEnviroment);
            DatabaseConnection.IInitializeConnection();

            //If initial connection can't be reached, then default to test database
            if (DatabaseConnection.IIsInstantiationConnectionConnected == false &&
                    isTestEnviroment == false)
            {
                DatabaseConnection = GetDatabase(true);
                DatabaseConnection.IInitializeConnection();
            }
        }

        private static IDatabaseAccessor GetDatabase(bool isTestEnviroment)
        {
            IDatabaseAccessor tempDatabaseHolder;

            if (isTestEnviroment == true)
                tempDatabaseHolder = new SQLiteConnectionManager();
            else
                tempDatabaseHolder = new MySQLConnectionManager();


            return tempDatabaseHolder;
            
        }

        public static string GetSetDetailsBagsInfo(string setID)
        {
            return DatabaseConnection.IGetSetDetailsBagsInfo(setID);
        }

        public static string GetSetDetails(string setID)
        {
            return DatabaseConnection.IGetSetDetails(setID);
        }

        public static string GetSetDetailsNotesInfo(string setID)
        {
            return DatabaseConnection.IGetSetDetailsNotesInfo(setID);
        }

        public static string GetSetGallery()
        {
            return DatabaseConnection.IGetSetGallery();
        }
    }
}
