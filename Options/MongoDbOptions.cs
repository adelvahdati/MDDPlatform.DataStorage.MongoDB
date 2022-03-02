namespace MDDPlatform.DataStorage.MongoDB.Options{
    public class MongoDbOption {
        public string ConnectionString { get; set; }
        public string Database { get; set; }

        public MongoDbOption(){

        }
        public MongoDbOption(string connectionString, string database)
        {
            ConnectionString = connectionString;
            Database = database;
        }
    }    
}