namespace Aktie_WebAPI.Model
{
    public class Stock
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public string AssetType { get; set; }
        public string IpoDate { get; set; }
        public string DelistingDate { get; set; }
        public string Status { get; set; }
    }
}
