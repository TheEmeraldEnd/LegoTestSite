using MySqlConnector;
using Newtonsoft.Json;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LegoTestSite.DatabaseAccessors.DatabaseConnectionManagers
{
    public class MySQLConnectionManager : IDatabaseAccessor
    {
        #region Connection Loggin Details
        /// <summary>
        /// Used to identify the user in the database
        /// </summary>
        private static string _userID;
        public static string UserID
        {
            set
            {
                _userID = value;
            }
        }

        /// <summary>
        /// Where is the database server located
        /// </summary>
        private static string _serverIP;
        public static string ServerIP
        {
            set
            {
                _serverIP = value;
            }
        }

        /// <summary>
        /// The password 
        /// </summary>
        private static string _password;
        public static string Password
        {
            set
            {
                _password = value;
            }
        }

        /// <summary>
        /// Used to direct to which database to access
        /// </summary>
        private static string _databaseName;
        public static string DatabaseName
        {
            set
            {
                _databaseName = value;
            }
        }

        public bool _isInitializedConnectionConnected = false;
        public bool IIsInstantiationConnectionConnected
        {
            get
            {
                return _isInitializedConnectionConnected;
            }
            set
            {
                _isInitializedConnectionConnected = value;
            }
        }

        /// <summary>
        /// Used to get the connection ID to be able to get to the MySQL database
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionString()
        {
            return GetConnectionString(_databaseName);
        }

        /// <summary>
        /// Used to get the connection ID to be able to get to the MySQL database (capable of using dual)
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionString(string databaseName)
        {
            string userIDFormattedString = $"User ID={_userID};";
            string serverIDFormattedString = $"Server={_serverIP};";
            string passwordFormattedString = $"Password={_password};";
            string databaseNameFormattedString = $"Database={databaseName};";

            string connectionString =
                serverIDFormattedString +
                userIDFormattedString +
                passwordFormattedString +
                databaseNameFormattedString;

            return connectionString;
        }

        /// <summary>
        /// Attempts to initialize the connection (is required at least for the connection).
        /// </summary>
        public void IInitializeConnection()
        {
            #region Login
            SensitiveReader.PrepLoginCredentials();

            MySQLConnectionManager.UserID = SensitiveReader.GetUserID();
            MySQLConnectionManager.ServerIP = SensitiveReader.GetServerIP();
            MySQLConnectionManager.Password = SensitiveReader.GetPassword();
            MySQLConnectionManager.DatabaseName = SensitiveReader.GetDatabaseName();
            #endregion

            string connectionString = GetConnectionString();

            if (_connectionInstance is null)
                _connectionInstance = new(connectionString);

            try
            {
                _isInitializedConnectionConnected = _connectionInstance.Ping();
            }
            catch(Exception exception)
            {
                Console.WriteLine($"{exception.Message}");
            }
            
        }
        #endregion

        #region Connection Instance
        /// <summary>
        /// Get's a connection set up while being called for
        /// </summary>
        private static MySqlConnection ConnectionInstance
        {
            get
            {
                return _connectionInstance;
            }
        }
        private static MySqlConnection _connectionInstance;
        #endregion

        #region Private Methods
        /// <summary>
        /// Primarily intended for use in SELECT commands. Proceed with caution.
        /// </summary>
        /// <param name="unsafeCommand"></param>
        /// <returns></returns>
        private static DataTable GetData(string unsafeCommand)
        {
            ConnectionInstance.Open();

            #region Get Results
            MySqlCommand command = new(unsafeCommand, ConnectionInstance);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
            DataTable result = new();
            dataAdapter.Fill(result);
            #endregion

            ConnectionInstance.Close();

            return result;
        }

        /// <summary>
        /// Converts data to json format
        /// </summary>
        /// <param name="incomingData"></param>
        /// <returns></returns>
        private static string ConvertDataToJSON(DataTable incomingData)
        {
            string JSONResult = JsonConvert.SerializeObject(incomingData, Formatting.Indented);
            return JSONResult;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Returns a result of data as a string (recommended to only use for basic data types)
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="columnName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static List<string> GetColumn(string columnName, string tableName, string databaseName)
        {
            var tableSet = GetData($"SELECT {columnName} FROM {databaseName}.{tableName}");
            List<string> result = new();

            foreach (DataColumn column in tableSet.Columns)
            {
                foreach (DataRow row in tableSet.Rows)
                {
                    result.Add(row[column].ToString());
                }
            }
            result.TrimExcess();


            return result;
        }

        /// <summary>
        /// Used to get the column names of a table
        /// </summary>
        /// <param name="tableName"></param>
        ///     Table name only for some reason (No need for database name)
        /// <returns></returns>
        public static List<string> GetColumnNames(string tableName, string databaseName)
        {
            var tableSet = GetData($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE " +
                $"(TABLE_NAME = N'{tableName}') && (TABLE_SCHEMA = N'{databaseName}');");
            List<string> result = new();

            foreach (DataColumn column in tableSet.Columns)
            {
                foreach (DataRow row in tableSet.Rows)
                {
                    result.Add(row[column].ToString());
                }
            }
            result.TrimExcess();

            return result;
        }

        public static  List<string> GetColumnNames(string tableName)
        {
            var tableSet = GetData($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE " +
                $"(TABLE_NAME = N'{tableName}') && (TABLE_SCHEMA = N'{_databaseName}');");
            List<string> result = new();

            foreach (DataColumn column in tableSet.Columns)
            {
                foreach (DataRow row in tableSet.Rows)
                {
                    result.Add(row[column].ToString());
                }
            }
            result.TrimExcess();

            return result;
        }

        //TODO: Pass through column names to get the key and result columns to search through
        //?Do not use until teacher comes back with result on how to use prepared statements
        public static List<string> GetSpecificResult(string keyColumn, string resultColumn, string tableName, string databaseName)
        {
            //TODO: use prepared statement to get specific thing
            //string unsafeCommand = "";
            //GetData()

            return null;
        }

        #endregion

        #region Specific Methods for Lego application
        /// <summary>
        /// Gets the details of each set. This will allow a good overlook of all sets.
        /// </summary>
        /// <returns></returns>
        public string IGetSetGallery()
        {
            string command = 
                "SELECT SetID, Name, TotalSetPieces, IsTested, PhotoURL " +
                "FROM set_table;";
            DataTable data = GetData(command);
            return ConvertDataToJSON(data);
        }

        /// <summary>
        /// Get's the set details for a certain set in JSON form.
        ///     This is needed for planned SetDetails.HTML INFO tab.
        /// </summary>
        /// <param name="incomingSetID"></param>
        /// <returns></returns>
        public string IGetSetDetails(string incomingSetID)
        {
            string command =
                $"SELECT SetID, IsTested, Notes, PhotoURL, Name, InstructionsURL, TotalSetPieces " +
                $"FROM set_table " +
                $"WHERE SetID = '{incomingSetID}';";
            DataTable data = GetData(command);
            return ConvertDataToJSON(data);
        }

        /// <summary>
        /// Gets the info per bag, with the bag number, legoID, photoURL of lego piece, 
        ///     sticker number, and Total amount of pieces per bag (TotalPerBagQuantity)
        ///     as a JSON file.
        /// </summary>
        /// <param name="incomingSetID"></param>
        /// <returns></returns>
        /// 
        //Something is wrong here
        public string IGetSetDetailsBagsInfo(string incomingSetID)
        {
            string command =
            //$"SELECT " +
            //$"\r\n\tbag_table.BagNumber AS BagNumber, " +
            //$"\r\n    lego_table.LegoID AS LegoID, " +
            //$"\r\n    lego_table.PhotoURL AS PhotoURL, " +
            //$"\r\n    instruction_detail_table.StickerNumber AS StickerNumber," +
            //$"\r\n    SUM(instruction_detail_table.Quantity) AS TotalPerBagQuantity" +
            //$"\r\nFROM (((bag_table\r\n\tLEFT JOIN instruction_table" +
            //$"\r\n    ON (bag_table.StartInstructionNumber <= instruction_table.InstructionNumber)" +
            //$"\r\n\t\tAND (instruction_table.InstructionNumber <= bag_table.EndInstructionNumber)" +
            //$"\r\n        AND (instruction_table.SetID_FK = {incomingSetID})" +
            //$"\r\n        AND (bag_table.SetIDFK = {incomingSetID}))" +
            //$"\r\n\t\t\tLEFT JOIN instruction_detail_table" +
            //$"\r\n\t\t\tON (instruction_detail_table.InstructionNumberFK = instruction_table.InstructionNumber)" +
            //$"\r\n\t\t\t\tAND instruction_detail_table.Set_ID_FK = {incomingSetID})" +
            //$"\r\n\t\t\t\tLEFT JOIN lego_table" +
            //$"\r\n                ON instruction_detail_table.LegoIDFK = lego_table.LegoID) " +
            //$"\r\nGROUP BY BagNumber, LegoID, StickerNumber;";

            $"SELECT " +
            $"  bag_table.SetIDFK AS SetID, " +
            $"  bag_table.BagNumber AS BagNumber, " +
            $"  lego_table.LegoID AS LegoID, " +
            $"  lego_table.PhotoURL AS PhotoURL, " +
            $"  instruction_detail_table.StickerNumber AS StickerNumber, " +
            $"  SUM(instruction_detail_table.Quantity) AS TotalPerBagQuantity " +
            $"FROM (((bag_table LEFT JOIN instruction_table " +
            $"  ON (bag_table.StartInstructionNumber <= instruction_table.InstructionNumber) " +
            $"      AND (instruction_table.InstructionNumber <= bag_table.EndInstructionNumber) " +
            $"      AND (instruction_table.SetID_FK = '{incomingSetID}') " +
            $"      AND (bag_table.SetIDFK = '{incomingSetID}')) " +
            $"  LEFT JOIN instruction_detail_table " +
            $"      ON (instruction_detail_table.InstructionNumberFK = instruction_table.InstructionNumber) " +
            $"          AND instruction_detail_table.Set_ID_FK = '{incomingSetID}') " +
            $"  LEFT JOIN lego_table " +
            $"      ON instruction_detail_table.LegoIDFK = lego_table.LegoID) " +
            $"WHERE bag_table.SetIDFK = '{incomingSetID}' " +
            $"GROUP BY BagNumber, LegoID, StickerNumber; ";
            
            DataTable data = GetData(command);
            string jsonData = ConvertDataToJSON(data);
            return jsonData;
        }

        /// <summary>
        /// Gets the notes info in JSON format for a specific set by setID.
        /// </summary>
        /// <param name="incomingSetID"></param>
        /// <returns></returns>
        public string IGetSetDetailsNotesInfo(string incomingSetID)
        {
            string command = 
                $"SELECT SetID, Name, Notes " +
                $"FROM set_table " +
                $"WHERE SetID = {incomingSetID};";
            DataTable data = GetData(command);
            return ConvertDataToJSON(data);
        }
        #endregion
    }
}
