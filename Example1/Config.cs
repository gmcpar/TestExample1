using System.Configuration;

public class Config
{
    public static string ConnectionString
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["DataStoreConnectionString"].ConnectionString;
        }
    }
}