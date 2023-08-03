namespace CasgemMicroservices.Catolog.Settings.Abstract
{
    public interface IDatabaseSettings
    {
        //tanımlayacağım tablo mongo db deki tablo coleksiyon veritabanı adı özelliklerini burada tutacağım 
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CatgeoryCollectionName { get; set; }
        public string ProductCollectionName { get; set; }
    }
}
