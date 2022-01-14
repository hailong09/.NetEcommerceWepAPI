namespace backend.Settings
{
    public class MongoDbSettings
    {
        public string User { get; set; }
        public string Password { get; set; }

        public string ConnectionString
        {
            get
            {
                return $"mongodb+srv://{User}:{Password}@shopping.tsk56.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";
            }
        }
    }
}