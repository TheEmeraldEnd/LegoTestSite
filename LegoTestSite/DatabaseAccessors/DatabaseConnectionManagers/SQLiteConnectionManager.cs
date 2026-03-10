using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using MySqlConnector;
using Newtonsoft.Json;
using System.Data;

namespace LegoTestSite.DatabaseAccessors.DatabaseConnectionManagers
{
    public class SQLiteConnectionManager : IDatabaseAccessor
    {
        
        private static SqliteConnection _connection;

        private bool _isInstantiationConnectionConnected = false;
        public bool IIsInstantiationConnectionConnected
        {
            get
            {
                return _isInstantiationConnectionConnected;
            }
            set
            {
                _isInstantiationConnectionConnected = value;
            }
        }

        public void IInitializeConnection()
        {
            if (_connection is null)
                _connection = new SqliteConnection("Data Source=Databases/TestDataDB.db;");

            try
            {
                _connection.Open();
                _connection.Close();
                IIsInstantiationConnectionConnected = true;
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// TODO: Need to test
        /// </summary>
        /// <param name="unsafeCommand"></param>
        /// <returns></returns>
        private static DataTable GetData(string unsafeCommand)
        {
            _connection.Open();

            using var command = _connection.CreateCommand();
            command.CommandText = unsafeCommand;
            var sqlAdapter = command.ExecuteReader();
            
            
            
            DataTable result = new();
            result.Load(sqlAdapter);

            _connection.Close();
            return result;
        }

        /// <summary>
        /// Converts data to json format
        /// </summary>
        /// <param name="incomingData"></param>
        /// <returns></returns>
        private string ConvertDataToJSON(DataTable incomingData)
        {
            string JSONResult = JsonConvert.SerializeObject(incomingData, Formatting.Indented);
            return JSONResult;
        }

        public string IGetSetGallery()
        {
            string command =
                "SELECT SetID, Name, TotalSetPieces, IsTested, PhotoURL " +
                "FROM set_table;";
            DataTable data = GetData(command);
            return ConvertDataToJSON(data);
        }

        public string IGetSetDetails(string incomingSetID)
        {
            string command =
                $"SELECT SetID, IsTested, Notes, PhotoURL, Name, InstructionsURL, TotalSetPieces " +
                $"FROM set_table " +
                $"WHERE SetID = '{incomingSetID}';";
            DataTable data = GetData(command);
            return ConvertDataToJSON(data);
        }

        public string IGetSetDetailsBagsInfo(string incomingSetID)
        {
            string command =

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

        public string IGetSetDetailsNotesInfo(string incomingSetID)
        {
            string command =
                $"SELECT SetID, Name, Notes " +
                $"FROM set_table " +
                $"WHERE SetID = {incomingSetID};";
            DataTable data = GetData(command);
            return ConvertDataToJSON(data);
        }
    }
}
