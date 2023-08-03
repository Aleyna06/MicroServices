using CasgemMicroservices.Catolog.Settings.Abstract;

namespace CasgemMicroservices.Catolog.Settings.Concrete
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CatgeoryCollectionName { get; set; }
        public string ProductCollectionName { get; set; }
    }
}
