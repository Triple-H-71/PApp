
namespace PApp
{
    public interface IDBConnector
    {
        public string GetDatabaseName();
    }
    public class DBConnector : IDBConnector
    {
        public string GetDatabaseName()
        {
            var dbstring = "Server=tcp:hunterh.database.windows.net,1433;Initial Catalog=Main;Persist Security Info=False;User ID=hunterh_admin;Password=XsYXkPVnMn3JD54;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            return dbstring;
        }
    }
}
