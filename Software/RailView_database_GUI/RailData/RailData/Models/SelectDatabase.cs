namespace RailData.Models
{
    public class SelectDatabase
    {
        public string DatabaseName { get; set; }
        public string Username { get; set; }

        public SelectDatabase() : this("RailView", "admin") { }

        public SelectDatabase(string databaseName, string username)
        {
            DatabaseName = databaseName;
            Username = username;
        }

        public string SetDatabaseSession()
        {
            return $"Server=192.168.161.205;Port=3306;Database={DatabaseName};Uid={Username};Pwd=TopMaster99;";
        }
    }
}
