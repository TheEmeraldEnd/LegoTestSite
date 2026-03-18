namespace LegoTestSite.DatabaseAccessors
{
    public class DatabaseErrorStatic
    {
        public static string ErrorMessage
        {
            get
            {
                return "Error";
            }
        }

        public static string SuccessMessage
        {
            get
            {
                return "Success";
            }
        }

        public static bool IsDatabaseRefSameAsTest(ILogger logger)
        {
            if (!DatabaseAccessorStatic.IsDatabaseRefSameAsTest)
            {
                logger.LogWarning("Database ref set to main");
                return false;
            }
            else
            {
                logger.LogInformation("Database ref set to main");
                return true;
            }
        }

        public static bool IsMainConnectable(ILogger logger)
        {
            if (DatabaseAccessorStatic.WasAbleToConnectToMain)
            {
                logger.LogInformation("Database was initialized with main connection");
                return true;
            }
            else
            {
                logger.LogWarning("Database wasn't able to connect to main on initialization");
                return false;
            }
        }
    }
}
